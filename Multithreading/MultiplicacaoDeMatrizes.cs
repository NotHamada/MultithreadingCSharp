namespace Multithreading.Console;

using System;
using System.Diagnostics;
using System.Threading;

public class MultiplicacaoDeMatrizes
{
    public static void Multiplicacao()
    {
        int size = 100; // Alterar para 1000, 10000, 100000 conforme necessário
        int[,] matrixA = GenerateMatrix(size);
        int[,] matrixB = GenerateMatrix(size);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int[,] result = MultiplyMatricesParallel(matrixA, matrixB, 4); // 4 threads

        stopwatch.Stop();
        Console.WriteLine($"Tempo para multiplicação paralela de matrizes de tamanho {size}: {stopwatch.ElapsedMilliseconds} ms");
    }

    static int[,] GenerateMatrix(int size)
    {
        int[,] matrix = new int[size, size];
        Random random = new Random();
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                matrix[i, j] = random.Next(1, 100);

        return matrix;
    }

    static int[,] MultiplyMatricesParallel(int[,] matrixA, int[,] matrixB, int threadCount)
    {
        int size = matrixA.GetLength(0);
        int[,] result = new int[size, size];
        int rowsPerThread = size / threadCount;
        Thread[] threads = new Thread[threadCount];

        for (int threadIndex = 0; threadIndex < threadCount; threadIndex++)
        {
            int startRow = threadIndex * rowsPerThread;
            int endRow = (threadIndex == threadCount - 1) ? size : startRow + rowsPerThread;
            threads[threadIndex] = new Thread(() => MultiplyMatrixRows(matrixA, matrixB, result, startRow, endRow));
            threads[threadIndex].Start();
        }

        foreach (Thread thread in threads)
            thread.Join();

        return result;
    }

    static void MultiplyMatrixRows(int[,] matrixA, int[,] matrixB, int[,] result, int startRow, int endRow)
    {
        for (int i = startRow; i < endRow; i++)
            for (int j = 0; j < matrixA.GetLength(1); j++)
                for (int k = 0; k < matrixB.GetLength(0); k++)
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
    }
}
