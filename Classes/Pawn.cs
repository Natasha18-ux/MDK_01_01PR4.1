using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

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
            bool atack = false;
            Pawn SelectPawn = MainWindow.init.Pawns.Find(x => x.Select == true);
            if(SelectPawn != null)
            {

            }
        }
    }
}
