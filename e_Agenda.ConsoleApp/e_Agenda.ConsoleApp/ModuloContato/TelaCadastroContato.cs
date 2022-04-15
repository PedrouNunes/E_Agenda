using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;


namespace e_Agenda.ConsoleApp.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Contato> _repositorioContato;
        private readonly Notificador _notificador;

        public TelaCadastroContato(IRepositorio<Contato> repositorioContato, Notificador notificador)
            : base("Cadastro de Contatos")
        {
            _repositorioContato = repositorioContato;
            _notificador = notificador;
        }
        
        public void Inserir()
        {
            MostrarTitulo("Cadastro de Contato");

            Contato novoContato = ObterContato();

            _repositorioContato.Inserir(novoContato);

            _notificador.ApresentarMensagem("Contato cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Contato");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma contatos cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Contato contatoAtualizada = ObterContato();

            bool conseguiuEditar = _repositorioContato.Editar(numeroGenero, contatoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Contato editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Contato");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum contatos cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioContato.Excluir(numeroContato);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Contato excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contatos Cadastrados");

            List<Contato> contatos = _repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum contatos disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        private Contato ObterContato()
        {
            string numero;
            do { 
            Console.Write("Digite o numero de telefone: ");
            numero = Console.ReadLine();
            } while (numero.Length < 9);

            Console.Write("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email do contato: ");
            string email = Console.ReadLine();

            Console.Write("Digite o nome da empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            return new Contato(numero, nome, email, empresa, cargo);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do contato que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioContato.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID d contato não foi encontrada, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
