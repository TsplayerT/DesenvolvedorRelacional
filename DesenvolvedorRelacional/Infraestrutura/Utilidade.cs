using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using DesenvolvedorRelacional.Repositorio;

namespace DesenvolvedorRelacional.Infraestrutura
{
    public static class Utilidade
    {
        public static void SincronizarMovimentos(this IBase baseDestino, IBase baseOrigem)
        {
            //sem motivo mas nao funciona
            baseOrigem.MouseMove += (s, e) =>
            {
                if (baseDestino.PossivelMover && e.Button == MouseButtons.Left)
                {
                    baseDestino.Left = e.X + baseOrigem.Left - baseOrigem.MousePosicaoAntiga.X + baseDestino.Posicao.X - baseOrigem.Posicao.X;
                    baseDestino.Top = e.Y + baseOrigem.Top - baseOrigem.MousePosicaoAntiga.Y + baseDestino.Posicao.Y - baseOrigem.Posicao.Y;
                }
            };
        }

        public static void RemoverEvento(string nomeEvento, IBase b)
        {
            var f1 = typeof(Control).GetField(nomeEvento, BindingFlags.Static | BindingFlags.NonPublic);
            if (f1 != null)
            {
                var obj = f1.GetValue(b);
                var pi = b.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                if (pi != null)
                {
                    var list = (EventHandlerList)pi.GetValue(b, null);
                    list.RemoveHandler(obj, list[obj]);
                }
            }
        }
    }
}
