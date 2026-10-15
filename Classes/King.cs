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
        public void SelectKing(object sender, MouseButtonEventArgs e)
        {
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
            foreach (Pawn p in MainWindow.init.Pawns)
            {
                if (!p.Black                          
                    && p.Y + 1 == this.Y             
                    && (p.X - 1 == this.X || p.X + 1 == this.X)) 
                {
                    MessageBox.Show("Под шах зачем ?");
                    break;
                }
            }
            SelectKing(null, null);
        }
    }

}
