using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.ModuloCompromisso
{
    public class Compromisso : EntidadeBase
    {
        public string Assunto { get; set; }

        public string Local { get; set; }

        public DateTime Data { get; set; }

        public string HoraInicio { get; set; }

        public string HoraTermino { get; set; }

        public Compromisso(string assunto, string local, DateTime data, string horaInicio, string horaTermino)
        {
            Assunto = assunto;
            Local = local;
            Data = data;
            HoraInicio = horaInicio;
            HoraTermino = horaTermino;
        }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Assunto: " + Assunto + Environment.NewLine +
                "Local: " + Local + Environment.NewLine;
        }
}
}
