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

            var menu = new Repositorio.Base.Menu
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
            var menu2 = new Repositorio.Base.Menu
            {
                PossivelMover = true,
                Botoes = new List<Botao>
                {
                    new Botao(),
                    new Botao()
                }
            };
            var menu3 = new Repositorio.Base.Menu
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
            janela.Controls.Add(menu);
            janela.ShowDialog();
        }
    }
}
