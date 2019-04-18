using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DesenvolvedorRelacional.Infraestrutura;

namespace DesenvolvedorRelacional.Repositorio.Base
{
    public class CampoEscolha : IBase
    {
        public Dictionary<Utilidade.TipoCor, Color> CoresInteracaoMouse => Utilidade.PegarCoresInteracaoMouse(100, 100, 100);

        public int AlturaItem => Tamanho.Y;
        private ObservableCollection<Botao> Items { get; }
        public List<string> ItemsTexto => Items.Select(x => x.Texto).ToList();
        public Botao ItemEscolhido { get; set; }
        private Botao BotaoExpandir { get; }
        private Panel PlanoEscolha { get; }
        public new Point Tamanho
        {
            get => new Point(Size.Width, Size.Height);
            set
            {
                Size = new Size(value.X, value.Y);
                BotaoExpandir.Tamanho = new Point(value.Y, value.Y);
                BotaoExpandir.Posicao = new Point(value.X - BotaoExpandir.Tamanho.X, 0);

                var tamanhoLabelTexto1 = TextRenderer.MeasureText(ItemEscolhido.Text, ItemEscolhido.Font);
                ItemEscolhido.Location = new Point((value.X - tamanhoLabelTexto1.Width) / 2, (value.Y - tamanhoLabelTexto1.Height) / 2);

                foreach (var item in Items)
                {
                    var tamanhoLabelTexto2 = TextRenderer.MeasureText(item.Text, item.Font);
                    item.Location = new Point((value.X - tamanhoLabelTexto2.Width) / 2, (value.Y - tamanhoLabelTexto2.Height) / 2);
                }
            }
        }
        public CampoEscolha()
        {
            ItemEscolhido = new Botao
            {
                Texto = string.Empty
            };
            PlanoEscolha = new Panel();
            Items = new ObservableCollection<Botao>();

            BotaoExpandir = new Botao
            {
                PossivelClicar = true,
                PossivelDestacarMouse = true,
                Texto = string.Empty
            };
            BotaoExpandir.MouseClick += (s, e) =>
            {
                PlanoEscolha.Visible = !PlanoEscolha.Visible;
            };
            Controls.Add(BotaoExpandir);

            PossivelMover = true;
            BackColor = CoresInteracaoMouse[Utilidade.TipoCor.CorFundo];
            Tamanho = new Point(200, 30);

            Items.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    var novoItem = new Botao
                    {
                        //PossivelClicar = true,
                        PossivelDestacarMouse = true,
                        Texto = s.ToString()
                    };
                    novoItem.MouseClick += (sender, args) =>
                    {
                        ItemEscolhido.Texto = novoItem.Texto;
                    };
                    Items.Add(novoItem);

                    //para ativar o "set" e reajustar o novo item 
                    Tamanho = Tamanho;

                    PlanoEscolha.Size = new Size(Tamanho.X, Items.Count * AlturaItem);
                    PlanoEscolha.Controls.Add(Items.Last());
                }
            };
        }
    }
}
