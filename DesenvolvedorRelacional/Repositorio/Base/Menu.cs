using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DesenvolvedorRelacional.Repositorio.Base
{
    public class Menu : IBase
    {
        public int TamanhoBorda => 10;
        public Color CorFundo => Color.FromArgb(75, 75, 75);
        public List<Botao> Botoes
        {
            get => Controls.Cast<Control>().Where(x => x.GetType() == typeof(Botao)).Cast<Botao>().ToList();
            set
            {
                Controls.Clear();
                Controls.AddRange(value.Cast<Control>().ToArray());

                var tamanhoTotal = TamanhoBorda;
                foreach (var botaoAtual in value)
                {
                    botaoAtual.Posicao = new Point(10, tamanhoTotal);
                    if (botaoAtual != value.Last())
                    {
                        tamanhoTotal += TamanhoBorda + botaoAtual.Tamanho.Y;
                    }
                }

                if (value.Any())
                {
                    Size = new Size(value.Max(x => x.Tamanho.X) + TamanhoBorda * 2, tamanhoTotal + value.Last().Tamanho.Y + TamanhoBorda);
                }
            }
        }

        public Menu()
        {
            BackColor = CorFundo;
        }
    }
}
