using System;
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
        internal Point MousePosicaoAntiga { get; set; }
        public bool PossivelDestacarMouse { get; set; }
        public IBase SincronizarMovimento { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (PossivelDestacarMouse)
            {
                Cursor = Cursors.Hand;
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (PossivelDestacarMouse)
            {
                Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (PossivelMover && e.Button == MouseButtons.Left)
            {
                MousePosicaoAntiga = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (PossivelMover && e.Button == MouseButtons.Left)
            {
                Left = e.X + Left - MousePosicaoAntiga.X;
                Top = e.Y + Top - MousePosicaoAntiga.Y;

                //fazer funcionar em Utilidade.SincronizarMoviementos
                //if (SincronizarMovimento != null)
                //{
                //    SincronizarMovimento.Left = e.X + Left - MousePosicaoAntiga.X + SincronizarMovimento.Posicao.X - Posicao.X;
                //    SincronizarMovimento.Top = e.Y + Top - MousePosicaoAntiga.Y + SincronizarMovimento.Posicao.Y - Posicao.Y;
                //}
            }
        }
    }
}
