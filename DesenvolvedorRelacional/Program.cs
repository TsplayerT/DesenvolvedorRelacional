using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DesenvolvedorRelacional.Apresentacao.Base;
using DesenvolvedorRelacional.Apresentacao.Essencial;

namespace DesenvolvedorRelacional
{
    public class Program
    {
        //private static void Main(string[] args)
        private static void Main()
        {
            //var conexao = new Conectividade();
            //var gerenciar = new Gerenciamento(conexao);
            //gerenciar.AdicionarTabela("primeira_tabela");
            //gerenciar.AdicionarColuna("primeira_tabela", "nova_coluna");
            //gerenciar.AdicionarLinha("primeira_tabela");
            //gerenciar.Atualizar("primeira_tabela", "nova_coluna", 1, "asdasd");

            var barraRolagemVertical = new BarraRolagemVertical();

            var menuu = new MenuLista
            {
                Botoes = new List<Botao>
                {
                    new Botao(),
                    new Botao(),
                    new Botao(),
                    new Botao(),
                    new Botao(),
                    new Botao()
                },
                Posicao = new Point(700, 0)
            };

            var barraRolagemVertical2 = new BarraRolagemVertical
            {
                Base = menuu
            };

            var campoTexto = new CampoTexto
            {
                //PossivelMover = true,
                //PossivelDestacarMouse = true,
                Posicao = new Point(200, 450)
            };
            var botaoTeste = new Botao
            {
                PossivelMover = true,
                PossivelDestacarFundo = true,
                PossivelDestacarMouse = true,
                Posicao = new Point(400, 500)
            };

            var campoEscolha = new CampoEscolha
            {
                Posicao = new Point(200, 200)
            };

            var menu = new MenuLista
            {
                PossivelMover = true,
                Botoes = new List<Botao>
                {
                    new Botao(),
                    new Botao(),
                    new Botao()
                },
                Posicao = new Point(0, 10)
            };
            var menu2 = new MenuLista
            {
                PossivelMover = true,
                Botoes = new List<Botao>
                {
                    new Botao(),
                    new Botao()
                }
            };
            var menu3 = new MenuLista
            {
                PossivelMover = true,
                Botoes = new List<Botao>
                {
                    new Botao()
                }
            };
            menu.Vincular(0, menu2);
            menu.Vincular(1, menu3);
            var janela = new Form
            {
                Size = new Size(800, 600),
                StartPosition = FormStartPosition.CenterScreen
            };
            janela.Controls.Add(botaoTeste);
            janela.Controls.Add(menu);
            janela.Controls.Add(campoEscolha);
            janela.Controls.Add(campoTexto);
            janela.Controls.Add(menuu);
            janela.Controls.Add(barraRolagemVertical);
            janela.ShowDialog();
        }
    }
}
