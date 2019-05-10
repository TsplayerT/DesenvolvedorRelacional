using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DesenvolvedorRelacional.Aplicacao;
using DesenvolvedorRelacional.Infraestrutura;

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
            set => Size = new Size(value.X, value.Y);
        }
        public ListaPrivada<TipoImplementacao> Implementacoes { get; }
        public bool PossivelMover { get; set; }
        public bool EstaDestacado { get; set; }
        public bool PossivelDestacarFundo { get; set; }
        public bool PossivelDestacarMouse { get; set; }
        public Dictionary<Utilidade.TipoCor, Color> CoresDestaqueFundo { get; set; }
        internal Point MousePosicaoAntiga { get; set; }

        protected IBase()
        {
            Implementacoes = new ListaPrivada<TipoImplementacao>();
            CoresDestaqueFundo = Utilidade.PegarCoresInteracaoMouse();

            ImplementarEventosMover(this);
            ImplementarEventosDestaqueFundo(this);
            ImplementarEventosDestaqueMouse(this);
        }
        public enum TipoImplementacao
        {
            Mover,
            DestacarFundo,
            DestacarMouse
        }

        public void ImplementarEventosMover(IBase objeto)
        {
            Implementacoes.Adicionar(TipoImplementacao.Mover);

            objeto.MouseDown += (s, e) =>
            {
                objeto.Focus();
                if (objeto.PossivelMover && e.Button == MouseButtons.Left)
                {
                    objeto.MousePosicaoAntiga = e.Location;
                }
            };
            objeto.MouseMove += (s, e) =>
            {
                if (objeto.PossivelMover && e.Button == MouseButtons.Left)
                {
                    var novaPosicaoX = e.X + objeto.Left - objeto.MousePosicaoAntiga.X;
                    //if (novaPosicaoX > 0 && novaPosicaoX < Parent.Size.Width - Tamanho.X)
                    {
                        objeto.Left = novaPosicaoX;
                    }
                    //ajustar, ainda naum está bom
                    var novaPosicaoY = e.Y + objeto.Top - objeto.MousePosicaoAntiga.Y;
                    //if (novaPosicaoY > 0 && novaPosicaoY < Parent.Size.Height - Tamanho.Y)
                    {
                        objeto.Top = novaPosicaoY;
                    }
                }
            };
        }

        public void ImplementarEventosDestaqueFundo(IBase objeto)
        {
            Implementacoes.Adicionar(TipoImplementacao.DestacarFundo);

            objeto.MouseEnter += (s, e) =>
            {
                if (objeto.PossivelDestacarFundo)
                {
                    objeto.BackColor = objeto.EstaDestacado ? objeto.CoresDestaqueFundo[Utilidade.TipoCor.CorDestaqueSelecionado] : objeto.CoresDestaqueFundo[Utilidade.TipoCor.CorDestaque];
                }
            };
            objeto.MouseLeave += (s, e) =>
            {
                if (objeto.PossivelDestacarFundo)
                {
                    objeto.BackColor = objeto.EstaDestacado ? objeto.CoresDestaqueFundo[Utilidade.TipoCor.CorDestaqueSelecionado] : objeto.CoresDestaqueFundo[Utilidade.TipoCor.CorDestaque];
                }
            };
        }
        public void ImplementarEventosDestaqueMouse(IBase objeto)
        {
            Implementacoes.Adicionar(TipoImplementacao.DestacarMouse);

            objeto.MouseEnter += (s, e) =>
            {
                if (objeto.PossivelDestacarMouse)
                {
                    objeto.Cursor = Cursors.Hand;
                }
            };
            objeto.MouseLeave += (s, e) =>
            {
                if (objeto.PossivelDestacarMouse)
                {
                    objeto.Cursor = Cursors.Default;
                }
            };
        }
    }
}
