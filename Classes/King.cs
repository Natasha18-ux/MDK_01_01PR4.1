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
                MainWindow.init.ClearMarks();
                return;
            }
            if (this.Black == MainWindow.WhiteTurn)
                return;
            else
            {
                Figure.Opacity = 0.5;
                this.Select = true;

                //GJLCDTNRF
                MainWindow.init.ClearMarks();

                // зелёные — пустые
                if (MainWindow.init.Free(this.X - 1, this.Y - 1)) MainWindow.init.MarkCell(this.X - 1, this.Y - 1, false);
                if (MainWindow.init.Free(this.X, this.Y - 1)) MainWindow.init.MarkCell(this.X, this.Y - 1, false);
                if (MainWindow.init.Free(this.X + 1, this.Y - 1)) MainWindow.init.MarkCell(this.X + 1, this.Y - 1, false);
                if (MainWindow.init.Free(this.X - 1, this.Y)) MainWindow.init.MarkCell(this.X - 1, this.Y, false);
                if (MainWindow.init.Free(this.X + 1, this.Y)) MainWindow.init.MarkCell(this.X + 1, this.Y, false);
                if (MainWindow.init.Free(this.X - 1, this.Y + 1)) MainWindow.init.MarkCell(this.X - 1, this.Y + 1, false);
                if (MainWindow.init.Free(this.X, this.Y + 1)) MainWindow.init.MarkCell(this.X, this.Y + 1, false);
                if (MainWindow.init.Free(this.X + 1, this.Y + 1)) MainWindow.init.MarkCell(this.X + 1, this.Y + 1, false);

                // красные — где враг
                if (MainWindow.init.Foe(this.X - 1, this.Y - 1, this.Black)) MainWindow.init.MarkCell(this.X - 1, this.Y - 1, true);
                if (MainWindow.init.Foe(this.X, this.Y - 1, this.Black)) MainWindow.init.MarkCell(this.X, this.Y - 1, true);
                if (MainWindow.init.Foe(this.X + 1, this.Y - 1, this.Black)) MainWindow.init.MarkCell(this.X + 1, this.Y - 1, true);
                if (MainWindow.init.Foe(this.X - 1, this.Y, this.Black)) MainWindow.init.MarkCell(this.X - 1, this.Y, true);
                if (MainWindow.init.Foe(this.X + 1, this.Y, this.Black)) MainWindow.init.MarkCell(this.X + 1, this.Y, true);
                if (MainWindow.init.Foe(this.X - 1, this.Y + 1, this.Black)) MainWindow.init.MarkCell(this.X - 1, this.Y + 1, true);
                if (MainWindow.init.Foe(this.X, this.Y + 1, this.Black)) MainWindow.init.MarkCell(this.X, this.Y + 1, true);
                if (MainWindow.init.Foe(this.X + 1, this.Y + 1, this.Black)) MainWindow.init.MarkCell(this.X + 1, this.Y + 1, true);
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
            MainWindow.WhiteTurn = !MainWindow.WhiteTurn;
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
            MainWindow.init.ClearMarks();
            SelectKing(null, null);
        }
    }

}
