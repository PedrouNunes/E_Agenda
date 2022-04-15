using e_Agenda.ConsoleApp.Compartilhado;
using System;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        public string Relevancia { get; set; }

        public string Titulo { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime DataConclusao { get; set; }

        public decimal Percentual { get; set; } = 0m;

        public Tarefa(string relevancia, string titulo, DateTime dataCriacao, DateTime dataConclusao, decimal percentual)
        {
            Relevancia = relevancia;
            Titulo = titulo;
            DataCriacao = dataCriacao;
            DataConclusao = dataConclusao;
            Percentual = percentual;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
           "Relevância: " + Relevancia + Environment.NewLine +
            "Titulo do tarefa: " + Titulo + Environment.NewLine +
            "Data de criação: " + DataCriacao + Environment.NewLine +
            "Data de conclusao: " + DataConclusao + Environment.NewLine +
            "Percentual Concluido: " + Percentual;
        }
    }
}
