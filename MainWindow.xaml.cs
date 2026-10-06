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
        public MainWindow()
        {
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
                k.Figure.MouseDown += k.SelectFigure;
                gameBorder.Children.Add(k.Figure);
            }
        }
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

        private void SelectTile(object sender, MouseButtonEventArgs e)
        {
            Grid Tile = sender as Grid;
            int X = Grid.GetColumn(Tile);
            int Y = Grid.GetRow(Tile);

            Pawn SelectPawn = MainWindow.init.Pawns.Find(x => x.Select == true);
            if(SelectPawn != null)
                SelectPawn.Transform(X, Y);

            King SelectKing = MainWindow.init.king.Find(x => x.Select == true);
            if (SelectKing != null)
                SelectKing.Transform(X, Y);
        }

        public void OnSelect(Pawn pawn)
        {
            foreach (Pawn Pawn in Pawns)
            {
                if (Pawn != pawn)
                    if (Pawn.Select)
                        Pawn.SelectFigure(null, null);

            }
        }
        public void OnSelect(King kiing)
        {
            foreach (King k in king)
            {
                if (k != kiing)
                    if (k.Select)
                        k.SelectFigure(null, null);

            }
        }
    }
}
