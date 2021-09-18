using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace sb_task_4_3
{
    class Program
    {
        static int[][] result;
        static int[][] A;
        static int[][] B;
        static int x_rows = 1000;
        static int x_columns = 2000;
        static int y_rows = 2000;
        static int y_columns = 3000;


        static void Main(string[] args)
        {
            A = CreateJaggedArray(x_rows, x_columns);
            Fill(A);
            B = CreateJaggedArray(y_rows, y_columns);
            Fill(B);
            result = CreateJaggedArray(x_rows, y_columns);

            Console.WriteLine($"\n\nПриступаем к умножению матрицы А размера {x_rows} на {x_columns} на матрицу B размера {y_rows} на {y_columns}:");

            if (x_columns == y_rows)
            {
                Console.WriteLine("Матрицы подходят по размеру. Продолжаем умножение...");
            }
            else
            {
                Console.WriteLine("Данные матрицы нельзя перемножить");
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            
            Parallel.For(0, x_rows, MultiplyJagged);

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine($"Умножение завершено. Заняло времени: {elapsedTime}");

            Console.ReadLine();
        }

     
        public static void MultiplyJagged(int t)
        {
            for (int j = 0; j < y_columns; j++)
            {
                for (int k = 0; k < x_columns; k++)
                {
                    result[t][j] += A[t][k] * B[k][j];
                }
            }
        }


        public static void Fill(int[][] y)
        {
            Random r = new Random();
            for (int i = 0; i < y.Length; i++)
            {
                for (int j = 0; j < y[i].Length; j++)
                {
                    y[i][j] = r.Next(-10, 10);
                }
            }
        }

        public static int[][] CreateJaggedArray(int m, int n)
        {
            int[][] res = new int[m][];
            for (int i = 0; i < m; i++)
            {
                res[i] = new int[n];
            }
            return res;
        }
    }
}