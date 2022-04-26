using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    internal class Newton
    {
        private double accuracy;
        private int numbers = 5;
        private void FindNumbers()  //метод поиска количества знаков для округления
        {
            numbers = 0;
            if (accuracy < 1 && accuracy > 0)
            {
                char[] chars = accuracy.ToString("##.############").ToCharArray();
                foreach (var item in chars)
                {
                    if (item == '0' || item == ',')
                    {
                        numbers++;
                    }
                }
            }
        }
        private double Function1(double x, double y)
        {
            return (x / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 0.5)) + 0.5;
        }
        private double Function2(double x, double y)
        {
            return (y / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 0.5)) - 0.5;
        }
        public void Work()
        {
            Console.WriteLine("Введите погрешность");
            accuracy = Convert.ToDouble(Console.ReadLine());
            FindNumbers();
            Console.WriteLine("Введите a");
            double a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите x0");
            double x0 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введите x1");
            double x1 = Convert.ToDouble(Console.ReadLine());
            double difference = 1;
            int count = 0;
            while (difference > accuracy)
            {
                double x = x0 - Function1(x0, x1) * a;
                double y = x1 - Function2(x0, x1) * a;
                difference = Math.Max(Math.Abs(x - x0), Math.Abs(y - x1));
                x0 = x;
                x1 = y;
                Console.WriteLine("x1 : " + Math.Round(x, numbers));
                Console.WriteLine("x2 : " + Math.Round(y, numbers));
                count++;
            }
            Console.WriteLine("Количество итераций: " + count);
        }
    }
}
