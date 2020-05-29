using GameTOP.Interface;

namespace GameTOP.Lib
{
    public class Jogador2 : IJogador
    {
        public string Chuta()
        {
            return "Maradona esta Chutando";
        }

        public string Corre()
        {
            return "Maradona esta Correndo";
        }

        public string Passa()
        {
            return "Maradona esta Passando";
        }
    }
}