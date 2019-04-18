using System.ComponentModel;
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
                    base2.Left = eventArgs.X + base1.Left - base1.MousePosicaoAntiga.X + base2.Posicao.X - base1.Posicao.X;
                    base2.Top = eventArgs.Y + base1.Top - base1.MousePosicaoAntiga.Y + base2.Posicao.Y - base1.Posicao.Y;
                }
            };
            //corrigir
            base2.MouseMove += (sender, eventArgs) =>
            {
                if (base2.PossivelMover && eventArgs.Button == MouseButtons.Left)
                {
                    base1.Left = eventArgs.X + base2.Left - base1.MousePosicaoAntiga.X + base2.Posicao.X - base1.Posicao.X;
                    base1.Top = eventArgs.Y + base2.Top - base1.MousePosicaoAntiga.Y + base2.Posicao.Y - base1.Posicao.Y;
                }
            };
        }

        public static void RemoverEvento(string nomeEvento, IBase baseAlvo)
        {
            var f1 = typeof(Control).GetField(nomeEvento, BindingFlags.Static | BindingFlags.NonPublic);
            if (f1 != null)
            {
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
