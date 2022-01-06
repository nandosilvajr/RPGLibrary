using Android.Widget;
using RPGLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RGPXamarin.ViewModels
{
    public class MainViewModel : BindableObject
    {

        int _x;
        int _y;

        int _positionX;
        int _positionY;

        int _positionGridX;
        int _positionGridY;

        int _vidas;

        double _forca;

        string _imageSource;
        Jogo Jogo { get; set; }
        Jogador Jogador { get; set; }
        public int Vidas { get => _vidas; set => _vidas = value; }

        ObservableCollection<ImageGridSet> _imagens;

        Image _minhaImagem;

        ImageGridSet _imageGridSet;
        string _name;

        ICommand _adicionarCommand;

        public MainViewModel()
        {
            Jogador = new Jogador(_x, _y, 5, 5);
            Jogo = new Jogo(5, 5);

            GetVidas();
            Swipecommand = new Command<string>(SwipReactions);

            GetPosition(out _positionX, out _positionY);

            Jogador.PosicaoX = _positionX;
            Jogador.PosicaoY = _positionY;

            PositionX = Jogador.PosicaoX;
            PositionY = Jogador.PosicaoY;

            Forca = Jogador.Forca / 10;

            ImageGridSet = new ImageGridSet();

            Imagens = new ObservableCollection<ImageGridSet>();
/*            ImageGridSet.Name = "perso.png";

            ImageGridSet.PositionGridX = PositionX;
            ImageGridSet.PositionGridY = PositionY;


            Imagens.Add(ImageGridSet);
*/
            ImageSource = Jogador.Vidas.ToString();

            //Debug.WriteLine("Vidas" + Vidas);
        }

        private void GetPosition(out int positionX, out int positionY)
        {
            positionX = 0;
            positionY = 0;
            for (int i = 0; i < Jogo.Cenario.GetLength(0); i++)
            {
                for (int j = 0; j < Jogo.Cenario.GetLength(1); j++)
                {
                    if (Jogo.Cenario[i, j] == 0)
                    {
                        _positionX = i;
                        _positionY = j;
                        break;
                    }
                }
            }

        }

        private void SwipReactions(string str)
        {


            switch (str)
            {
                case "Left":
                    Debug.WriteLine("Esquerda");
                    Esquerda();
                    int casa = Jogo.Cenario[Jogador.PosicaoX, Jogador.PosicaoY];
                    UpdateScore(casa);
                    break;
                case "Right":
                    Debug.WriteLine("Direita");

                    Direita();
                    casa = Jogo.Cenario[Jogador.PosicaoX, Jogador.PosicaoY];
                    UpdateScore(casa);
                    break;
                case "Up":
                    Debug.WriteLine("Cima");

                    Cima();
                    casa = Jogo.Cenario[Jogador.PosicaoX, Jogador.PosicaoY];
                    UpdateScore(casa);
                    break;

                case "Down":
                    Debug.WriteLine("Baixo");
                    casa = Jogo.Cenario[Jogador.PosicaoX, Jogador.PosicaoY];
                    UpdateScore(casa);
                    Baixo();
                    break;
            }

        }

        private void UpdateScore(int casa)
        {
            if (casa == 2)
            {
                Application.Current.MainPage.DisplayAlert("Campo de batalha!", Jogo.Mensagem, "Ok");
                Jogo.Batalha(ref _forca, ref _vidas);
                Jogador.Vidas = _vidas;
                ImageSource = _vidas.ToString();
                Jogador.Forca = (int)_forca;
                Forca = (double)Jogador.Forca / 10;
                Jogo.Cenario[Jogador.PosicaoX, Jogador.PosicaoY] = 0;

                if (Jogador.Forca < 10)
                {
                    Jogo.Mensagem = "Você ganhou a batalha. Parabéns!";
                    Application.Current.MainPage.DisplayAlert("Campo de batalha!", Jogo.Mensagem, "Ok");
                }
                else
                {
                    ImageGridSet image = new ImageGridSet();

                    image.Name = "rip.png";
                    image.PositionGridX = PositionX;
                    image.PositionGridY = PositionY;

                    Imagens.Add(image);
                    PositionGridX = image.PositionGridX;
                    PositionGridY = image.PositionGridY;

                    AdicionarCommand = new Command(AddImages)
               
                    Jogo.Mensagem = "Você perdeu a batalha!";
                    Application.Current.MainPage.DisplayAlert("Campo de batalha!", Jogo.Mensagem, "Ok");

                }

            }
            else if (casa == 3)
            {
                if (Jogador.Forca < 10)
                {
                    Jogador.Forca = 10;
                    Forca = Jogador.Forca / 10;

                    if (Jogador.Forca == 10)
                    {
                        if (Jogador.Vidas < 10)
                        {
                            Jogador.Vidas++;
                            ImageSource = Jogador.Vidas.ToString();
                        }
                    }
                    // Zera o campo que já foi passado pelo jogador
                    Jogo.Cenario[Jogador.PosicaoX, Jogador.PosicaoY] = 0;
                    /*pictureBox.Image = Properties.Resources.pills;
                    this.Controls.Add(pictureBox);
                    pictureBox.BringToFront();*/
                }
            }
            Debug.WriteLine("Casa>>" + casa);
            /*                pBarForca.BringToFront();
                            pictureBoxLife.BringToFront();
                            lblVidas.BringToFront();
                            pictureBoxCharacter.BringToFront();*/

        }

        private void AddImages(object obj)
        {
            ImageGridSet.Name = "rip.png";
          


            Grid.SetRow(ImageGridSet, PositionY);
        }

        private void Esquerda()
        {
            Jogador.PosicaoX -= 1;
            PositionX = Jogador.PosicaoX;
        }

        private void Direita()
        {
            Jogador.PosicaoX += 1;
            PositionX = Jogador.PosicaoX;
        }

        private void Cima()
        {
            Jogador.PosicaoY -= 1;
            PositionY = Jogador.PosicaoY;
        }

        private void Baixo()
        {
            Jogador.PosicaoY += 1;
            PositionY = Jogador.PosicaoY;
        }

        private void GetVidas()
        {
            Vidas = Jogador.Vidas;
        }

        public Command AdicionarCommand { get; set; }
        public Command<string> Swipecommand { get; set; }
        public int PositionX
        {
            get => _positionX;
            set
            {
                _positionX = value;
                OnPropertyChanged();
            }
        }

        public int PositionY
        {
            get => _positionY;
            set
            {
                _positionY = value;
                OnPropertyChanged();
            }
        }

        public double Forca
        {
            get => _forca;
            set
            {
                _forca = value;
            }
        }

        public string ImageSource
        {
            get => _imageSource;
            set
            {
                if (Vidas > 0)
                {
                    _imageSource = $"hart{value}.png";
                    OnPropertyChanged();
                }

            }

        }

        public ObservableCollection<ImageGridSet> Imagens { 
            get => _imagens;
            set {
                _imagens = value;
                OnPropertyChanged();
            }
        
        }

        public ImageGridSet ImageGridSet { 
            get => _imageGridSet;
            set
            {
                _imageGridSet = value;
                OnPropertyChanged();
            }
        
        }

        public string Name { 
            get => _name;
            set { 
                _name = value;
                OnPropertyChanged();
            } 
        }

        public Image MinhaImagem { get => _minhaImagem; set => _minhaImagem = value; }

        string _someImage = string.Empty;
        public string SomeImage
        {
            get { return string.Format(_someImage); }
            set { 
                _someImage = string.Format(value);
                OnPropertyChanged();
            }
        }

        public int PositionGridY
        {
            get => _positionGridY;
            set { 
                _positionGridY = value;
                OnPropertyChanged();
            }
        }
        public int PositionGridX
        {
            get => _positionGridX;
            set
            {
                _positionGridX = value;
                OnPropertyChanged();
            }
        }
    }
}
