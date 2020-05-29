using GameTOP.Interface;

namespace GameTOP.Lib
{
    public class Jogador1 : IJogador
    {
        private readonly string _Nome;
        public Jogador1(string nome)
        {
            _Nome = nome;
        }
        
        public string Chuta()
        {
            return $"{_Nome} esta Chutando";
        }
        public string Corre()
        {
            return $"{_Nome} esta Correndo";
        }
        public string Passa()
        {
            return $"{_Nome} esta Passando";
        }
    }
}