using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ches_Патрушева.Classes;

namespace Ches_Патрушева
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Pawn> Pawns = new List<Pawn>();
        public List<King> king = new List<King>();
        public static MainWindow init;

        public static bool WhiteTurn = true; // ход белых
        public MainWindow()
        {
            //добавление фигур на доску
            InitializeComponent();
            init = this;

            for (int i = 0; i <= 7; i++)
            {
                Pawns.Add(new Pawn(i, 1, false));
                Pawns.Add(new Pawn(i, 6, true));
            }
            CreateFigures();


            king.Add(new King(3, 7, true));
            CreateKing();
        }

        //Создание короля и размешение его на доску
        public void CreateKing()
        {
            foreach (King k in king)
            {
                k.Figure = new Grid()
                {
                    Width = 50,
                    Height = 50
                };

                if (k.Black)
                    k.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/images/King.png")));
                Grid.SetColumn(k.Figure, k.X);
                Grid.SetRow(k.Figure, k.Y);
                k.Figure.MouseDown += k.SelectKing;
                gameBorder.Children.Add(k.Figure);
            }
        }

        //создание пешек
        public void CreateFigures()
        {
            foreach (Pawn Pawn in Pawns)
            {
                Pawn.Figure = new Grid()
                {
                    Width = 50,
                    Height = 50
                };
                if (Pawn.Black)
                    Pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/images/Pawn (black).png")));
                else
                    Pawn.Figure.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/images/Pawn.png")));

                Grid.SetColumn(Pawn.Figure, Pawn.X);
                Grid.SetRow(Pawn.Figure, Pawn.Y);
                Pawn.Figure.MouseDown += Pawn.SelectFigure;
                gameBorder.Children.Add(Pawn.Figure);
                    
            }
        }

        //Перемешение фигур
        private void SelectTile(object sender, MouseButtonEventArgs e)
        {
            Grid Tile = sender as Grid;
            int X = Grid.GetColumn(Tile);
            int Y = Grid.GetRow(Tile);

            //если выбрана пешка перейти к методу ее перемешения
            Pawn SelectPawn = MainWindow.init.Pawns.Find(x => x.Select == true);
            if(SelectPawn != null)
                SelectPawn.Transform(X, Y);

            //если выбран король перейти к методу его перемешения
            King SelectKing = MainWindow.init.king.Find(x => x.Select == true);
            if (SelectKing != null)
                SelectKing.Transform(X, Y);
        }

        public void OnSelect(object next)
        {
            //Нельзя выделять одновременно 2 пешки
            foreach (Pawn p in Pawns)
            {
                if (p != next && p.Select)
                    p.SelectFigure(null, null);
            }

            //пешка и король не могут быть веделены одновременно
            foreach (King k in king)
            {
                if (k != next && k.Select)
                    k.SelectKing(null, null);
            }
        }
       
        //наложение подсветки
        public void MarkCell(int x, int y, bool red)
        {
            Rectangle r = new Rectangle();
            r.Width = 50;
            r.Height = 50;
            r.Tag = "hl";                // метка чотб потом найти квадраты и убрать
            r.IsHitTestVisible = false;  // чтоб при клике не задействовало наложенный квадрат

            if (red)
                r.Fill = new SolidColorBrush(Color.FromArgb(120, 255, 0, 0)); 
            else
                r.Fill = new SolidColorBrush(Color.FromArgb(120, 0, 255, 0)); 

            Grid.SetColumn(r, x);
            Grid.SetRow(r, y);
            gameBorder.Children.Add(r);
        }

        // Поиск по метке и удаление
        public void ClearMarks()
        {
            for (int i = gameBorder.Children.Count - 1; i >= 0; i--)
            {
                Rectangle r = gameBorder.Children[i] as Rectangle;
                if (r != null && (string)r.Tag == "hl")
                    gameBorder.Children.RemoveAt(i);
            }
        }

        //Метод проверки свободных клеток
        public bool Free(int x, int y)
        {
            foreach (Pawn p in Pawns) 
                if (p.X == x && p.Y == y) 
                    return false;
            foreach (King k in king) 
                if (k.X == x && k.Y == y) 
                    return false;
            return true;
        }

        //Проверка есть ли враг
        public bool Chack(int x, int y, bool myBlack)
        {
            foreach (Pawn p in Pawns) 
                if (p.X == x && p.Y == y) 
                    return p.Black != myBlack;
            foreach (King k in king) 
                if (k.X == x && k.Y == y) 
                    return k.Black != myBlack;
            return false;
        }
    }
    
}
