namespace DesenvolvedorRelacional.Apresentacao.Base
{
    public class CampoDeslizante : IBase
    {
        public IBase Controle { get; set; }
        public IBase Linha { get; set; }
        public CampoDeslizante()
        {

        }
    }
}
