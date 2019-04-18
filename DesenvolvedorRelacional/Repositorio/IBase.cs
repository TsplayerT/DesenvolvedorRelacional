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

        private Point TamanhoDiminuirClicar { get; }
        public bool PossivelMover { get; set; }
        public bool PossivelClicar { get; set; }
        public bool PossivelDestacarMouse { get; set; }
        internal Point MousePosicaoAntiga { get; set; }

        protected IBase()
        {
            TamanhoDiminuirClicar = new Point((int)(Tamanho.X * 0.03), (int)(Tamanho.Y * 0.03));
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
                if (PossivelClicar && e.Button == MouseButtons.Left)
                {
                    Posicao = new Point(Posicao.X + TamanhoDiminuirClicar.X / 2, Posicao.Y + TamanhoDiminuirClicar.Y / 2);
                    Tamanho = new Point(Tamanho.X - TamanhoDiminuirClicar.X, Tamanho.Y - TamanhoDiminuirClicar.Y);
                    foreach (Control filho in Controls)
                    {
                        filho.Location = new Point(filho.Location.X - TamanhoDiminuirClicar.X / 2, filho.Location.Y - TamanhoDiminuirClicar.Y / 2);
                        //filho.Scale();
                    }
                }
            };
            MouseUp += (s, e) =>
            {
                if (PossivelClicar && e.Button == MouseButtons.Left)
                {
                    Posicao = new Point(Posicao.X - TamanhoDiminuirClicar.X / 2, Posicao.Y - TamanhoDiminuirClicar.Y / 2);
                    Tamanho = new Point(Tamanho.X + TamanhoDiminuirClicar.X, Tamanho.Y + TamanhoDiminuirClicar.Y);
                    foreach (Control filho in Controls)
                    {
                        filho.Location = new Point(filho.Location.X + TamanhoDiminuirClicar.X / 2, filho.Location.Y + TamanhoDiminuirClicar.Y / 2);
                        //filho.Scale();
                    }
                }
            };
            MouseMove += (s, e) =>
            {
                if (PossivelMover && e.Button == MouseButtons.Left)
                {
                    Left = e.X + Left - MousePosicaoAntiga.X;
                    Top = e.Y + Top - MousePosicaoAntiga.Y;
                }
            };
        }
    }
}
