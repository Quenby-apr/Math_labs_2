using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    
    public class Lab5
    {
        private int numberRound;
        private double[] UniformIteration(double a, double b, int N)
        {
            double[] x = new double[N + 1];
            double[] y = new double[N + 1];
            double min_y = double.MaxValue;
            int index = -1;
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = a + i * (b - a) / (N + 1);
            }
            for (int i = 0; i < y.Length; i++)
            {
                y[i] = x[i] * x[i] + 2 * Math.Exp(-0.65 * x[i]);
                if (y[i] < min_y)
                {
                    min_y = y[i];
                    index = i;
                }
                Console.WriteLine("x = " + Math.Round(x[i], numberRound) + "; f(x) = " + Math.Round(y[i], numberRound));
            }
            double[] answer = { x[index - 1], x[index], x[index + 1], Math.Max(Math.Abs(y[index - 1] - y[index]), Math.Abs(y[index] - y[index + 1])), y[index] };
            return answer;
        }

        private void Uniform()
        {
            List<string> startParams = UniformParams();
            double startPoint = Convert.ToDouble(startParams[0]);
            double endPoint = Convert.ToDouble(startParams[1]);
            int count = Convert.ToInt32(startParams[2]);
            double errorRate = Convert.ToDouble(startParams[3]);
            FindNumbers(errorRate);
            double minimumPoint = 0;
            double currentError = double.MaxValue;
            int numberIteration = 1;
            double valueOfMinimun = 0;
            while (currentError > errorRate)
            {
                Console.WriteLine("Значения " + numberIteration + " итерации:");
                double[] result = UniformIteration(startPoint, endPoint, count);
                startPoint = result[0];
                minimumPoint = result[1];
                endPoint = result[2];
                currentError = result[3];
                valueOfMinimun = result[4];
                numberIteration++;
            }
            Console.WriteLine("Ответ: x = " + Math.Round(minimumPoint,numberRound) + ", f(x) = " + Math.Round(valueOfMinimun, numberRound) + ", номер итерации: " + (numberIteration-1));
        }

        private void Newton()
        {
            List<string> startParams = NewtonParams();
            double currentPoint = Convert.ToDouble(startParams[0]);
            double errorRate = Convert.ToDouble(startParams[1]);
            FindNumbers(errorRate);
            double currentError = double.MaxValue;
            int numberIteration = 1;
            while (currentError > errorRate)
            {
                double firstDeriative = 2*currentPoint - 1.3 * Math.Exp(-0.65 * currentPoint);
                double secondDeriative = 2 + 0.845 * Math.Exp(-0.65 * currentPoint);
                double previousPoint = currentPoint;
                currentPoint = previousPoint - firstDeriative / secondDeriative;
                double valueOfMinimum = currentPoint * currentPoint + 2 * Math.Exp(-0.65 * currentPoint);
                currentError = Math.Abs(currentPoint - previousPoint);
                Console.WriteLine("Ответ: x = " + Math.Round(currentPoint, numberRound) + ", f(x) = " + Math.Round(valueOfMinimum, numberRound) + ", номер итерации: " + (numberIteration));
                numberIteration++;
            }
        }

        private void GoldenRatio()
        {
            List<string> startParams = GoldenParams();
            double startPoint = Convert.ToDouble(startParams[0]);
            double endPoint = Convert.ToDouble(startParams[1]);
            double errorRate = Convert.ToDouble(startParams[2]);
            FindNumbers(errorRate);
            double currentY = startPoint + (3-Math.Sqrt(5))/2*(endPoint-startPoint);
            double currentZ = startPoint + endPoint - currentY;

            double currentError = double.MaxValue;
            int numberIteration = 1;
            while (currentError > errorRate)
            {
                double valueY = currentY * currentY + 2 * Math.Exp(-0.65 * currentY);
                double valueZ = currentZ * currentZ + 2 * Math.Exp(-0.65 * currentZ);
                if (valueY <= valueZ)
                {
                    endPoint = currentZ;
                    double previousY = currentY;
                    currentY = startPoint + endPoint - previousY;
                    currentZ = previousY;
                }
                if (valueY > valueZ)
                {
                    startPoint = currentY;
                    double previousZ = currentZ;
                    currentY = previousZ;
                    currentZ = startPoint + endPoint - previousZ;
                }
                Console.WriteLine("Текущий x равен: " + Math.Round(((startPoint + endPoint) / 2), numberRound+1));
                currentError = Math.Abs(startPoint-endPoint);
                numberIteration++;
            }
            double x = ((startPoint + endPoint) / 2);
            double y = x * x + 2 * Math.Exp(-0.65 * x);
            Console.WriteLine("Ответ: x = " + Math.Round(x,numberRound) + ", f(x) = " + Math.Round(y, numberRound) + ", количество итераций: " + (numberIteration - 1));
        }

        public void ChooseMethod()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите метод:");
                Console.WriteLine("1 - равномерный поиск");
                Console.WriteLine("2 - метод Ньютона");
                Console.WriteLine("3 - метод золотого сечения");
                string method = Console.ReadLine();
                switch (method)
                {
                    case "1":
                        Uniform();
                        break;
                    case "2":
                        Newton();
                        break;
                    case "3":
                        GoldenRatio();
                        break;
                }
            }
        }
        private List<string> UniformParams()
        {
            Console.WriteLine("Введите начало интервала");
            string startPont = Console.ReadLine();
            Console.WriteLine("Введите конец интервала");
            string endPoint = Console.ReadLine();
            Console.WriteLine("Введите количество вычислений функции");
            string count = Console.ReadLine();
            Console.WriteLine("Введите допустимую погрешность");
            string errorRate = Console.ReadLine();
            return new List<string> { startPont, endPoint, count, errorRate };
        }

        private List<string> NewtonParams()
        {
            Console.WriteLine("Введите начальную точку");
            string startPont = Console.ReadLine();
            Console.WriteLine("Введите допустимую погрешность");
            string errorRate = Console.ReadLine();
            return new List<string> { startPont,errorRate };
        }

        private List<string> GoldenParams()
        {
            Console.WriteLine("Введите начало интервала");
            string startPont = Console.ReadLine();
            Console.WriteLine("Введите конец интервала");
            string endPoint = Console.ReadLine();
            Console.WriteLine("Введите допустимую погрешность");
            string errorRate = Console.ReadLine();
            return new List<string> { startPont, endPoint, errorRate };
        }

        private void FindNumbers(double accuracy)  //метод поиска количества знаков для округления
        {
            numberRound = 0;
            if (accuracy < 1 && accuracy > 0)
            {
                char[] chars = accuracy.ToString("##.############").ToCharArray();
                foreach (var item in chars)
                {
                    if (item == '0' || item == ',')
                    {
                        numberRound++;
                    }
                }
            }
        }
    }
}
