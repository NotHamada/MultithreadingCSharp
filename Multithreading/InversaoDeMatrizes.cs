namespace Multithreading.Console;

using System;
using System.Diagnostics;
using System.Threading;

public interface IInversaoDeMatrizes
{
    void Inversao(int size, bool isSequential);
}

public class InversaoDeMatrizes : IInversaoDeMatrizes
{
    public void Inversao(int size, bool isSequential)
    {
        double[,] matrix = GenerateMatrix(size);
        double[,] inverse = new double[size, size];
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var success = isSequential ? InverseMatrixSequential(matrix, inverse, size) : InverseMatrixParallel(matrix, inverse, size, 4);

        stopwatch.Stop();
        Console.WriteLine(success
            ? $"Tempo para inversão {(isSequential ? "sequencial" : "paralela")} de matriz de tamanho {size}: {stopwatch.ElapsedMilliseconds} ms"
            : "A matriz não é invertível.");
    }

    static double[,] GenerateMatrix(int size)
    {
        double[,] matrix = new double[size, size];
        Random random = new Random();
        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
            matrix[i, j] = random.Next(1, 100);

        return matrix;
    }

    static bool InverseMatrixSequential(double[,] matrix, double[,] inverse, int size)
    {
        InitializeIdentityMatrix(inverse, size);

        for (int i = 0; i < size; i++)
        {
            double pivot = matrix[i, i];
            if (pivot == 0)
            {
                return false;
            }

            for (int j = 0; j < size; j++)
            {
                matrix[i, j] /= pivot;
                inverse[i, j] /= pivot;
            }

            for (int k = 0; k < size; k++)
            {
                if (k != i)
                {
                    double factor = matrix[k, i];
                    for (int j = 0; j < size; j++)
                    {
                        matrix[k, j] -= factor * matrix[i, j];
                        inverse[k, j] -= factor * inverse[i, j];
                    }
                }
            }
        }

        return true;
    }

    static bool InverseMatrixParallel(double[,] matrix, double[,] inverse, int size, int threadCount)
    {
        InitializeIdentityMatrix(inverse, size);

        for (int i = 0; i < size; i++)
        {
            double pivot = matrix[i, i];
            if (pivot == 0)
            {
                return false;
            }

            Parallel.For(0, size, j =>
            {
                matrix[i, j] /= pivot;
                inverse[i, j] /= pivot;
            });

            Thread[] threads = new Thread[threadCount];
            int rowsPerThread = size / threadCount;

            for (int threadIndex = 0; threadIndex < threadCount; threadIndex++)
            {
                int startRow = threadIndex * rowsPerThread;
                int endRow = (threadIndex == threadCount - 1) ? size : startRow + rowsPerThread;
                threads[threadIndex] = new Thread(() => SubtractRow(matrix, inverse, i, startRow, endRow, size));
                threads[threadIndex].Start();
            }

            foreach (Thread thread in threads)
                thread.Join();
        }

        return true;
    }

    static void SubtractRow(double[,] matrix, double[,] inverse, int pivotRow, int startRow, int endRow, int size)
    {
        for (int k = startRow; k < endRow; k++)
        {
            if (k != pivotRow)
            {
                double factor = matrix[k, pivotRow];
                for (int j = 0; j < size; j++)
                {
                    matrix[k, j] -= factor * matrix[pivotRow, j];
                    inverse[k, j] -= factor * inverse[pivotRow, j];
                }
            }
        }
    }

    static void InitializeIdentityMatrix(double[,] matrix, int size)
    {
        for (int i = 0; i < size; i++)
        for (int j = 0; j < size; j++)
            matrix[i, j] = (i == j) ? 1 : 0;
    }

    static void PrintMatrix(double[,] matrix, int size)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write($"{matrix[i, j]:0.00}\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
