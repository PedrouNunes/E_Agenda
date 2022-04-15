using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace e_Agenda.ConsoleApp.ModuloContato
{
    public class Contato : EntidadeBase
    {
        public string Telefone { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Empresa { get; set; }

        public string Cargo { get; set; }

        public Contato(string telefone, string nome, string email, string empresa, string cargo)
        {
        Telefone = telefone;
        Nome = nome;
        Email = email;
        Empresa = empresa;
        Cargo = cargo;
        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
           "Nome: " + Nome + Environment.NewLine +
            "Telefone: " + Telefone + Environment.NewLine +
            "Email: " + Email + Environment.NewLine +
            "Empresa: " + Empresa + Environment.NewLine +
            "Cargo: " + Cargo;
        }
    }
}
