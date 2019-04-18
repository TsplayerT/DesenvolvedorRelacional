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
    }
}
