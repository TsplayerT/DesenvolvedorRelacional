using DesenvolvedorRelacional.Repositorio;

namespace DesenvolvedorRelacional.Apresentacao.Manutencao
{
    public class TelaManutencaoRepositorio: IBase
    {
        private Gerenciamento Gerenciamento { get; }
        public TelaManutencaoRepositorio(Gerenciamento gerenciamento)
        {
            Gerenciamento = gerenciamento;
        }

    }
}
