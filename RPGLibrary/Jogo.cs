using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RPGLibrary
{
    public class Jogo
    {
        int _sobe;
        int _desce;
        int _avanca;
        int _recua;
        int[,] _cenario;
        int _linhas = 5;
        int _colunas = 5;
        string _mensagem;
        bool _lutando;
        int _gridSize;

        public int Sobe { get => _sobe; set => _sobe = value; }
        public int Desce { get => _desce; set => _desce = value; }
        public int Avanca { get => _avanca; set => _avanca = value; }
        public int Recua { get => _recua; set => _recua = value; }
        public int[,] Cenario { get => _cenario; set => _cenario = value; }
        public int Linhas { get => _linhas; set => _linhas = value; }
        public int Colunas { get => _colunas; set => _colunas = value; }
        public string Mensagem { get => _mensagem; set => _mensagem = value; }
        public bool Lutando { get => _lutando; set => _lutando = value; }
        public int GridSize { get => _gridSize; set => _gridSize = value; }

        public Jogo(int linhas, int colunas)
        {
            Random random = new Random();

            Linhas = linhas;
            Colunas = colunas;

            Cenario = new int[Linhas, Colunas];

            for (int i = 0; i < Linhas; i++)
            {
                for (int j = 0; j < Colunas; j++)
                {
                    Cenario[i, j] = random.Next(4);
                }
            }

        }
        public void Batalha(ref double forca, ref int vidas)
        {
            int _rounds = 5;
            Mensagem = "Prepare-se a batalha irá comerçar!";
            if (Lutando == false)
            {
                Lutando = true;
            }

            for (int i = 0; i < _rounds; i++)
            {
                if (forca < Luta())
                {
                    forca = 0;
                }
                else
                {
                    forca -= Luta();
                }

                if (forca == 0)
                {
                    vidas--;
                    Lutando = false;
                    forca = 10;
                    break;

                }
                if( vidas == 0)
                {

                }
            }
            Lutando = false;
        }

        public int Luta()
        {
            Random random = new Random();

            return random.Next(1, 3);
        }
    }
}
