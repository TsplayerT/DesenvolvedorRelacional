using DesenvolvedorRelacional.Repositorio;

namespace DesenvolvedorRelacional.Apresentacao.Manutencao
{
    public class ManutencaoRepositorio: IBase
    {
        private Gerenciamento Gerenciamento { get; }
        public ManutencaoRepositorio(Gerenciamento gerenciamento)
        {
            Gerenciamento = gerenciamento;
        }

    }
}
