using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DesenvolvedorRelacional.Aplicacao
{
    public class ListaPrivada<T> where T : new()
    {
        private ObservableCollection<T> ListaValores { get; }

        public ListaPrivada(params T[] valores)
        {
            ListaValores = new ObservableCollection<T>(valores);
        }
        protected internal void Adicionar(params T[] valores)
        {
            foreach (var valor in valores)
            {
                ListaValores.Add(valor);
            }
        }
        protected void AdicionarValorNoIndice(int index, T objeto)
        {
            ListaValores[index] = objeto;
        }
        public T[] PegarTodosValores()
        {
            return ListaValores.ToArray();
        }
        public T PegarValor(int index)
        {
            return index < 0 || index > ListaValores.Count ? new T() : ListaValores[index];
        }
    }
}
