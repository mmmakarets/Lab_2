using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    interface IAlgebra
    {
        int n { get; set; }
        double[,] matrix { get; set; }
        void Set();
        void Print();
    }

    class Matrix : IAlgebra
    {
        public int n { get; set; }
        public double[,] matrix { get; set; }

        public Matrix(int n)
        {
            this.n = n;
            matrix = new double[n, n];
        }

        public void Set()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = random.Next(1, 30);
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{matrix[i, j]}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    class Vector : IAlgebra
    {
        public int n { get; set; }
        public double[,] matrix { get; set; }

        public Vector(int n)
        {
            this.n = n;
            matrix = new double[n, 1];
        }

        public void Set()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < n; i++)
            {
                matrix[i, 0] = random.Next(1, 30);
            }
        }

        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                if(matrix.GetLength(0) != 1)
                {
                    Console.Write($"{matrix[i, 0]}\t");
                    Console.WriteLine();
                }
                else
                {
                    Console.Write($"{matrix[0, i]}\t");
                }
            }
            Console.WriteLine();
        }
    }


    class Program
    {
        
        public static double[,] MultiplyMatrix(IAlgebra a, IAlgebra b)
        {
            double[,] c = null;

            if (a.matrix.GetLength(1) == b.matrix.GetLength(0))
            {
                c = new double[a.matrix.GetLength(0), b.matrix.GetLength(1)];
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    for (int j = 0; j < c.GetLength(1); j++)
                    {
                        c[i, j] = 0;
                        for (int k = 0; k < a.matrix.GetLength(1); k++)
                            c[i, j] = c[i, j] + a.matrix[i, k] * b.matrix[k, j];
                    }
                }
            }
            else
            {
                Console.WriteLine("\n Number of columns in First Matrix should be equal to Number of rows in Second Matrix.");
                Console.WriteLine("\n Please re-enter correct dimensions.");
                Environment.Exit(-1);
            }
            return c;
        }

        public static double[,] DiffMatrix(IAlgebra a, IAlgebra b)
        {
            double[,] c = new double[a.matrix.GetLength(0), a.matrix.GetLength(1)];

            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = 0;
                    c[i, j] = a.matrix[i, j] - b.matrix[i, j];
                }
            }

            return c;
        }

        public static double[,] SummMatrix(IAlgebra a, IAlgebra b)
        {
            double[,] c = new double[a.matrix.GetLength(0), a.matrix.GetLength(1)];

            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = 0;
                    c[i, j] = a.matrix[i, j] + b.matrix[i, j];
                }
            }

            return c;
        }

        public static double[,] MultiplyMatrixOnNumber(IAlgebra a, double num)
        {
            double[,] c = new double[a.matrix.GetLength(0), a.matrix.GetLength(1)];

            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = 0;
                    c[i, j] = a.matrix[i, j] * num;
                }
            }

            return c;
        }

        public static double[,] PowMatrixOnNumber(IAlgebra a, double num)
        {
            double[,] c = new double[a.matrix.GetLength(0), a.matrix.GetLength(1)];

            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = 0;
                    c[i, j] = Math.Pow(a.matrix[i, j], num);
                }
            }

            return c;
        }

        public static double[,] MatrixTransp(IAlgebra a)
        {
            double[,] c = new double[a.matrix.GetLength(1), a.matrix.GetLength(0)];

            for (int i = 0; i < c.GetLength(0); i++)
            {
                for (int j = 0; j < c.GetLength(1); j++)
                {
                    c[i, j] = a.matrix[j, i];
                }
            }

            return c;
        }

        static void Main(string[] args)
        {
            const int n = 3;
            const double K1 = 0.00000001;
            const double K2 = 0.00000005;

            //Vector 
            Console.WriteLine("------------- vector b -------------");

            var b = new Vector(n);
            for (int i = 1; i <= n; i++)
            {
                b.matrix[i - 1, 0] = 6.0 / Math.Pow(i, 2);
            }
            b.Print();

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix A -------------");

            var A = new Matrix(n);
            A.Set();
            A.Print();
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine("------------- matrix y1 -------------");

            var y1 = new Vector(n);
            y1.matrix = MultiplyMatrix(A, b);
            y1.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix A1 -------------");

            var A1 = new Matrix(n);
            A1.Set();
            A1.Print();
            Console.WriteLine();
            Console.WriteLine();


            Console.WriteLine("-------------vector b1 -------------");

            var b1 = new Vector(n);
            b1.Set();
            b1.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------vector c1 -------------");

            var c1 = new Vector(n);
            c1.Set();
            c1.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("------------- matrix y2 -------------");

            var y2 = new Vector(n);
            var tmp6b1 = new Vector(n) { matrix = MultiplyMatrixOnNumber(b1, 6) };
            var tmpDiffB1C1 = new Vector(n) { matrix = DiffMatrix(tmp6b1, c1) };
            y2.matrix = MultiplyMatrix(A1, tmpDiffB1C1);
            y2.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix A2 -------------");

            var A2 = new Matrix(n);
            A2.Set();
            A2.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix B2 -------------");

            var B2 = new Matrix(n);
            B2.Set();
            B2.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix C2 -------------");

            var C2 = new Matrix(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    C2.matrix[i, j] = 1.0 / Math.Pow(i + j + 2, 3);
                }
            }
            C2.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix Y3 -------------");

            var Y3 = new Matrix(n);
            var tmp10B2 = new Matrix(n) { matrix = MultiplyMatrixOnNumber(B2, 10) };
            var tmpSummB2C2 = new Matrix(n) { matrix = SummMatrix(tmp10B2, C2) };
            Y3.matrix = MultiplyMatrix(A2, tmpSummB2C2);
            Y3.Print();
            Console.WriteLine();
            Console.WriteLine();

            // RESULTS AND THREADING
            var R1 = new Matrix(n);
            var R2 = new Matrix(n);
            var R3 = new Matrix(n);
            var R4 = new Matrix(n);

            Thread th1 = new Thread(() =>
            {
                var y2Transp = new Vector(n) { matrix = MatrixTransp(y2) };

                var k1y2 = new Vector(n) { matrix = MultiplyMatrixOnNumber(y2Transp, K1) };

                var k1y2y1 = MultiplyMatrix(k1y2, y1)[0,0];

                var powY3_2 = new Matrix(n) { matrix = MultiplyMatrix(Y3, Y3) };

                R1 = new Matrix(n) { matrix = MultiplyMatrixOnNumber(powY3_2, k1y2y1)};
            });


            Thread th2 = new Thread(() =>
            {
                var y1Transp = new Vector(n) { matrix = MatrixTransp(y1) };

                var k2y1 = new Vector(n) { matrix = MultiplyMatrixOnNumber(y1Transp, K2) };

                var k2y1y2 = MultiplyMatrix(k2y1, y2)[0, 0];

                var powY3_3 = new Matrix(n) { matrix = MultiplyMatrix(Y3, Y3) };

                R2 = new Matrix(n) { matrix = MultiplyMatrixOnNumber(powY3_3, k2y1y2) };
            });

            Thread th3 = new Thread(() =>
            {
                var y2Transp = new Vector(n) { matrix = MatrixTransp(y2) };

                var k2y2 = new Vector(n) { matrix = MultiplyMatrixOnNumber(y2Transp, K2) };

                var y2_Y3 = new Matrix(n) { matrix = MultiplyMatrix(k2y2, Y3) };

                var y2_Y3_y1 = MultiplyMatrix(y2_Y3, y1)[0,0];

                R3 = new Matrix(n) { matrix = MultiplyMatrixOnNumber(Y3, y2_Y3_y1) };

            });

            Thread th4 = new Thread(() =>
            {
                R4 = new Matrix(n) { matrix = Y3.matrix };
            });


            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();

            //Waiting for ALL results
            th1.Join();
            th2.Join();
            th3.Join();
            th4.Join();

            Console.WriteLine("-------------matrix RES1 -------------");
            R1.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix RES2 -------------");
            R2.Print();
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("-------------matrix RES3 -------------");
            R3.Print();
            Console.WriteLine();
            Console.WriteLine();
            
            Console.WriteLine("-------------matrix RES4 -------------");
            R4.Print();
            Console.WriteLine();
            Console.WriteLine();

            //SUMM EVERYTHING AND GET THE FINAL VALUE
            var sum1 = new Matrix(n) { matrix = SummMatrix(R1,R2) };
            var sum2 = new Matrix(n) { matrix = SummMatrix(sum1, R3) };
            var sum3 = new Matrix(n) { matrix = SummMatrix(sum2, R4) };
         
            Console.WriteLine("-------------RESULT -------------");
            sum3.Print();

            Console.ReadKey();
        }
    }
}
