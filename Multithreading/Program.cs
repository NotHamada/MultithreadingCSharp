namespace Multithreading.Console;

public class Program
{
    static void Main()
    {
        SomaDeMatrizes somaDeMatrizes = new SomaDeMatrizes();
        MultiplicacaoDeMatrizes multiplicacaoDeMatrizes = new MultiplicacaoDeMatrizes();
        
        multiplicacaoDeMatrizes.Multiplicacao(1000);
        //somaDeMatrizes.Soma(10000);
    }
}