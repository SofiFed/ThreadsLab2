using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab2
{
    class Program
    {
        class Matrix
        {
            public int n { get; set; }
            public double[,] elements { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public Matrix(int N)
            {
                n = N;
                elements = new double[n, n];
            }
            public Matrix(int N, string Type, string Name) : this(N)
            {
                name = Name;
                type = Type;
            }
            public static Matrix operator *(Matrix matrix1, Matrix matrix2)
            {
                int n = matrix1.n;
                bool mtrx1_isnumber = true;
                bool mtrx2_isnumber = true;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == 0 & j == 0)
                            continue;
                        if (matrix1.elements[i, j] != 0 & mtrx1_isnumber)
                            mtrx1_isnumber = false;
                        if (matrix2.elements[i, j] != 0 & mtrx2_isnumber)
                            mtrx2_isnumber = false;

                    }
                }
                Matrix NewMatrix = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (mtrx1_isnumber)
                            NewMatrix.elements[i, j] = matrix1.elements[0, 0] * matrix2.elements[i, j];
                        else if (mtrx2_isnumber)
                            NewMatrix.elements[i, j] = matrix1.elements[i, j] * matrix2.elements[0, 0];
                        else
                        {
                            double s = 0;
                            for (int k = 0; k < n; k++)
                                s += matrix1.elements[i, k] * matrix2.elements[k, j];
                            NewMatrix.elements[i, j] = s;
                        }
                    }
                }
                return NewMatrix;
            }

            public static Matrix operator *(double number, Matrix matrix)
            {
                int n = matrix.n;
                Matrix NewMatrix = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        NewMatrix.elements[i, j] = number * matrix.elements[i, j];
                }
                return NewMatrix;
            }

            public static Matrix operator ~(Matrix matrix)
            {
                int n = matrix.n;
                Matrix NewMatrix = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        NewMatrix.elements[j, i] = matrix.elements[i, j];
                }
                return NewMatrix;
            }

            public static Matrix operator +(Matrix matrix1, Matrix matrix2)
            {
                int n = matrix1.n;
                Matrix NewMatrix = new Matrix(n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        NewMatrix.elements[i, j] = matrix1.elements[i, j] + matrix2.elements[i, j];
                }
                return NewMatrix;
            }
        }

        class ExpressionCalculation
        {
            private int n;
            public int N
            {
                get { return n; }
                set { if (value > 0) { n = value; } else { Console.WriteLine("The size of the matrix must be a positive integer value!"); } }
            }
            public Matrix active_obj { get; private set; }
            public double K1 { get; private set; }
            public double K2 { get; private set; }
            public Matrix A { get; private set; }
            public Matrix A1 { get; private set; }
            public Matrix A2 { get; private set; }
            public Matrix B2 { get; private set; }
            public Matrix Y3 { get; private set; }
            public Matrix C2 { get; private set; }
            public Matrix y1 { get; private set; }
            public Matrix b { get; private set; }
            public Matrix b1 { get; private set; }
            public Matrix c1 { get; private set; }
            public Matrix y2 { get; private set; }
            public Matrix addition1 { get; private set; }
            public Matrix addition2 { get; private set; }
            public Matrix addition3 { get; private set; }
            public Matrix addition4 { get; private set; }
            public Matrix addition5 { get; private set; }
            public Matrix X { get; private set; }
            public Matrix ExpressionPart1 { get; private set; }
            public Matrix ExpressionPart2 { get; private set; }
            public ExpressionCalculation(int N)
            {
                n = N;
                active_obj = new Matrix(n);
                A = new Matrix(n, "matrix", "A");
                A1 = new Matrix(n, "matrix", "A1");
                A2 = new Matrix(n, "matrix", "A2");
                B2 = new Matrix(n, "matrix", "B2");
                Y3 = new Matrix(n, "matrix", "Y3");
                C2 = new Matrix(n, "matrix", "C2");
                y1 = new Matrix(n, "vector", "y1");
                b = new Matrix(n, "vector", "b");
                b1 = new Matrix(n, "vector", "b1");
                c1 = new Matrix(n, "vector", "c1");
                y2 = new Matrix(n, "vector", "y2");
                addition1 = new Matrix(n, "matrix", "addition 1");
                addition2 = new Matrix(n, "matrix", "addition 2");
                addition3 = new Matrix(n, "matrix", "addition 3");
                addition4 = new Matrix(n, "matrix", "addition 4");
                addition5 = new Matrix(n, "matrix", "addition 5");
                ExpressionPart1 = new Matrix(n);
                ExpressionPart2 = new Matrix(n);
                X = new Matrix(n, "matrix", "X");
            }
            public void ElementsForTheAlgebraicObject(object Obj)
            {
                switch (Obj)
                {
                    case "A": { active_obj = A; break; }
                    case "A1": { active_obj = A1; break; }
                    case "b1": { active_obj = b1; break; }
                    case "c1": { active_obj = c1; break; }
                    case "A2": { active_obj = A2; break; }
                    case "B2": { active_obj = B2; break; }
                }
                Console.Write($"\nChoosing how to create a {active_obj.type} {active_obj.name}\n" +
                "1 - generate random elements, 2 - enter elements using the keyboard : ");
                string method = Console.ReadLine();
                switch (method)
                {
                    case "1":
                        Console.WriteLine($"\nRandomly generating elements of {active_obj.type} {active_obj.name}...");
                        Random rand = new Random();
                        for (int i = 0; i < n; i++)
                        {
                            for (int j = 0; j < n; j++)
                                active_obj.elements[i, j] = (active_obj.type == "vector" & j != 0) ? 0 : rand.Next();
                        }
                        Console.WriteLine("Done!");
                        break;
                    case "2":
                        Console.WriteLine($"\nInput of elements using the keyboards...");
                        for (int i = 0; i < n; i++)
                        {
                            Console.WriteLine("");
                            for (int j = 0; j < n; j++)
                            {
                                if (active_obj.type == "vector" & j != 0)
                                    active_obj.elements[i, j] = 0.0;
                                else
                                {
                                    Console.Write($"{active_obj.name}[{i}{j}] = ");
                                    try { active_obj.elements[i, j] = Convert.ToDouble(Console.ReadLine()); }
                                    catch { Console.WriteLine("Incorrect input. re-enter, please!"); j--; }
                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Error...Enter 1 or 2, please...");
                        this.ElementsForTheAlgebraicObject(Obj);
                        break;
                }
                switch (Obj)
                {
                    case "A": { A = active_obj; ; ShowMatrix(A); break; }
                    case "A1": { A1 = active_obj; ShowMatrix(A1); break; }
                    case "b1": { b1 = active_obj; ShowMatrix(b1); break; }
                    case "c1": { c1 = active_obj; ShowMatrix(c1); break; }
                    case "A2": { A2 = active_obj; ShowMatrix(A2); break; }
                    case "B2": { B2 = active_obj; ShowMatrix(B2); break; }
                }
            }
            public void Vector_b()
            {
                for (int i = 1; i <= n; i++)
                    b.elements[i - 1, 0] = i % 2 == 0 ? 11.0 * (i * i) : 11.0 / i;
                ShowMatrix(b);
            }
            public void Matrix_C()
            {
                for (int i = 1; i <= n; i++)
                {
                    for (int j = 1; j <= n; j++)
                        C2.elements[i - 1, j - 1] = 1.0 / (i * i + j);
                }
                ShowMatrix(C2);
            }
            public void Vector_y1() { y1 = A * b; ShowMatrix(y1); }
            public void Vector_y2_Part1() { y2 = b1 + (-2 * c1); ShowMatrix(y2); }
            public void Vector_y2_Part2() { y2 = A1 * y2; ShowMatrix(y2); }
            public void Matrix_Y3_Part1() { Y3 = B2 + (-2 * C2); ShowMatrix(Y3); }
            public void Matrix_Y3_Part2() { Y3 = A2 * Y3; ShowMatrix(Y3); }
            public void Addition1() { addition1 = y2 * ~y1; ShowMatrix(addition1); }
            public void Addition2_Part1() { addition2 = ~y2 * y2; ShowMatrix(addition2); }
            public void Addition2_Part2() { addition2 = addition2 * Y3; ShowMatrix(addition2); }
            public void Addition3_Part1() { addition3 = Y3 * Y3; ShowMatrix(addition3); }
            public void Addition3_Part2() { addition3 = ~y1 * addition3; ShowMatrix(addition3); }
            public void Addition3_Part3() { addition3 = addition3 * y2; ShowMatrix(addition3); }
            public void Addition4() { addition4 = Y3; ShowMatrix(addition4); }
            public void Addition5_Part1() { addition5 = Y3 * y1; ShowMatrix(addition5); }
            public void Addition5_Part2() { addition5 = addition5 * ~y1; ShowMatrix(addition5); }
            public void Addition5_Part3() { addition5 = addition5 * Y3; ShowMatrix(addition5); }
            public void Expression_Part1() { ExpressionPart1 = K1 * addition1 + K1 * addition2;  ShowMatrix(ExpressionPart1); }
            public void Expression_Part2() { ExpressionPart2 = K2 * addition3 + K2 * addition5; ShowMatrix(ExpressionPart2); }
            public void Expression_Result() { X = ExpressionPart1 + ExpressionPart2 + addition4; ShowMatrix(X); }

            public void ChoiceOf_K()
            {
                while (true)
                {
                    Console.Write("\nEnter the values for the number K1 for next matrixs : \n\n");
                    ShowMatrix(addition1);
                    Console.WriteLine("\nand\n");
                    ShowMatrix(addition2);
                    Console.Write("\nK1 = ");
                    try { K1 = Convert.ToDouble(Console.ReadLine()); break; }
                    catch { Console.WriteLine("Incorrect input. re-enter, please!"); }
                }
                while (true)
                {
                    Console.Write("\nEnter the values for the number K2 for next matrixs : \n\n");
                    ShowMatrix(addition3);
                    Console.WriteLine("\nand\n");
                    ShowMatrix(addition5);
                    Console.Write("\nK2 = ");
                    try { K2 = Convert.ToDouble(Console.ReadLine()); break; }
                    catch { Console.WriteLine("Incorrect input. re-enter, please!"); }
                }
            }

            public void ShowMatrix(Matrix matrix)
            {
                Console.WriteLine("\n"+matrix.name);
                for (int i = 0; i < n; i++)
                {
                    Console.Write("[");
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write("{0, -25}", Math.Round(matrix.elements[i, j], 3));
                    }
                    Console.Write("]");
                    Console.WriteLine();
                }
            }
        }

        static void Main(string[] args)
        {
            int n = 0;
            while (true)
            {
                Console.WriteLine("Enter the size of matrix : ");
                try { n = int.Parse(Console.ReadLine()); break; }
                catch { Console.WriteLine("Incorrect input. re-enter, please!"); }
            }
            
            ExpressionCalculation E = new ExpressionCalculation(n);
            Thread threadForA = new Thread(new ParameterizedThreadStart(E.ElementsForTheAlgebraicObject));
            Thread threadForA1 = new Thread(new ParameterizedThreadStart(E.ElementsForTheAlgebraicObject));
            Thread threadForb1 = new Thread(new ParameterizedThreadStart(E.ElementsForTheAlgebraicObject));
            Thread threadForc1 = new Thread(new ParameterizedThreadStart(E.ElementsForTheAlgebraicObject));
            Thread threadForA2 = new Thread(new ParameterizedThreadStart(E.ElementsForTheAlgebraicObject));
            Thread threadForB2 = new Thread(new ParameterizedThreadStart(E.ElementsForTheAlgebraicObject));
            Thread threadForC = new Thread(E.Matrix_C);
            Thread threadForb = new Thread(E.Vector_b);
            Thread threadFory1 = new Thread(E.Vector_y1);
            Thread threadFory2part1 = new Thread(E.Vector_y2_Part1);
            Thread threadFory2part2 = new Thread(E.Vector_y2_Part2);
            Thread threadForY3part1 = new Thread(E.Matrix_Y3_Part1);
            Thread threadForY3part2 = new Thread(E.Matrix_Y3_Part2);
            Thread threadForAddition1 = new Thread(E.Addition1);
            Thread threadForAddition2part1 = new Thread(E.Addition2_Part1);
            Thread threadForAddition2part2 = new Thread(E.Addition2_Part2);
            Thread threadForAddition3part1 = new Thread(E.Addition3_Part1);
            Thread threadForAddition3part2 = new Thread(E.Addition3_Part2);
            Thread threadForAddition3part3 = new Thread(E.Addition3_Part3);
            Thread threadForAddition4 = new Thread(E.Addition4);
            Thread threadForAddition5part1 = new Thread(E.Addition5_Part1);
            Thread threadForAddition5part2 = new Thread(E.Addition5_Part2);
            Thread threadForAddition5part3 = new Thread(E.Addition5_Part3);
            Thread threadForK = new Thread(E.ChoiceOf_K);
            Thread threadForExpressionpart1 = new Thread(E.Expression_Part1);
            Thread threadForExpressionpart2 = new Thread(E.Expression_Part2);
            Thread threadForX = new Thread(E.Expression_Result);

            //without threads
            /*E.ElementsForTheAlgebraicObject("A");
            E.ElementsForTheAlgebraicObject("A1");
            E.ElementsForTheAlgebraicObject("b1");
            E.ElementsForTheAlgebraicObject("c1");
            E.ElementsForTheAlgebraicObject("A2");
            E.ElementsForTheAlgebraicObject("B2");
            E.Matrix_C();
            E.Vector_b();
            E.Vector_y1();
            E.Vector_y2_Part1();
            E.Vector_y2_Part2();
            E.Matrix_Y3_Part1();
            E.Matrix_Y3_Part2();
            E.Addition1();
            E.Addition2_Part1();
            E.Addition2_Part2();
            E.Addition3_Part1();
            E.Addition3_Part2();
            E.Addition3_Part3();
            E.Addition4();
            E.Addition5_Part1();
            E.Addition5_Part2();
            E.Addition5_Part3();
            E.ChoiceOf_K();
            E.ShowMatrix(E.addition1);
            E.ShowMatrix(E.addition2);
            E.ShowMatrix(E.addition3);
            E.ShowMatrix(E.addition4);
            E.ShowMatrix(E.addition5);
            E.ShowMatrix(E.K1 * E.addition1);
            E.ShowMatrix(E.K1 * E.addition2);
            E.ShowMatrix(E.K2 * E.addition3);
            E.ShowMatrix(E.K2 * E.addition5);
            E.Expression_Part1();
            E.Expression_Part2();
            E.Expression_Result();*/


            //for checking
            threadForA.Start("A");
            threadForA.Join();
            threadForA1.Start("A1");
            threadForA1.Join();
            threadForb1.Start("b1");
            threadForb1.Join();
            threadForc1.Start("c1");
            threadForc1.Join();
            threadForA2.Start("A2");
            threadForA2.Join();
            threadForB2.Start("B2");
            threadForB2.Join();

            //main version
            /*threadForA.Start("A");
            threadForA1.Start("A1");
            threadForb1.Start("b1");
            threadForc1.Start("c1");
            threadForA2.Start("A2");
            threadForB2.Start("B2");*/

            threadForC.Start();
            threadForb.Start();

            threadForb.Join();
            threadForA.Join();
            threadFory1.Start();

            threadForb1.Join();
            threadForc1.Join();
            threadFory2part1.Start();

            threadForB2.Join();
            threadForC.Join();
            threadForY3part1.Start();

            threadForA1.Join();
            threadFory2part1.Join();
            threadFory2part2.Start();

            threadForA2.Join();
            threadForY3part1.Join();
            threadForY3part2.Start();

            threadFory1.Join();
            threadFory2part2.Join();
            threadForAddition1.Start();

            threadForY3part2.Join();
            threadForAddition2part1.Start();
            threadForAddition3part1.Start();
            threadForAddition4.Start();
            threadForAddition5part1.Start();

            threadForAddition2part1.Join();
            threadForAddition2part2.Start();

            threadForAddition3part1.Join();
            threadForAddition3part2.Start();

            threadForAddition5part1.Join();
            threadForAddition5part2.Start();

            threadForAddition3part2.Join();
            threadForAddition3part3.Start();

            threadForAddition5part2.Join();
            threadForAddition5part3.Start();

            threadForAddition1.Join();
            threadForAddition2part2.Join();
            threadForAddition3part3.Join();
            threadForAddition4.Join();
            threadForAddition5part3.Join();
            threadForK.Start();

            threadForK.Join();
            threadForExpressionpart1.Start();
            threadForExpressionpart2.Start();

            threadForExpressionpart1.Join();
            threadForExpressionpart2.Join();
            threadForX.Start();

            threadForX.Join();



            Console.ReadKey();
        }
    }
}
