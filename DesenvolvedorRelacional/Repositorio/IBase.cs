using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
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
        public ObservableCollection<IBase> SincronizarMovimento { get; }

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
                    Left = e.X + Left - MousePosicaoAntiga.X;
                    Top = e.Y + Top - MousePosicaoAntiga.Y;
                }
            };

            SincronizarMovimento = new ObservableCollection<IBase>();
            SincronizarMovimento.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var novoIBase in e.NewItems.Cast<IBase>().ToList())
                    {
                        MouseMove += (sender, eventArgs) =>
                        {
                            if (PossivelMover && eventArgs.Button == MouseButtons.Left)
                            {
                                novoIBase.Left = eventArgs.X + Left - MousePosicaoAntiga.X + novoIBase.Posicao.X - Posicao.X;
                                novoIBase.Top = eventArgs.Y + Top - MousePosicaoAntiga.Y + novoIBase.Posicao.Y - Posicao.Y;
                            }
                        };
                        novoIBase.MouseMove += (sender, eventArgs) =>
                        {
                            if (PossivelMover && eventArgs.Button == MouseButtons.Left)
                            {
                                Left = eventArgs.X + novoIBase.Left - MousePosicaoAntiga.X + novoIBase.Posicao.X - Posicao.X;
                                Top = eventArgs.Y + novoIBase.Top - MousePosicaoAntiga.Y + novoIBase.Posicao.Y - Posicao.Y;
                            }
                        };
                    }
                }
            };
        }
    }
}
