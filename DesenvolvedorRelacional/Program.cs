using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DesenvolvedorRelacional.Repositorio.Base;

namespace DesenvolvedorRelacional
{
    class Program
    {
        static void Main(string[] args)
        {

            // removido
                
            //var conexao = new Conectividade();
            //var gerenciar = new Gerenciamento(conexao);
            //gerenciar.AdicionarTabela("primeira_tabela");
            //gerenciar.AdicionarColuna("primeira_tabela", "nova_coluna");
            //gerenciar.AdicionarLinha("primeira_tabela");
            //gerenciar.Atualizar("primeira_tabela", "nova_coluna", 1, "asdasd");

            var janela = new Form
            {
                Size = new Size(800, 600),
                StartPosition = FormStartPosition.CenterScreen
            };

            var menu = new Repositorio.Base.Menu
            {
                PossivelMover = true
            };
            menu.Botoes = new List<Botao>
            {
                new Botao(menu),
                new Botao(menu),
                new Botao(menu)
            };
            menu.Posicao = new Point(0, 10);
            janela.Controls.Add(menu);
            janela.ShowDialog();
        }
    }
}
