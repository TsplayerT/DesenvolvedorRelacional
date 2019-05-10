using System.Collections.Generic;
using System.Drawing;
using DesenvolvedorRelacional.Apresentacao.Base;

namespace DesenvolvedorRelacional.Aplicacao
{
    public class MalhaQuadriculada
    {
        public int Espacamento { get; set; }
        public List<Botao> Blocos { get; }

        public MalhaQuadriculada(int quantidadeX, int quantidadeY, int largura, int altura)
        {
            Blocos = new List<Botao>();
            for (var y = 0; y < quantidadeY; y++)
            {
                for (var x = 0; x < quantidadeX; x++)
                {
                    var posicaoX = x * largura + Espacamento;
                    var posicaoY = y * altura + Espacamento;
                    var novoBloco = new Botao
                    {
                        PossivelMover = false,
                        PossivelDestacarFundo= false,
                        PossivelDestacarMouse = false,
                        Tamanho = new Point(largura, altura),
                        Posicao = new Point(posicaoX, posicaoY)
                    };

                    Blocos.Add(novoBloco);
                }
            }
        }
    }
}
