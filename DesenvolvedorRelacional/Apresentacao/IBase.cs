using System.Drawing;
using System.Windows.Forms;

namespace DesenvolvedorRelacional.Apresentacao
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
            set
            {
                Size = new Size(value.X, value.Y);
                Mascara.Size = new Size(value.X, value.Y);
            }
        }
        protected internal Mascara Mascara { get; set; }
        public bool PossivelMover { get; set; }
        public bool PossivelDestacarMouse { get; set; }
        internal Point MousePosicaoAntiga { get; set; }

        protected IBase()
        {
            Mascara = new Mascara();
            Controls.Add(Mascara);

            Mascara.MouseEnter += (s, e) =>
            {
                if (PossivelDestacarMouse)
                {
                    Cursor = Cursors.Hand;
                }
            };
            Mascara.MouseLeave += (s, e) =>
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
            //provisórião
            Mascara.MouseDown += (s, e) =>
            {
                if (PossivelMover && e.Button == MouseButtons.Left)
                {
                    MousePosicaoAntiga = e.Location;
                }
            };
            //provisórião
            Mascara.MouseMove += (s, e) =>
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

        protected override void OnControlAdded(ControlEventArgs e)
        {
            Mascara.BringToFront();

            base.OnControlAdded(e);
        }

    }
}
