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
    public class King
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Select = false;
        public bool Black = false;

        public Grid Figure { get; set; }

        public King(int X, int Y, bool Black)
        {
            this.X = X;
            this.Y = Y;
            this.Black = Black;
        }
        public void SelectFigure(object sender, MouseButtonEventArgs e)
        {
            bool atack = false;
            King SelectKing = MainWindow.init.king.Find(x => x.Select == true);
            if (SelectKing != null)
            {
                if (Black && Y - 1 == SelectKing.Y && (X == SelectKing.X - 1 || X == SelectKing.X + 1) ||
                    !Black && Y + 1 == SelectKing.Y && (X == SelectKing.X - 1 || X == SelectKing.X + 1))
                {
                    MainWindow.init.gameBorder.Children.Remove(Figure);
                    Grid.SetColumn(SelectKing.Figure, X);
                    Grid.SetRow(SelectKing.Figure, Y);

                    SelectKing.X = X;
                    SelectKing.Y = Y;

                    SelectKing.SelectFigure(null, null);
                    return;
                }
            }
            MainWindow.init.OnSelect(this);
            if (Select)
            {
                Figure.Opacity = 1.0;   
                this.Select = false;
            }
            else
            {
                Figure.Opacity = 0.5;
                this.Select = true;
            }
        }
        public void Transform(int X, int Y)
        {
            if (Math.Abs(X - this.X) <= 1 && Math.Abs(Y - this.Y) <= 1)
            {
                Grid.SetColumn(Figure, X);
                Grid.SetRow(Figure, Y);
                this.X = X;
                this.Y = Y;
            }
            SelectFigure(null, null);
        }
    }

}
