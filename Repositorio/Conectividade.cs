using System.Data;
using System.Data.SQLite;
using System.IO;

namespace DesenvolvedorRelacional.Repositorio
{
    public class Conectividade
    {
        public UtilidadeConexao LinhaConexao { get; }
        internal SQLiteConnection SQLConexao { get; }
        public Conectividade(bool conectar = true)
        {
            LinhaConexao = new UtilidadeConexao();
            SQLConexao = new SQLiteConnection(LinhaConexao.Linha());

            if (conectar)
            {
                Conectar();
            }
        }

        public bool Conectar()
        {
            SQLConexao.Open();
            return SQLConexao.State == ConnectionState.Open;
        }
        public bool Desconectar()
        {
            SQLConexao.Close();
            return SQLConexao.State == ConnectionState.Closed;
        }

        public class UtilidadeConexao
        {
            public string Diretorio { get; set; }
            public string NomeArquivo { get; set; }
            public string Extensao => "db";
            public string Senha { get; set; }
            public bool PossivelCriarArquivo { get; set; }
            public int Versao => 3;

            public UtilidadeConexao()
            {
                NomeArquivo = "MyDB";
                Senha = "god";
                var assembly = System.Reflection.Assembly.GetEntryAssembly();
                Diretorio = assembly.Location.Replace(assembly.ManifestModule.Name, string.Empty);
                PossivelCriarArquivo = true;
            }

            public string Linha()
            {
                return $" Data Source = {Path.Combine(Diretorio, $"{NomeArquivo}.{Extensao}")};" +
                       $" Version = {Versao};" +
                       $" New = {PossivelCriarArquivo};" +
                       $" Password = {Senha};";
            }
        }
    }
}
