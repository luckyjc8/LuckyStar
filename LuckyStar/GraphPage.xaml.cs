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
using System.Windows.Forms;
using System.Drawing;

namespace LuckyStar
{
    /// <summary>
    /// Interaction logic for GraphPage.xaml
    /// </summary>
    public partial class GraphPage : Page
    {

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
            DrawLine(100, 100, 400, 400, blankGrid);
            this.Content = mainGrid;
        }

        private void DrawLine(double x1, double y1, double x2, double y2, Grid g)
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

        public Grid MakeGrid(Grid g, double r, int max)
        //g = gridnya, r = diameter circles, max = jml circles
        {
            g.ShowGridLines = false;
            g.MaxHeight = max * r;
            g.MaxWidth = max * r; 

            for (int i = 0; i < max; i++)
            {
                ColumnDefinition x1 = new ColumnDefinition();
                g.ColumnDefinitions.Add(x1);
            }
            for (int i = 0; i < max; i++)
            {
                RowDefinition y1 = new RowDefinition();
                g.RowDefinitions.Add(y1);
            }

            return g;
        }

        public void MakeCircles(Grid g, double r, int max, double x, double y)
        //g = gridnya, r = diameter circles, max = jml circles, x = koordinat x, y = koordinat y
        {
            g.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            g.VerticalAlignment = VerticalAlignment.Top;
            g.ShowGridLines = true;

            for (int i = 0; i < max; i++)
            {
                int column = max/(i+1);
                int row = i;
                DrawCircle(r, column, row, x, y, g);
            }
        }

        private void DrawCircle(double r, int column, int row, double x, double y, Grid s)
        //s = gridnya, r = diameter circles, column = column di grid, row = row di grid, x = koordinat x, y = koordinat y
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
            x = circle.Width * column - circle.Width/2;
            y = circle.Height * row - circle.Height/2;

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
