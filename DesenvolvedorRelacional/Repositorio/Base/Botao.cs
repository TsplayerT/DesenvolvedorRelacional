using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DesenvolvedorRelacional.Infraestrutura;

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
        private Point TamanhoDiminuirClicar { get; }
        public bool EstaSelecionado { get; set; }
        public bool PossivelClicar { get; set; }
        public Dictionary<Utilidade.TipoCor, Color> CoresInteracaoMouse { get; set; }

        public enum TipoBotao
        {
            Basico,
            Simples,
            Completo
        }

        public Botao(TipoBotao tipoBotao = TipoBotao.Completo)
        {
            switch (tipoBotao)
            {
                case TipoBotao.Completo:

                    PossivelClicar = true;
                    PossivelDestacarMouse = true;
                    ImplementarEventos();

                    goto case TipoBotao.Simples;
                case TipoBotao.Simples:

                    LabelTexto = new Label();
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

                    CoresInteracaoMouse = Utilidade.PegarCoresInteracaoMouse(100, 100, 100);
                    BackColor = CoresInteracaoMouse[Utilidade.TipoCor.CorFundo];
                    Tamanho = new Point(100, 30);
                    TamanhoDiminuirClicar = new Point((int)(Tamanho.X * 0.03), (int)(Tamanho.Y * 0.03));

                    break;
            }
        }

        public void ImplementarEventos()
        {
            MouseEnter += (s, e) =>
            {
                if (PossivelClicar)
                {
                    BackColor = EstaSelecionado ? CoresInteracaoMouse[Utilidade.TipoCor.CorFundoDestaqueSelecionado] : CoresInteracaoMouse[Utilidade.TipoCor.CorFundoDestaque];
                }
            };
            MouseLeave += (s, e) =>
            {
                if (PossivelClicar)
                {
                    BackColor = EstaSelecionado ? CoresInteracaoMouse[Utilidade.TipoCor.CorFundoSelecionado] : CoresInteracaoMouse[Utilidade.TipoCor.CorFundo];
                }
            };
            MouseDown += (s, e) =>
            {
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
            MouseClick += (s, e) =>
            {
                if (PossivelClicar)
                {
                    EstaSelecionado = !EstaSelecionado;
                }
            };
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                return;
            }
            base.OnMouseLeave(e);
        }

        public void Clicar(bool tirarDestaque = false)
        {
            BackColor = CoresInteracaoMouse[Utilidade.TipoCor.CorFundoSelecionado];
            EstaSelecionado = !EstaSelecionado;
            if (tirarDestaque)
            {
                BackColor = EstaSelecionado ? CoresInteracaoMouse[Utilidade.TipoCor.CorFundoSelecionado] : CoresInteracaoMouse[Utilidade.TipoCor.CorFundo];
            }
        }
    }
}
