using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ches_Патрушева.Classes
{
    public class Pawn
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public bool Select = false;
        public bool Black = false;

        public Grid Figure { get; set; }

        public Pawn(int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }

        public void SelectFigure(object sender, MouseButtonEventArgs e)
        {
            King SelectKing = MainWindow.init.king.Find(x => x.Select == true);
            if (SelectKing != null && this.Black != SelectKing.Black && Math.Abs(X - SelectKing.X) <= 1 && Math.Abs(Y - SelectKing.Y) <= 1) //диапазон атаки короля
            {
                MainWindow.init.gameBorder.Children.Remove(Figure); // удаление пешки
                MainWindow.init.Pawns.Remove(this);

                Grid.SetColumn(SelectKing.Figure, X);
                Grid.SetRow(SelectKing.Figure, Y);
                SelectKing.X = X;
                SelectKing.Y = Y;

                SelectKing.SelectKing(null, null);
                MainWindow.WhiteTurn = !MainWindow.WhiteTurn;
                return;
            }

            Pawn SelectPawn = MainWindow.init.Pawns.Find(x => x.Select == true);
            if(SelectPawn != null)
            {
                if(Black && Y - 1 == SelectPawn.Y && (X == SelectPawn.X - 1 || X == SelectPawn.X + 1) ||
                    !Black && Y + 1 == SelectPawn.Y && (X == SelectPawn.X - 1 || X == SelectPawn.X + 1)) // диапазон атаки пешки
                {
                    MainWindow.init.gameBorder.Children.Remove(Figure);  //удаление пешки
                    Grid.SetColumn(SelectPawn.Figure, X);
                    Grid.SetRow(SelectPawn.Figure, Y);
                    SelectPawn.X = X;
                    SelectPawn.Y = Y;

                    SelectPawn.SelectFigure(null, null);
                    MainWindow.WhiteTurn = !MainWindow.WhiteTurn;
                    return;
                }
            }

            MainWindow.init.OnSelect(this);
            if (Select)
            {
                if (Black)
                    Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/images/Pawn (black).png")));
                else
                    Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/images/Pawn.png")));

                this.Select = false;
                MainWindow.init.ClearMarks();
                return;
            }
            if (this.Black == MainWindow.WhiteTurn)
                return;

            
            MainWindow.init.OnSelect(this);
            this.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/images/Pawn (select).png")));
            this.Select = true;
            MainWindow.init.ClearMarks();




                //ПОДСВЕТКА
                if (Black)
                {
                    //черные
                    if (MainWindow.init.Free(this.X, this.Y - 1))
                    {
                        MainWindow.init.MarkCell(this.X, this.Y - 1, false); //подсвет на одну клетку вперед
                    if (this.Y == 6 && MainWindow.init.Free(this.X, this.Y - 2))
                            MainWindow.init.MarkCell(this.X, this.Y - 2, false); //подсвет на одну клетку вперед
                }
                    // проверка врагов
                    if (MainWindow.init.Chack(this.X - 1, this.Y - 1, true)) // проверка левой диоганали
                    MainWindow.init.MarkCell(this.X - 1, this.Y - 1, true);
                    if (MainWindow.init.Chack(this.X + 1, this.Y - 1, true)) // проверка правой диоганали
                    MainWindow.init.MarkCell(this.X + 1, this.Y - 1, true);
                }
                else
                {
                    //белые
                    if (MainWindow.init.Free(this.X, this.Y + 1))
                    {
                        MainWindow.init.MarkCell(this.X, this.Y + 1, false); //подсвет на одну клетку вперед
                        if (this.Y == 1 && MainWindow.init.Free(this.X, this.Y + 2))
                            MainWindow.init.MarkCell(this.X, this.Y + 2, false);  //подсвет на две клетки вперед
                    }
                    //проверка врагов
                    if (MainWindow.init.Chack(this.X - 1, this.Y + 1, false)) //проверка левой диоганали
                        MainWindow.init.MarkCell(this.X - 1, this.Y + 1, true); // нашли врага (красный)
                    if (MainWindow.init.Chack(this.X + 1, this.Y + 1, false)) // проверка правой диогнали
                        MainWindow.init.MarkCell(this.X + 1, this.Y + 1, true);
                }
            }
        

        public void Transform(int X, int Y)
        {
            if(X != this.X)
            {
                SelectFigure(null, null);
                return;
            }

            bool moved = false;  // переменая для переключения ходов

            if (Black && ((this.Y == 6 && this.Y - 2 == Y) || this.Y - 1 == Y) ||
                !Black && ((this.Y == 1 && this.Y + 2 == Y) || this.Y + 1 == Y))
            {
                Grid.SetColumn(Figure, X);
                Grid.SetRow(Figure, Y);

                this.X = X;
                this.Y = Y;
                moved = true; // фиксация кода
            }
            if (moved) 
                MainWindow.WhiteTurn = !MainWindow.WhiteTurn;  //больше не ход белых
            if (!this.Black)
            {
                King k = MainWindow.init.king[0];
                if (k.Y == this.Y + 1 && (k.X == this.X - 1 || k.X == this.X + 1))
                {
                    MessageBox.Show("Шах!");
                }
            }
            MainWindow.init.ClearMarks();
            SelectFigure(null, null);
        }
    }
}
