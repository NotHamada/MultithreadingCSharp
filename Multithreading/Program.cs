namespace Multithreading.Console;

public class Program
{
    static void Main()
    {
        SomaDeMatrizes somaDeMatrizes = new SomaDeMatrizes();
        MultiplicacaoDeMatrizes multiplicacaoDeMatrizes = new MultiplicacaoDeMatrizes();
        InversaoDeMatrizes inversaoDeMatrizes = new InversaoDeMatrizes();

        inversaoDeMatrizes.Inversao(1000, false);
         

        //multiplicacaoDeMatrizes.Multiplicacao(100000, true);
        //somaDeMatrizes.Soma(10000, true);
    }
}