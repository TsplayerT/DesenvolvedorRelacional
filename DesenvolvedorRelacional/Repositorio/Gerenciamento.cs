using System.Data.SQLite;

namespace DesenvolvedorRelacional.Repositorio
{
    public class Gerenciamento
    {
        private SQLiteCommand SQLComando { get; }

        public Gerenciamento(Conectividade conectividade)
        {
            SQLComando = new SQLiteCommand
            {
                Connection = conectividade.SQLConexao
            };
        }

        public int PegarMenorIdDisponivel(string tabela)
        {
            //comando funciona no SQL mas no SQLite falta algo semelhante ao "spt_values"
            //SQLComando.CommandText = $"SELECT MIN(id) FROM master..spt_values WHERE TYPE = 'p' AND id<= (SELECT MAX(id) FROM {tabela}) AND id NOT IN (SELECT id FROM {tabela}) AND id != 0";
            //var novoId = (int?)SQLComando.ExecuteReader()[0];

            //if (novoId == null)
            {
                SQLComando.CommandText = $"SELECT MAX(id) + 1 FROM {tabela}";
                return SQLComando.ExecuteReader().GetInt32(0);
            }
            //return (int)novoId;
        }

        public void AdicionarTabela(string tabela)
        {
            Adicionar($"CREATE TABLE {tabela} (id INTEGER PRIMARY KEY)");
        }
        public void AdicionarColuna(string tabela, string coluna)
        {
            Adicionar($"ALTER TABLE {tabela} ADD COLUMN {coluna}");
        }
        public void AdicionarLinha(string tabela)
        {
            var novoId = PegarMenorIdDisponivel(tabela);
            Adicionar($"INSERT INTO {tabela} (id) VALUES ({novoId})");
        }
        //criar esse mesmo método com retorno Table/Grid/DataReader, string, bool(se deu certo ou naum) e array/vetor de valores retornados
        private void Adicionar(string linhaComando)
        {
            SQLComando.CommandText = linhaComando;
            //fazer tratamento de erro(try catch)
            SQLComando.ExecuteNonQuery();
        }
        //criar uma forma de condicional (where com and e or)
        public void Atualizar(string tabela, string coluna, int linha, object valor)
        {
            SQLComando.CommandText = $"UPDATE {tabela} SET {coluna} = '{valor}' WHERE id = {linha}";
            SQLComando.ExecuteNonQuery();
        }
    }
}
