using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    internal class Lab6
    {
        private int numberRound;
        private double[,] HesseMatrix;
        private double func(double x, double y)
        {
            return Math.Pow(Math.Pow(x, 2) + Math.Pow(y, 2) + 1, 0.5) + 0.5 * x - 0.5 * y;
        }
        private double d1x(double x, double y)
        {
            return x / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 0.5) + 0.5;
        }
        private double d1y(double x, double y)
        {
            return y / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 0.5) - 0.5;
        }
        private double d2x(double x, double y)
        {
            return -(Math.Pow(x, 2) / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 1.5)) + (1 / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 0.5));
        }
        private double d2y(double x, double y)
        {
            return -(Math.Pow(y, 2) / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 1.5)) + (1 / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 0.5));
        }
        private double d2xy(double x, double y)
        {
            return -((x * y) / Math.Pow(1 + Math.Pow(x, 2) + Math.Pow(y, 2), 1.5));
        }
        private double FindLength(double num1, double num2)
        {
            return Math.Pow(Math.Pow(num1, 2) + Math.Pow(num2, 2), 0.5);
        }
        private double[] FindGradient(double x, double y)
        {
            return new double[] { d1x(x, y), d1y(x, y) };
        }
        private double[,] GetHesseMatrix(double x, double y)
        {
            double[,] matrix = new double[2,2];
            matrix[0,0] = d2x(x, y);
            matrix[0,1] = d2xy(x, y);
            matrix[1,0] = d2xy(x, y);
            matrix[1,1] = d2y(x, y);
            return matrix;
        }
        private double[,] GetTransposeMatrix(double[,] matrix) //квадратной
        {
            double[,] answer = new double[matrix.GetLength(0), matrix.GetLength(0)];
            for (int i=0; i< matrix.GetLength(0); i++)
            {
                for (int j=0; j< matrix.GetLength(0); j++)
                {
                    answer[j,i] = matrix[i,j];
                }
            }
            return answer;
        }
        private double[,] GetReverseMatrix(double[,] matrix)
        {
            double determinant = GetDeterminant(matrix);
            double[,] _matrix = GetTransposeMatrix(matrix);
            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(0); j++)
                {
                    _matrix[i, j] = _matrix[i, j] * Math.Pow(-1,i+j) / determinant;
                }
            }
            return matrix;
        }
        private double GetDeterminant(double[,] matrix)
        {
            return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        }
        private double[] CompositionMatrix(double[,] matrix1, double[] matrix2)
        {
            double[] answer = new double[matrix1.GetLength(0)]; 
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int k = 0; k < matrix2.Length; k++)
                {
                    double c = matrix1[i, k] * matrix2[k];
                    answer[i] += c;
                }
            }
            return answer;
        }
        private double NewtonProcess(double x, double y, double errorRate)
        {
            double _x = x;
            double _y = y;
            
            while (true)
            {
                Console.WriteLine(_x.ToString()); 
                Console.WriteLine(_y.ToString()); 
                double[] gradient = FindGradient(_x, _y);
                if (FindLength(gradient[0], gradient[1]) < errorRate)
                {
                    Console.WriteLine(_x.ToString(), _y.ToString());
                    return 0;// заглушка верного ответа
                }
                double[,] matrix = GetHesseMatrix(_x, _y);
                matrix = GetReverseMatrix(matrix);
                if (matrix[0, 0] < 0 && GetDeterminant(matrix) < 0)
                {
                    Console.WriteLine("Ошибка");
                    return 0; // заглушка ошибки
                }
                double[] d = CompositionMatrix(matrix, gradient);
                _x -= d[0];
                _y -= d[1];
            }
        }
       
        private void Fletcher(double x, double y, double errorRate)
        {
            double _x = x;
            double _y = y;
            double[] gradient = FindGradient(_x, _y);
            int iter = 0;
            while (true)
            {
                if (FindLength(gradient[0], gradient[1]) < errorRate)
                {
                    Console.WriteLine(_x.ToString(), _y.ToString());
                    return;// заглушка верного ответа
                }
                if (iter == 0)
                {
                    double[] d = new double[] {- gradient[0], - gradient[1]} ;
                }
                else
                {

                }

            }
            

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
        public void Newton()
        {
            Console.WriteLine("Введите начальный x");
            double x = Double.Parse(Console.ReadLine());
            Console.WriteLine("Введите начальный y");
            double y = Double.Parse(Console.ReadLine());
            Console.WriteLine("Введите допустимую погрешность");
            double errorRate = Double.Parse(Console.ReadLine());
            NewtonProcess(x, y, errorRate);
        }


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

        private void Uniform(double x, double y, double error)
        {
            double startPoint = x;
            double endPoint = y;
            int count = 100;
            double errorRate = error;
            FindNumbers(errorRate);
            double minimumPoint = 0;
            double currentError = double.MaxValue;
            int numberIteration = 1;
            double valueOfMinimun = 0;
            while (currentError > errorRate)
            {
                double[] result = UniformIteration(startPoint, endPoint, count);
                startPoint = result[0];
                minimumPoint = result[1];
                endPoint = result[2];
                currentError = result[3];
                valueOfMinimun = result[4];
                numberIteration++;
            }
        }
    }
}
