using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DesenvolvedorRelacional.Repositorio;

namespace DesenvolvedorRelacional.Infraestrutura
{
    public static class Utilidade
    {
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

        public static void RemoverEvento(string nomeEvento, IBase baseAlvo)
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
    }
}

