using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using DesenvolvedorRelacional.Apresentacao;

namespace DesenvolvedorRelacional.Infraestrutura
{
    public static class Utilidade
    {
        public enum TipoCor
        {
            CorNormal,
            CorDestaque,
            CorSelecionado,
            CorDestaqueSelecionado
        }
        public static void PerderFoco(this object sender)
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
        public static double ValorEntre(this double valor, double minimo, double maximo)
        {
            return valor < minimo ? minimo : valor > maximo ? maximo : valor;
        }
        public static Dictionary<TipoCor, Color> PegarCoresInteracaoMouse(int r = 100, int g = 100, int b = 100)
        {
            r = Convert.ToInt32(ValorEntre(r, 0, 180));
            g = Convert.ToInt32(ValorEntre(g, 0, 180));
            b = Convert.ToInt32(ValorEntre(b, 0, 180));

            return new Dictionary<TipoCor, Color>
            {
                { TipoCor.CorNormal, Color.FromArgb(r, g, b) },
                { TipoCor.CorDestaque, Color.FromArgb(r + 25, g + 25, b + 25) },
                { TipoCor.CorSelecionado, Color.FromArgb(r + 75, g + 75, b + 75) },
                { TipoCor.CorDestaqueSelecionado, Color.FromArgb(r + 50, g + 50, b + 50) }
            };

        }
        public static void SincronizarMovimentos(this IBase base1, IBase base2)
        {
            base1.MouseMove += (sender, eventArgs) =>
            {
                if (base2.PossivelMover && eventArgs.Button == MouseButtons.Left)
                {
                    var novaPosicaoX = eventArgs.X + base1.Left - base1.MousePosicaoAntiga.X + base2.Posicao.X - base1.Posicao.X;
                    //if (novaPosicaoX > 0 && novaPosicaoX < base1.Parent.Size.Width - base1.Tamanho.X)
                    {
                        base2.Left = novaPosicaoX;
                    }
                    //ajustar, ainda naum está bom
                    var novaPosicaoY = eventArgs.Y + base1.Top - base1.MousePosicaoAntiga.Y + base2.Posicao.Y - base1.Posicao.Y;
                    //if (novaPosicaoY > 0 && novaPosicaoY < base1.Parent.Size.Height - base1.Tamanho.Y)
                    {
                        base2.Top = novaPosicaoY;
                    }
                }
            };
            base2.MouseMove += (sender, eventArgs) =>
            {
                if (base2.PossivelMover && eventArgs.Button == MouseButtons.Left)
                {
                    var novaPosicaoX = eventArgs.X + base2.Left - base2.MousePosicaoAntiga.X + base1.Posicao.X - base2.Posicao.X;
                    //if (novaPosicaoX > 0 && novaPosicaoX < base1.Parent.Size.Width - base1.Tamanho.X)
                    {
                        base1.Left = novaPosicaoX;
                    }
                    //ajustar, ainda naum está bom
                    var novaPosicaoY = eventArgs.Y + base2.Top - base2.MousePosicaoAntiga.Y + base1.Posicao.Y - base2.Posicao.Y;
                    //if (novaPosicaoY > 0 && novaPosicaoY < base1.Parent.Size.Height - base1.Tamanho.Y)
                    {
                        base1.Top = novaPosicaoY;
                    }
                }
            };
        }

        public static void RemoverEvento(IBase baseAlvo, string nomeEvento)
        {
            var f1 = typeof(Control).GetField(nomeEvento, BindingFlags.Static | BindingFlags.NonPublic);
            //var f2 = new List<FieldInfo>();
            //foreach (var filed in typeof(Control).GetFields(BindingFlags.Static | BindingFlags.NonPublic))
            //{
            //    f2.Add(filed);
            //}

            //f2 =f2.Where(x => x.Name.Contains("Mouse")).ToList();
            /*
                [0]: "EventMouseDown"
                [1]: "EventMouseEnter"
                [2]: "EventMouseLeave"
                [3]: "EventMouseHover"
                [4]: "EventMouseMove"
                [5]: "EventMouseUp"
                [6]: "EventMouseWheel"
                [7]: "EventMouseClick"
                [8]: "EventMouseDoubleClick"
                [9]: "EventMouseCaptureChanged"
             */
            //foreach (var fieldInfo in f2)
            //{
            //    if (fieldInfo != null)
            if (f1 != null)
            {
                //var obj = fieldInfo.GetValue(baseAlvo);
                var obj = f1.GetValue(baseAlvo);
                var pi = baseAlvo.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                if (pi != null)
                {
                    var list = (EventHandlerList)pi.GetValue(baseAlvo, null);
                    list.RemoveHandler(obj, list[obj]);
                }
            }
        }

        public static void LeituraPropriedade(this object objeto, string nomeProriedade, bool apenasLeitura)
        {
            if (!string.IsNullOrEmpty(nomeProriedade) && objeto != null)
            {
                var descriptor = TypeDescriptor.GetProperties(objeto.GetType())[nomeProriedade];
                if (descriptor != null)
                {
                    var attrib = (ReadOnlyAttribute)descriptor.Attributes[typeof(ReadOnlyAttribute)];
                    var isReadOnly = attrib.GetType().GetField("isReadOnly", BindingFlags.NonPublic | BindingFlags.Instance);

                    if (isReadOnly != null)
                    {
                        isReadOnly.SetValue(attrib, apenasLeitura);
                    }
                }
            }
        }
    }
}

