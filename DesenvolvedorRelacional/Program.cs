using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DesenvolvedorRelacional.Infraestrutura;
using DesenvolvedorRelacional.Repositorio.Base;

namespace DesenvolvedorRelacional
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //var conexao = new Conectividade();
            //var gerenciar = new Gerenciamento(conexao);
            //gerenciar.AdicionarTabela("primeira_tabela");
            //gerenciar.AdicionarColuna("primeira_tabela", "nova_coluna");
            //gerenciar.AdicionarLinha("primeira_tabela");
            //gerenciar.Atualizar("primeira_tabela", "nova_coluna", 1, "asdasd");

            var botao1 = new Botao();
            var botao2 = new Botao();
            var listaBotoes = new List<Botao>
            {
                botao1,
                botao2,
                new Botao()
            };
            var listaBotoes2 = new List<Botao>
            {
                new Botao(),
                new Botao()
            };
            var menu = new Repositorio.Base.Menu
            {
                PossivelMover = true,
                Botoes = listaBotoes,
                Posicao = new Point(0, 10)
            };
            var menu2 = new Repositorio.Base.Menu
            {
                PossivelMover = true,
                Botoes = listaBotoes2
            };
            var menu3 = new Repositorio.Base.Menu
            {
                PossivelMover = true,
                Botoes = new List<Botao>
                {
                    new Botao()
                }
            };
            botao1.Vincular(menu2);
            botao2.Vincular(menu3);
            var janela = new Form
            {
                Size = new Size(800, 600),
                StartPosition = FormStartPosition.CenterScreen
            };
            menu.SincronizarMovimentos(menu2);
            janela.Controls.Add(menu);
            janela.Controls.Add(menu2);
            janela.Controls.Add(menu3);
            janela.ShowDialog();
        }
    }
}
