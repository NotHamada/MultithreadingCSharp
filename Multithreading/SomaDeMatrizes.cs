using System;
using System.Diagnostics;
using System.Threading;

public interface ISomaDeMatrizes
{
    void Soma(int size, bool isSequential);
}

public class SomaDeMatrizes : ISomaDeMatrizes
{
    public void Soma(int size, bool isSequential)
    {
        int[,] matrixA = GenerateMatrix(size);
        int[,] matrixB = GenerateMatrix(size);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        if (isSequential)
        {
            var sequential = AddMatricesSequential(matrixA, matrixB, size);
        }
        else
        {
            var parallel = AddMatricesParallel(matrixA, matrixB, 4);
        }

        stopwatch.Stop();
        if(isSequential)
        {
            Console.WriteLine(
            $"Tempo para soma sequencial de matrizes de tamanho {size}: {stopwatch.ElapsedMilliseconds} ms");
        }
        else
        {
            Console.WriteLine(
            $"Tempo para soma paralela de matrizes de tamanho {size}: {stopwatch.ElapsedMilliseconds} ms");
        }
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

    static int[,] AddMatricesSequential(int[,] matrixA, int[,] matrixB, int size)
    {
        int[,] result = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }

        return result;
    }

    static int[,] AddMatricesParallel(int[,] matrixA, int[,] matrixB, int threadCount)
    {
        int size = matrixA.GetLength(0);
        int[,] result = new int[size, size];
        int rowsPerThread = size / threadCount;
        Thread[] threads = new Thread[threadCount];

        for (int threadIndex = 0; threadIndex < threadCount; threadIndex++)
        {
            int startRow = threadIndex * rowsPerThread;
            int endRow = (threadIndex == threadCount - 1) ? size : startRow + rowsPerThread;
            threads[threadIndex] = new Thread(() => AddMatrixRows(matrixA, matrixB, result, startRow, endRow));
            threads[threadIndex].Start();
        }

        foreach (Thread thread in threads)
            thread.Join();

        return result;
    }

    static void AddMatrixRows(int[,] matrixA, int[,] matrixB, int[,] result, int startRow, int endRow)
    {
        for (int i = startRow; i < endRow; i++)
        for (int j = 0; j < matrixA.GetLength(1); j++)
            result[i, j] = matrixA[i, j] + matrixB[i, j];
    }
}