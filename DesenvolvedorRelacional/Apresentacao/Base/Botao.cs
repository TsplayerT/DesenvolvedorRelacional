using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DesenvolvedorRelacional.Apresentacao.Essencial;
using DesenvolvedorRelacional.Infraestrutura;

namespace DesenvolvedorRelacional.Apresentacao.Base
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
                Mascara.Size = new Size(value.X, value.Y);
                var tamanhoLabelTexto = TextRenderer.MeasureText(LabelTexto.Text, LabelTexto.Font);
                LabelTexto.Location = new Point((Tamanho.X - tamanhoLabelTexto.Width) / 2, (Tamanho.Y - tamanhoLabelTexto.Height) / 2);
            }
        }

        public  bool EstaSelecionado { get; set; }
        public new bool PossivelDestacarFundo { get; set; }
        internal Mascara Mascara { get; }
        private Point TamanhoDiminuirClicar { get; }

        public enum TipoBotao
        {
            Basico,
            Simples,
            Completo
        }

        public Botao(TipoBotao tipoBotao = TipoBotao.Completo)
        {
            LabelTexto = new Label();
            Mascara = new Mascara();
            Controls.Add(Mascara);

            switch (tipoBotao)
            {
                case TipoBotao.Completo:

                    PossivelDestacarFundo = true;
                    ImplementarEventos();

                    goto case TipoBotao.Simples;
                case TipoBotao.Simples:

                    Controls.Add(LabelTexto);
                    Texto = "Novo Botão";
                    ParentChanged += (s, e) =>
                    {
                        var listaBotoesControlPai = Parent.Controls.Cast<Control>().Where(x => x.GetType() == typeof(Botao)).ToList();
                        if (listaBotoesControlPai.Any(x => x != this))
                        {
                            Texto += listaBotoesControlPai.Count - 1;
                        }
                        Posicao = new Point((Parent.Size.Width - Tamanho.X) / 2, (Parent.Size.Height - Tamanho.Y) / 2);
                    };

                    goto case TipoBotao.Basico;
                case TipoBotao.Basico:

                    BackColor = CoresDestaqueFundo[Utilidade.TipoCor.CorNormal];
                    Tamanho = new Point(100, 30);
                    TamanhoDiminuirClicar = new Point((int)(Tamanho.X * 0.03), (int)(Tamanho.Y * 0.03));

                    break;
            }
        }
        public void ImplementarEventos()
        {
            Mascara.MouseEnter += (s, e) =>
            {
                if (PossivelDestacarFundo)
                {
                    BackColor = EstaDestacado ? CoresDestaqueFundo[Utilidade.TipoCor.CorDestaqueSelecionado] : CoresDestaqueFundo[Utilidade.TipoCor.CorDestaque];
                }
            };
            Mascara.MouseLeave += (s, e) =>
            {
                if (PossivelDestacarFundo)
                {
                    BackColor = EstaDestacado ? CoresDestaqueFundo[Utilidade.TipoCor.CorSelecionado] : CoresDestaqueFundo[Utilidade.TipoCor.CorNormal];
                }
            };
            Mascara.MouseDown += (s, e) =>
            {
                if (PossivelDestacarFundo && e.Button == MouseButtons.Left)
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
            Mascara.MouseUp += (s, e) =>
            {
                if (PossivelDestacarFundo && e.Button == MouseButtons.Left)
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
            Mascara.MouseClick += (s, e) =>
            {
                if (PossivelDestacarFundo)
                {
                    EstaSelecionado = !EstaSelecionado;
                }
            };
        }

        public void Clicar(bool tirarDestaque = false)
        {
            BackColor = CoresDestaqueFundo[Utilidade.TipoCor.CorSelecionado];
            EstaSelecionado = !EstaSelecionado;
            if (tirarDestaque)
            {
                BackColor = EstaSelecionado ? CoresDestaqueFundo[Utilidade.TipoCor.CorSelecionado] : CoresDestaqueFundo[Utilidade.TipoCor.CorNormal];
            }
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            Mascara.BringToFront();

            base.OnControlAdded(e);
        }
    }
}
