using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DesenvolvedorRelacional.Infraestrutura;

namespace DesenvolvedorRelacional.Apresentacao.Base
{
    public class MenuLista : IBase
    {
        private IBase VinculoVisivel { get; set; }
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
                    botaoAtual.PossivelClicar = false;
                    botaoAtual.PossivelDestacarMouse = false;

                    botaoAtual.Posicao = new Point(TamanhoBorda, tamanhoTotal);
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

        public MenuLista()
        {
            BackColor = CorFundo;
            PossivelMover = true;
        }
        //por enquanto, só vincula com objetos do tipo MenuLista
        public void Vincular(int indexBotao, MenuLista menuLista)
        {
            var botaoVinculo = Botoes[indexBotao];
            this.SincronizarMovimentos(menuLista);
            menuLista.Posicao = new Point(botaoVinculo.Posicao.X + botaoVinculo.Tamanho.X + menuLista.TamanhoBorda * 2, botaoVinculo.Posicao.Y);
            menuLista.Visible = false;

            botaoVinculo.PossivelClicar = true;
            PossivelMover = true;
            menuLista.PossivelMover = true;
            botaoVinculo.Mascara.MouseClick += (s, e) =>
            {
                var botaoSelecionado = Botoes.Find(x => x.EstaSelecionado && x != botaoVinculo);
                if (botaoSelecionado != null)
                {
                    botaoSelecionado.Clicar(true);
                }
                if (menuLista.Parent == null)
                {
                    Parent.Controls.Add(menuLista);
                }
                if (VinculoVisivel != null && VinculoVisivel != menuLista)
                {
                    VinculoVisivel.Visible = false;
                }
                VinculoVisivel = menuLista;

                menuLista.Visible = !menuLista.Visible;
            };
        }
    }
}
