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
using System.Drawing;

namespace LuckyStar
{
    /// <summary>
    /// Interaction logic for GraphPage.xaml
    /// </summary>
    
    public partial class GraphPage : Page
    {
        public double x1, y1, x2, y2, x, y; //global variabel buat garis

        public GraphPage(string fileName) : this()
        {
            //Backend backend;
            InitializeComponent();
            //LinkedList<int> paths = backend.readMap(fileName);
        }

        public GraphPage()
        {
            InitializeComponent();

            double r = 50;
            int max = 2;
            
            MakeGrid(myGrid, r, max); //bikin column & row definition
            MakeGraf(r); //bikin draw testing
            this.Content = mainGrid; //nambahin ke konten
        }

        public void MakeGraf(double r) //INI TESTING, Tapi fungsi ini nanti bakal dipake
        {
            DrawCircle(r, 1, 1, true, myGrid);
            DrawCircle(r, 2, 2, false, myGrid);
            DrawLine(blankGrid);
        }

        private void DrawLine(Grid g) //buat bikin garis
        {
            Line l1 = new Line();
            l1.X1 = x1;
            l1.Y1 = y1;
            l1.X2 = x2;
            l1.Y2 = y2;
            l1.Stroke = System.Windows.Media.Brushes.Black;
            l1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            l1.VerticalAlignment = VerticalAlignment.Center;
            l1.StrokeThickness = 2;

            g.Children.Add(l1);
        }

        public Grid MakeGrid(Grid g, double r, int max) //ini buat bikin grid2nya (kotak buat taro circle)
        //g = gridnya, r = diameter circles, max = jml circles
        {
            g.ShowGridLines = false;
            g.MaxHeight = max * r;
            g.MaxWidth = max * r; 

            for (int i = 0; i < max; i++) //bagi grid jadi column2
            {
                ColumnDefinition x1 = new ColumnDefinition();
                g.ColumnDefinitions.Add(x1);
            }
            for (int i = 0; i < max; i++) //bagi grid jadi row2
            {
                RowDefinition y1 = new RowDefinition();
                g.RowDefinitions.Add(y1);
            }

            return g;
        }

        public void MakeCircles(Grid g, double r, int max)
        //INI BUAT TESTING KALO MAU BIKIN BYK CIRCLE
        //g = gridnya, r = diameter circles, max = jml circles
        {
            g.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            g.VerticalAlignment = VerticalAlignment.Top;
            g.ShowGridLines = true;

            for (int i = 0; i < max; i++)
            {
                int column = max/(i+1);
                int row = i;
                DrawCircle(r, column, row, true, g);
            }
        }

        private void DrawCircle(double r, int column, int row, bool asal, Grid s)
        //INI BUAT BIKIN 1 CIRCLE
        //s = gridnya, r = diameter circles, column = column di grid, row = row di grid, 
        //asal = if bapak then true else if anak then false
        {
            Ellipse circle = new Ellipse();
            SolidColorBrush b1 = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0,0,0,0));
            circle.StrokeThickness = 3;
            circle.Stroke = System.Windows.Media.Brushes.Black;
            circle.Fill = b1;

            circle.Width = r;
            circle.Height = r;

            Grid.SetColumn(circle, column);
            Grid.SetRow(circle, row);

            if (asal) //nentuin ini anak atau bapak, buat ganti x1 x2 dll
            {
                x1 = circle.Width * column - circle.Width / 2;
                y1 = circle.Height * row - circle.Height / 2;
            }
            else
            {
                x2 = circle.Width * column - circle.Width / 2;
                y2 = circle.Height * row - circle.Height / 2;
            }

            s.Children.Add(circle);
        }

        private void handleCheck(object sender, RoutedEventArgs e)
        {
            //RadioButton rb = sender as RadioButton;
            //return rb.Name;
        }

        private void submitButton(object sender, RoutedEventArgs e)
        {
            /*
            //GraphPage graphPage = new GraphPage(namaFile.Text);
            //NavigationService.Navigate(graphPage);
            string choice = HandleCheck(sender, e);
            if (choice == "eksternal")
            {
                //proses file eksternal
                Console.WriteLine(choice);
            }
            else
            {
                //proses input biasa
                Console.WriteLine(choice);
            }
            */
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
