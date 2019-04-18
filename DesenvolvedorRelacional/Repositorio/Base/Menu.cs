﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DesenvolvedorRelacional.Infraestrutura;

namespace DesenvolvedorRelacional.Repositorio.Base
{
    public class Menu : IBase
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
        //por enquanto só vincula com objetos do tipo Menu
        public void Vincular(int indexBotao, Menu menu)
        {
            var botaoVinculo = Botoes[indexBotao];
            this.SincronizarMovimentos(menu);
            menu.Posicao = new Point(botaoVinculo.Posicao.X + botaoVinculo.Tamanho.X + menu.TamanhoBorda * 2, botaoVinculo.Posicao.Y);
            menu.Visible = false;

            botaoVinculo.MouseClick += (s, e) =>
            {
                var botaoSelecionado = Botoes.Find(x => x.Selecionado && x != botaoVinculo);
                if (botaoSelecionado != null)
                {
                    botaoSelecionado.Clicar(true);
                }
                if (menu.Parent == null)
                {
                    Parent.Controls.Add(menu);
                }
                if (VinculoVisivel != null && VinculoVisivel != menu)
                {
                    VinculoVisivel.Visible = false;
                }
                VinculoVisivel = menu;

                menu.Visible = !menu.Visible;
            };
            botaoVinculo.PossivelClicar = true;
            botaoVinculo.PossivelDestacarMouse = true;
        }
    }
}
