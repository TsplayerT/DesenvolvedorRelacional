using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DesenvolvedorRelacional.Infraestrutura;

namespace DesenvolvedorRelacional.Apresentacao.Base
{
    public class CampoTexto : IBase
    {
        public Color CorTextoInvalido => Color.FromArgb(250, 50, 50);
        public Dictionary<Utilidade.TipoCor, Color> CoresInteracaoTexto => Utilidade.PegarCoresInteracaoMouse();
        public Dictionary<Utilidade.TipoCor, Color> CoresInteracaoFundo => Utilidade.PegarCoresInteracaoMouse(100, 100, 100);
        private Control ControlPai { get; set; }
        private Label LabelTexto { get; }
        public Font TextoFonte
        {
            get => LabelTexto.Font;
            set => LabelTexto.Font = new Font(value, LabelTexto.Font.Style);
        }
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
                LabelTexto.Size = new Size(value.X, value.Y);
                LabelTexto.Font = new Font(LabelTexto.Font.FontFamily, (float)(value.Y * 0.5));
            }
        }
        public CampoTexto()
        {
            LabelTexto = new Label();
            Controls.Add(LabelTexto);

            Tamanho = new Point(300, 30);
            BackColor = CoresInteracaoFundo[Utilidade.TipoCor.CorFundo];

            ImplementarEventos();
        }

        protected override void OnCreateControl()
        {
            ControlPai = Parent;

            base.OnCreateControl();
        }

        private void ImplementarEventos()
        {
            Mascara.MouseClick += (s, e) =>
            {
                Focus();
                TrocarCorTexto();
                BackColor = CoresInteracaoFundo[Utilidade.TipoCor.CorFundoDestaque];
            };

            KeyDown += (s, e) =>
            {
                switch (e.KeyCode)
                {
                    case Keys.Back:
                        if (Texto.Length > 0)
                        {
                            //corrigir
                            //if (tamanhoLabelTexto.Width * 1.9 < Tamanho.X)
                            //{
                            //    LabelTexto.Font = new Font(LabelTexto.Font.FontFamily, LabelTexto.Font.Size * 2);
                            //}

                            Texto = Texto.Remove(Texto.Length - 1);
                            TrocarCorTexto();
                        }

                        break;
                    case Keys.Enter:
                        PerderFoco(this);
                        TrocarCorTexto();
                        BackColor = CoresInteracaoFundo[Utilidade.TipoCor.CorFundo];
                        //Texto = Texto.Remove(Texto.Length - 1);
                        //SendKeys.Send("{TAB}");
                        break;
                    case Keys.Space:
                        Texto += " ";
                        break;
                    case Keys.D1:
                        Texto += e.Shift ? "!" : "1";
                        break;
                    case Keys.D2:
                        Texto += e.Shift ? "@" : "2";
                        break;
                    case Keys.D3:
                        Texto += e.Shift ? "#" : "3";
                        break;
                    case Keys.D4:
                        Texto += e.Shift ? "$" : "4";
                        break;
                    case Keys.Alt:
                    case Keys.Shift:
                    case Keys.ShiftKey:
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                    case Keys.Control:
                    case Keys.ControlKey:
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                        break;
                    default:
                        TrocarCorTexto();

                        var tamanhoLabelTexto = TextRenderer.MeasureText(LabelTexto.Text, LabelTexto.Font);
                        if (tamanhoLabelTexto.Width < Tamanho.X * 0.9)
                        {
                            Texto += e.KeyCode;
                        }
                        //necessário?
                        //if (tamanhoLabelTexto.Width > Tamanho.X * 0.9)
                        //{
                        //    //Texto += Environment.NewLine;
                        //    //LabelTexto.Font = new Font(LabelTexto.Font.FontFamily, (float)(LabelTexto.Font.Size * 0.5));
                        //}
                        break;
                }
            };
        }

        public void TrocarCorTexto()
        {
            var tamanhoLabelTexto = TextRenderer.MeasureText(LabelTexto.Text, LabelTexto.Font);

            if (tamanhoLabelTexto.Width > Tamanho.X * 0.9 && Focused)
            {
                LabelTexto.ForeColor = CorTextoInvalido;
            }
            else if (Focused)
            {
                LabelTexto.ForeColor = CoresInteracaoTexto[Utilidade.TipoCor.CorFundoDestaqueSelecionado];
            }
            else
            {
                LabelTexto.ForeColor = CoresInteracaoTexto[Utilidade.TipoCor.CorFundo];
            }
        }
        public void PerderFoco(object sender)
        {
            var control = sender as Control;

            if (control != null)
            {
                var isTabStop = control.TabStop;

                control.TabStop = false;
                control.Enabled = false;
                control.Enabled = true;
                control.TabStop = isTabStop;
            }
        }
    }
}
