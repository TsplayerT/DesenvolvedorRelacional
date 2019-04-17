using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DesenvolvedorRelacional.Repositorio.Base
{
    public class Botao : IBase
    {
        private Label LabelTexto { get; }

        public string Texto
        {
            get => LabelTexto.Text;
            set => LabelTexto.Text = value;
        }

        public new Point Tamanho
        {
            get => new Point(Size.Width, Size.Height);
            set
            {
                Size = new Size(value.X, value.Y);
                var tamanhoLabelTexto = TextRenderer.MeasureText(LabelTexto.Text, LabelTexto.Font);
                LabelTexto.Location = new Point((Tamanho.X - tamanhoLabelTexto.Width) / 2, (Tamanho.Y - tamanhoLabelTexto.Height) / 2);
            }
        }

        public bool Selecionado { get; set; }
        public Color CorFundo => Color.FromArgb(100, 100, 100);
        public Color CorFundoDestaque => Color.FromArgb(125, 125, 125);
        public Color CorFundoSelecionado => Color.FromArgb(175, 175, 175);
        public Color CorFundoDestaqueSelecionado => Color.FromArgb(150, 150, 150);

        public Botao()
        {
            LabelTexto = new Label
            {
                AutoSize = true
            };
            Controls.Add(LabelTexto);

            BackColor = CorFundo;
            Texto = "Novo Botão";
            Tamanho = new Point(100, 30);

            MouseClick += (sender, args) =>
            {
                BackColor = CorFundoSelecionado;
                Selecionado = !Selecionado;
            };
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            BackColor = Selecionado ? CorFundoSelecionado : CorFundoDestaque;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Selecionado ? CorFundoSelecionado : CorFundo;

            if (ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                return;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            var listaBotoesControlPai = Parent.Controls.Cast<Control>().Where(x => x.GetType() == typeof(Botao)).ToList();
            if (listaBotoesControlPai.Any(x => x != this))
            {
                Texto += listaBotoesControlPai.Count - 1;
            }
            Posicao = new Point((Parent.Size.Width - Tamanho.X) / 2, (Parent.Size.Height - Tamanho.Y) / 2);
        }
    }
}
