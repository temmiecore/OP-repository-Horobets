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
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace Prac2
{
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        static DispatcherTimer dT;
        static int Radius = 15, Citiescount = 20, Populcount = 10, Iterations = 1000, Interval = 1000, DistanceNum;
        static double Mutachance = 0.7;
        static int[,] Chromosomes;
        static double BestDistance;

        static Polygon myPolygon = new Polygon();
        static List<Ellipse> EllipseArray = new List<Ellipse>();
        static PointCollection pC = new PointCollection();

        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; }

        public MainWindow()
        {
            dT = new DispatcherTimer();
            myPolygon.Stroke = Brushes.Black;
            myPolygon.StrokeThickness = 2;
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
               new LineSeries
               {
                   Values = new ChartValues<ObservablePoint>(),
                   Fill = Brushes.Transparent,
                   PointGeometry=null
               }
               
            };
            Formatter = value => value.ToString("N");
            DataContext = this;
        }
  


        private void InitPoints()
        {
            Random rnd = new Random();
            pC.Clear();
            EllipseArray.Clear();
            for (int i = 0; i < Citiescount; i++)
            {
                Point p = new Point();
                p.X = rnd.Next(Radius / 3, 513 - (Radius / 3));
                p.Y = rnd.Next(Radius / 3, 274 - (Radius / 3));
                pC.Add(p);
            }
            for (int i = 0; i < Citiescount; i++)
            {
                Ellipse el = new Ellipse();
                el.StrokeThickness = 2;
                el.Height = el.Width = Radius;
                el.Stroke = Brushes.Black;
                if (i == 0)
                    el.Fill = Brushes.DarkRed;
                else
                   el.Fill = Brushes.LightBlue;
                EllipseArray.Add(el);
            }
        }
        private void DrawPoints()
        {
            for (int i = 0; i < Citiescount; i++)
            {
                Canvas.SetLeft(EllipseArray[i], pC[i].X-Radius/2.5);
                Canvas.SetTop(EllipseArray[i], pC[i].Y-Radius/2.5);
                MyCanvas.Children.Add(EllipseArray[i]);
            }
        }
        private void DrawWay(int[] BestWayIndex)
        {
            PointCollection Points = new PointCollection();
            for (int i = 0; i < BestWayIndex.Length; i++)
                Points.Add(pC[BestWayIndex[i]]);
            myPolygon.Points = Points;
            MyCanvas.Children.Add(myPolygon);
        }

        private void Genbtn_Click(object sender, RoutedEventArgs e)
        {

            if (CitiesT.Text != "")
                Citiescount = int.Parse(CitiesT.Text);
            if (PopuloT.Text != "")
                Populcount = int.Parse(PopuloT.Text);
            if (MutaT.Text != "")
                Mutachance = double.Parse(MutaT.Text);
            if (IterT.Text != "")
                Iterations = int.Parse(IterT.Text);
            if (IntervalT.Text != "")
                Interval = int.Parse(IntervalT.Text);

            MyCanvas.Children.Clear();
            SeriesCollection[0].Values.Clear();
            InitPoints();
            DrawPoints();

            List<int> Chromosome = new List<int>();
            Chromosomes = new int[Populcount, Citiescount];
            for (int i = 0; i < Populcount; i++)
            {
                Chromosome.Clear();
                for (int j = 0; j < Citiescount; j++)
                {
                    int Num = rand.Next(0, Citiescount);
                    while (Chromosome.Contains(Num))
                        Num = rand.Next(0, Citiescount);
                    Chromosome.Add(Num);
                    Chromosomes[i, j] = Num;

                }
            }
            dT.Interval = new TimeSpan(0, 0, 0, 0, Interval);
            dT.Tick += new EventHandler(OneStep);
            dT.Start();
        }
        private void OneStep(object sender, EventArgs e)
        {
            MyCanvas.Children.Clear();
            Chromosomes = BestWay(Chromosomes);
            DrawPoints();
            int[] BestWayCities = new int[Citiescount];
            for (int i = 0; i < Citiescount; i++)
                BestWayCities[i] = Chromosomes[0, i];
            DrawWay(BestWayCities);
            SeriesCollection[0].Values.Add(new ObservablePoint(x:DistanceNum, y:BestDistance));
            DistanceNum++;
        }

        private void Greedbtn_Click(object sender, RoutedEventArgs e)
        {
            dT.Stop();
            if (CitiesT.Text != "")
                Citiescount = int.Parse(CitiesT.Text);

            MyCanvas.Children.Clear();
            InitPoints();
            DrawPoints();

            int[] GreedWay = new int[Citiescount+1];
            List<int> UsedCities = new List<int>();
            int j = 0; string str = "";
            for (int i = 0; i <= Citiescount; i++)
            {
                GreedWay[i] = j;
                UsedCities.Add(j);
                double D = 0; double min = int.MaxValue; int buff = 0;
                for (int k = 0; k < Citiescount; k++)
                {
                    if (k != j && !UsedCities.Contains(k))
                    {
                        D = Math.Sqrt(Math.Pow((pC[j].X - pC[k].X), 2)
                                  + Math.Pow((pC[j].Y - pC[k].Y), 2));
                        if (D < min)
                        { min = D; buff = k; }
                    }
                }
                str += j + " ";
                j = buff;
            }
            DrawWay(GreedWay);
        }

        private void Stopbtn_Click(object sender, RoutedEventArgs e)
        {
            if (dT.IsEnabled == true)
                dT.Stop();
            else
                dT.Start();
        }

        private void Exitbtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            Close();
        }

        private int[,] BestWay(int[,] Chromosomes)
        {
            int[,] ParentsChildren = new int[2 * Populcount, Citiescount];
            ParentsChildren = NewGeneration(Chromosomes);
            Dictionary<int, double> IndexDistance = new Dictionary<int, double>();
            for (int i = 0; i < 2 * Populcount; i++)
            {
                double D = 0;
                for (int j = 0; j < Citiescount - 1; j++)
                {
                    D += Math.Sqrt(Math.Pow((pC[ParentsChildren[i, j]].X - pC[ParentsChildren[i, j + 1]].X), 2)
                                + Math.Pow((pC[ParentsChildren[i, j]].Y - pC[ParentsChildren[i, j + 1]].Y), 2));
                }
                D += Math.Sqrt(Math.Pow((pC[ParentsChildren[i, 0]].X - pC[ParentsChildren[i, Citiescount - 1]].X), 2)
                                + Math.Pow((pC[ParentsChildren[i, 0]].Y - pC[ParentsChildren[i, Citiescount - 1]].Y), 2));
                IndexDistance.Add(i, D);
            }
            var SortedID = (from entry in IndexDistance orderby entry.Value ascending select entry).Take(Populcount);
            int[,] NewChromosomes = new int[Populcount, Citiescount];
            int count = 0;
            var first = SortedID.First(); BestDistance = first.Value;

            foreach (KeyValuePair<int, double> a in SortedID)
            {
                for (int j = 0; j < Citiescount; j++)
                    NewChromosomes[count, j] = ParentsChildren[a.Key, j];
                count++;
            }
            return NewChromosomes;
        }

        private int[,] NewGeneration(int[,] Parents)
        {
            int[,] ParentsChildren = new int[2 * Populcount, Citiescount];
            for (int i = 0; i < Populcount; i++)
            {
                int COpoint = rand.Next(1, Citiescount);

                int[] temp1 = new int[Citiescount], temp2 = new int[Citiescount], parent1 = new int[Citiescount],
                    parent2 = new int[Citiescount], child = new int[Citiescount];
                string str = "";
                for (int j = 0; j < Citiescount; j++)
                {
                    ParentsChildren[i, j] = Parents[i, j];
                    parent1[j] = Parents[i % Populcount, j];
                    parent2[j] = Parents[(i + 1) % Populcount, j];
                }
                foreach (int a in parent1)
                {
                    str += a + " ";

                }
                str = "";
                foreach (int a in parent2)
                {
                    str += a + " ";

                }
                temp1 = CrossOver(parent1, parent2, COpoint); temp2 = CrossOver(parent2, parent1, COpoint);
                str = "";
                foreach (int a in temp1)
                {
                    str += a + " ";

                }
                str = "";
                foreach (int a in temp2)
                {
                    str += a + " ";

                }
                if (rand.NextDouble() >= 0.5)
                    child = temp1.Union(temp2).ToArray();
                else
                    child = temp2.Union(temp1).ToArray();
                str = "";
                foreach (int a in child)
                {
                    str += a + " ";

                }
                if (rand.NextDouble() <= Mutachance)
                    child = Mutation(child);
                for (int j = 0; j < Citiescount; j++)
                {
                    ParentsChildren[i + Populcount, j] = child[j];
                }
            }
            return ParentsChildren;
        }

        private int[] CrossOver(int[] parentX, int[] parentY, int point)
        {
            int[] result = new int[Citiescount];
            for (int i = 0; i < Citiescount; i++)
            {
                if (i <= point)
                    result[i] = parentX[i];
                else
                    result[i] = parentY[i];
            }
            return result;
        }

        private int[] Mutation(int[] child)
        {
            int i1 = rand.Next(1, Citiescount), i2 = rand.Next(1, Citiescount);
            while (i1 >= i2)
            { i1 = rand.Next(1, Citiescount); i2 = rand.Next(1, Citiescount); }
            while (i1 < i2)
            {
                int temp = child[i1]; child[i1] = child[i2]; child[i2] = temp;
                i1++; i2--;
            }
            return child;
        }
    }
}
