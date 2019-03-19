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
        public string radio;
        public double[] arr;
        public LinkedList<int>[] paths;
        public int houses;

        public GraphPage(string fileName)
        {
            InitializeComponent();
            paths = Backend.ReadMap(fileName);
            houses = paths[0].First.Value;
            bool[] visited = new bool[houses+1];
            for(int i=0;i< houses + 1; i++)
            {
                visited[i] = false;
            }

            string asdf = "5-2-3-4"; //INI BUAT INPUTNYA
            string[] color = ColorGraf(asdf);
            MakeGraf(visited, color);

            //TESTING
            List<string> items = new List<string> { };
            items.Add("PERTANYAAN :");
            items.Add("qwer");
            items.Add("  ");
            items.Add("JAWABAN :");
            items.Add("asdf");
            items.Add("   ");
            items.Add("ENUMERASI LANGKAH :");
            items.Add("zxcv");
            list1.ItemsSource = items;
        }

        void MakeGraf(bool[] visited, string[] color)
        {
            int level = 0;
            arr = new double[1000001];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 20;
            }
            y = 20;
            DrawGraph(1, paths, visited, level, 20, 20, color);
            canvas1.Width = (houses + 1) * 100;
            canvas1.Height = (houses + 1) * 100;
            this.Content = mainGrid;
        }

        string[] ColorGraf(string path)
        {
            string[] jalur = path.Split('-');
            for (int i = 0; i < jalur.Length; i++)
            {
                jalur[i] = "c" + jalur[i];
            }
            return jalur;
        }

        void DrawGraph(int i, LinkedList<int>[] paths, bool[] visited, int level,double curr_x,double curr_y,string[] color)
        {
            visited[i] = true;
            DrawCircle(arr[level], y, canvas1, i, color);
            arr[level] += 100;
            y += 100;
            level++;
           
            
            foreach (int p in paths[i])
            {
                int new_p = p > 0 ? p : -p;
                if (!visited[new_p])
                {
                    x1 = curr_x + 25; y1 = curr_y + 25;
                    x2 = arr[level] + 25;
                    y2 = y + 25;
                    DrawLine(canvas1);
                    DrawGraph(new_p, paths, visited, level,arr[level],y,color);
                }
            }
            y -= 100;
            level--;
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

        bool IsElemen(string[] arr, string a)
        {
            bool found = false;
            int i = 0;
            while ((i < arr.Length) && (!found))
            {
                if (arr[i] == a)
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            return found;
        }

        private void DrawCircle(double x, double y,Canvas c,int no, string[] color)
        {
            Ellipse circle = new Ellipse();
            circle.StrokeThickness = 3;
            circle.Stroke = System.Windows.Media.Brushes.Black;
            circle.Width = 50;
            circle.Height = 50;
            string name = "c" + no.ToString();
            circle.Name = name;
            if (IsElemen(color, name))
            {
                circle.Fill = System.Windows.Media.Brushes.Red;
            }
            else
            {
                circle.Fill = System.Windows.Media.Brushes.Black;
            }

            TextBlock txt = new TextBlock();
            txt.Text = no.ToString();
            txt.Foreground = Brushes.White;
            txt.FontSize = 20;

            Canvas.SetTop(circle, y);
            Canvas.SetLeft(circle, x);
            Canvas.SetZIndex(circle, 5);
            Canvas.SetTop(txt, y+10);
            Canvas.SetLeft(txt, x+10);
            Canvas.SetZIndex(txt, 10);

            c.Children.Add(txt);
            c.Children.Add(circle);
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            radio = rb.Name;
        }

        private void SubmitButton(object sender, RoutedEventArgs e)
        {
            //GraphPage graphPage = new GraphPage(namaFile.Text);
            //NavigationService.Navigate(graphPage);
            if (radio == "eksternal")
            {
                //proses file eksternal
                Console.WriteLine(namaFile);
            }
            else // radio == "langsung"
            {
                //proses input biasa
                string[] exploded = pertanyaan.Text.Split(' ');
                int a = Int32.Parse(exploded[0]);
                int b = Int32.Parse(exploded[1]);
                int c = Int32.Parse(exploded[2]);
                List<string> enumeration = new List<string>();
                bool answers = Backend.Solve(a, b, c, paths, "", enumeration);
                Console.WriteLine(answers); //ini answer bools nya
                foreach(string ans in enumeration)
                {
                    Console.WriteLine("path : "+ans+", split : ");
                    foreach(string node in ans.Split('-'))
                    {
                        Console.WriteLine(node);
                    }

                }
            }
        }
    }
}
