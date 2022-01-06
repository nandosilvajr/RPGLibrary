using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RPGLibrary
{
    public class Jogador
    {
       
        int _posicaoX;
        int _posicaoY;
        int _vidas;
        int _forca;
        int _gridLimiteX;
        int _gridLimiteY;


        public Jogador(int x, int y, int limiteX, int limiteY)
        {
            Vidas = 3;
            Forca = 10;
            PosicaoX = x;
            PosicaoY = y;
            GridLimiteY = limiteY;
            GridLimiteX = limiteX;
        }

        public int PosicaoX
        {
            get => _posicaoX;
            set
            {
                if (value < GridLimiteX && value >= 0)
                    _posicaoX = value;
            }
        }
        public int PosicaoY
        {
            get => _posicaoY;
            set
            {
                if(value < GridLimiteY && value >= 0)
                _posicaoY = value;
            }
        }
        public int Vidas { get => _vidas; set => _vidas = value; }
        public int Forca
        {
            get => _forca;
            set
            {
                _forca = value;
            }
        }

        public int GridLimiteY { get => _gridLimiteY; set => _gridLimiteY = value; }
        public int GridLimiteX { get => _gridLimiteX; set => _gridLimiteX = value; }
    }
}
