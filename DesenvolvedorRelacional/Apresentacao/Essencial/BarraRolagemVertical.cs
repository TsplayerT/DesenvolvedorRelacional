using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DesenvolvedorRelacional.Apresentacao.Base;
using DesenvolvedorRelacional.Infraestrutura;

namespace DesenvolvedorRelacional.Apresentacao.Essencial
{
    public class BarraRolagemVertical : IBase
    {
        private IBase privateBase { get; set; }
        public IBase Base
        {
            get => privateBase;
            set
            {
                privateBase = value;

                Barra.Tamanho = new Point(Tamanho.X, Base.Tamanho.Y > Tamanho.Y ? Base.Tamanho.Y - Tamanho.Y : Tamanho.Y);
                Location = new Point(value.Tamanho.X - Tamanho.X, 0);

                Base.Controls.Add(this);
            }
        }

        private Botao Barra { get; }
        private Botao Rolagem { get; }
        private Dictionary<Utilidade.TipoCor, Color> BarraCoresInteracaoMouse => Utilidade.PegarCoresInteracaoMouse(50, 50, 50);
        private Dictionary<Utilidade.TipoCor, Color> RolagemBarraCoresInteracaoMouse => Utilidade.PegarCoresInteracaoMouse();
        public new Point Posicao => Location;
        public new Point Tamanho
        {
            get => new Point(Size.Width, Size.Height);
            private set
            {
                Size = new Size(value.X, value.Y);

                Barra.Size = new Size(value.X, value.Y);
                Rolagem.Size = new Size(value.X, value.Y);
            }
        }
        public BarraRolagemVertical()
        {
            Barra = new Botao(Botao.TipoBotao.Basico)
            {
                BackColor = BarraCoresInteracaoMouse[Utilidade.TipoCor.CorNormal],
            };
            Controls.Add(Barra);
            Barra.MouseEnter += (s, e) =>
            {
                Barra.BackColor = BarraCoresInteracaoMouse[Utilidade.TipoCor.CorDestaque];
                //Rolagem.BackColor = RolagemBarraCoresInteracaoMouse[Utilidade.TipoCor.CorDestaque];
            };
            Barra.MouseLeave += (s, e) =>
            {
                Barra.BackColor = BarraCoresInteracaoMouse[Utilidade.TipoCor.CorNormal];
                //Rolagem.BackColor = RolagemBarraCoresInteracaoMouse[Utilidade.TipoCor.CorNormal];
            };
            Barra.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Barra.MousePosicaoAntiga = e.Location;
                }
            };
            Barra.MouseMove += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    //ajustar, ainda naum está bom
                    var novaPosicaoY = e.Y + Barra.Top - Barra.MousePosicaoAntiga.Y;
                    if (novaPosicaoY > 0 && novaPosicaoY < Parent.Size.Height - Tamanho.Y)
                    {
                        Barra.Top = novaPosicaoY;
                    }
                }
            };

            Rolagem = new Botao(Botao.TipoBotao.Basico)
            {
                BackColor = RolagemBarraCoresInteracaoMouse[Utilidade.TipoCor.CorNormal]
            };
            Controls.Add(Rolagem);

            Tamanho = new Point(30, 200);
        }
    }
}
