using GameTOP.Lib;

namespace GameTOP
{
    class Program
    {
        static void Main(string[] args)
        {
            var jogo = new JogoFODA(new Jogador1("Rivaldo"), new Jogador1("Ronaldo"));
            jogo.IniciarJogo();
        }
    }
}
