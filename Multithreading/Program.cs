using System.Diagnostics;

namespace Multithreading.Console;

using System;

public class Program
{
    static void Main()
    {
        SomaDeMatrizes somaDeMatrizes = new SomaDeMatrizes();
        MultiplicacaoDeMatrizes multiplicacaoDeMatrizes = new MultiplicacaoDeMatrizes();
        InversaoDeMatrizes inversaoDeMatrizes = new InversaoDeMatrizes();

        int opcao = 0;

        while (opcao != 4)
        {
            Console.WriteLine("Selecione uma das opções abaixo:");
            Console.WriteLine("1 - Soma de matrizes");
            Console.WriteLine("2 - Multiplicação de matrizes");
            Console.WriteLine("3 - Inversão de matriz:");
            Console.WriteLine("4 - Sair");

            opcao = Convert.ToInt32(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Digite o tamanho da matriz:");
                    int size = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("0 - Paralela ou 1 - Sequencial?");
                    bool isSequential = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                    somaDeMatrizes.Soma(size, isSequential);
                    break;
                case 2:
                    Console.WriteLine("Digite o tamanho da matriz:");
                    size = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("0 - Paralela ou 1 - Sequencial?");
                    isSequential = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                    multiplicacaoDeMatrizes.Multiplicacao(size, isSequential);
                    break;
                case 3:
                    Console.WriteLine("Digite o tamanho da matriz:");
                    size = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("0 - Paralela ou 1 - Sequencial?");
                    isSequential = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                    inversaoDeMatrizes.Inversao(size, isSequential);
                    break;
                case 4:
                    break;
            }
        }


        //multiplicacaoDeMatrizes.Multiplicacao(10000, true);
        //somaDeMatrizes.Soma(10000, true);
    }
}