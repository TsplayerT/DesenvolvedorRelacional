using System.Drawing;
using System.Windows.Forms;

namespace DesenvolvedorRelacional.Repositorio
{
    public abstract class IBase : Panel
    {
        public Point Posicao
        {
            get => Location;
            set => Location = value;
        }

        public Point Tamanho
        {
            get => new Point(Size.Width, Size.Height);
            set => Size = new Size(value.X, value.Y);
        }

        public bool PossivelMover { get; set; }
        public bool PossivelDestacarMouse { get; set; }
        internal Point MousePosicaoAntiga { get; set; }

        protected IBase()
        {
            MouseEnter += (s, e) =>
            {
                if (PossivelDestacarMouse)
                {
                    Cursor = Cursors.Hand;
                }
            };
            MouseLeave += (s, e) =>
            {
                if (PossivelDestacarMouse)
                {
                    Cursor = Cursors.Default;
                }
            };
            MouseDown += (s, e) =>
            {
                if (PossivelMover && e.Button == MouseButtons.Left)
                {
                    MousePosicaoAntiga = e.Location;
                }
            };
            MouseMove += (s, e) =>
            {
                if (PossivelMover && e.Button == MouseButtons.Left)
                {
                    var novaPosicaoX = e.X + Left - MousePosicaoAntiga.X;
                    //if (novaPosicaoX > 0 && novaPosicaoX < Parent.Size.Width - Tamanho.X)
                    {
                        Left = novaPosicaoX;
                    }
                    //ajustar, ainda naum está bom
                    var novaPosicaoY = e.Y + Top - MousePosicaoAntiga.Y;
                    //if (novaPosicaoY > 0 && novaPosicaoY < Parent.Size.Height - Tamanho.Y)
                    {
                        Top = novaPosicaoY;
                    }
                }
            };
        }
    }
}
