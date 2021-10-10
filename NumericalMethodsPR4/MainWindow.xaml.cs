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


namespace NumericalMethodsPR4
{


    public partial class MainWindow : Window
    {
        public double x = 0;
        public double c = 0;
        public double fFromC = 0;
        public double e = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnHord_Click(object sender, RoutedEventArgs e)
        {
            MethodHord();
        }

        private void btnKasat_Click(object sender, RoutedEventArgs e)
        {
            MethodKasat();
        }
        private void btnKomb_Click(object sender, RoutedEventArgs e)
        {
            MethodKomb();
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            GetData();
        }
        public void GetData()
        {
            double a = double.Parse(Convert.ToString(tbxInterval.Text).Split()[0]);
            double b = double.Parse(Convert.ToString(tbxInterval.Text).Split()[1]);
            e = double.Parse(Convert.ToString(tbxE.Text));
            double h = (b - a) / 100;

            double fFromA = F(a);
            double fFromAMulTwoH = F(a + 2 * h);
            double fFromAMulH = F(a + h);

            double d = fFromAMulTwoH - (2 * fFromAMulH) + fFromA;

            x = a;
            c = b;
            if (fFromA * d < 0)
            {
                x = b;
                c = a;
            }

            fFromC = F(c);
        }

        public void MethodHord()
        {
            double x1 = (x * fFromC - c * F(x)) / (fFromC - F(x));
            int i = 1;
            while (Math.Abs(x1 - x) > e)
            {
                x = x1;
                x1 = (x * fFromC - c * F(x)) / (fFromC - F(x));
                i++;
            }
            tbHord.Text = $"{Math.Round(x1, 6)} за {i} итераций";
        }

        public void MethodKasat()
        {
            double x = c;
            double x1 = x - F(x) / FHatch(x);
            int i = 1;
            while (Math.Abs(x1 - x) > e)
            {
                x = x1;
                x1 = x - F(x) / FHatch(x);
                i++;
            }
            tbKasat.Text = $"{Math.Round(x1, 6)} за {i} итераций";
        }

        public void MethodKomb()
        {
            double x0 = x;
            double x1 = c;
            double x2 = (x * fFromC - c * F(x)) / (fFromC - F(x));
            double x3 = x - F(x) / FHatch(x);
            int i = 2;
            while (Math.Abs(x3 - x2) > e)
            {
                x0 = x2;
                x1 = x3;
                x2 = (x * fFromC - c * F(x)) / (fFromC - F(x));
                x3 = x - F(x) / FHatch(x);
                i += 2;
            }
            tbKomb.Text = $"{Math.Round(x3, 6)} за {i} итераций";
        }

        public double F(double x)
        {
            return (1.2 * Math.Pow(x, 4)) + (2 * Math.Pow(x, 3)) - (13 * Math.Pow(x, 2)) - (14.2 * x) - 24.1;
        }

        public double FHatch(double x)
        {
            return 4.8 * Math.Pow(x, 3) + 6 * Math.Pow(x, 2) - 26 * x - 14.2;
        }

        private void TextSizeChanger(object sender, SizeChangedEventArgs e)
        {
            Size n = e.NewSize;
            Size p = e.PreviousSize;
            double l = n.Width / p.Width;
            if (l != double.PositiveInfinity)
            {
                if (sender is TextBox)
                {
                    (sender as TextBox).FontSize *= l;
                }
                else if (sender is TextBlock)
                {
                    (sender as TextBlock).FontSize *= l;
                }
                else if (sender is Button)
                {
                    (sender as Button).FontSize *= l;
                }
                else if (sender is DatePicker)
                    (sender as DatePicker).FontSize *= l;
            }
        }
    }
}
