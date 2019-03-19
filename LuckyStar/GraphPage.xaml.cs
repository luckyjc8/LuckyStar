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
        public double[] arr;
        public GraphPage(string fileName)
        {
            InitializeComponent();
            LinkedList<int>[] paths = Backend.ReadMap(fileName);
            bool[] visited = new bool[paths[0].First.Value+1];
            for(int i=0;i< paths[0].First.Value + 1; i++)
            {
                visited[i] = false;
            }

            int level = 0;
            arr = new double[1000001];
            for (int i = 0; i<arr.Length; i++)
            {
                arr[i] = 20;
            }
            y = 20;
            DrawGraph(1, paths, visited, level);
        }

        void DrawGraph(int i, LinkedList<int>[] paths, bool[] visited, int level)
        {
            visited[i] = true;
            DrawCircle(arr[level], y, canvas1, i);
            arr[level] += 100;
            y += 100;
            level++;
            foreach(int p in paths[i])
            {
                if (!visited[p])
                {
                    DrawGraph(p, paths, visited, level);
                }
            }
            y -= 100;
            level--;
        }

        void drawingGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try { canvas1.Height = y; } catch { }
            try { canvas1.Width = x; } catch { }
        }


        private void DrawLine(Canvas g) //buat bikin garis
        {
            Line l1 = new Line();
            l1.X1 = x1; l1.Y1 = y1; l1.X2 = x2; l1.Y2 = y2;
            l1.Stroke = System.Windows.Media.Brushes.Black;
            l1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            l1.VerticalAlignment = VerticalAlignment.Center;
            l1.StrokeThickness = 2;
            g.Children.Add(l1);
        }


        private void DrawCircle(double x, double y,Canvas c,int no)
        {
            Ellipse circle = new Ellipse();
            circle.StrokeThickness = 3;
            circle.Stroke = System.Windows.Media.Brushes.Black;
            circle.Fill = System.Windows.Media.Brushes.Black;
            circle.Width = 50;
            circle.Height = 50;

            TextBlock txt = new TextBlock();
            txt.Text = no.ToString();
            txt.Foreground = Brushes.White;
            txt.FontSize = 20;

            Canvas.SetTop(circle, y);
            Canvas.SetLeft(circle, x);
            Canvas.SetTop(txt, y+10);
            Canvas.SetLeft(txt, x+10);
            Canvas.SetZIndex(txt, 10);

            c.Children.Add(txt);
            c.Children.Add(circle);
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            //RadioButton rb = sender as RadioButton;
            //return rb.Name;
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
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
    }
}
