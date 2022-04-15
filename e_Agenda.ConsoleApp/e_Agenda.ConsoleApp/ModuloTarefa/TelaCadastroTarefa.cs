using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Tarefa> _repositorioTarefa;
        private readonly Notificador _notificador;

        public TelaCadastroTarefa(IRepositorio<Tarefa> repositorioTarefa, Notificador notificador)
            : base("Cadastro de Tarefas")
        {
            _repositorioTarefa = repositorioTarefa;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Tarefa");

            Tarefa novoTarefa = ObterTarefa();

            _repositorioTarefa.Inserir(novoTarefa);

            _notificador.ApresentarMensagem("Tarefa cadastradra com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Tarefa");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Tarefa TarefaAtualizada = ObterTarefa();

            bool conseguiuEditar = _repositorioTarefa.Editar(numeroGenero, TarefaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioTarefa.Excluir(numeroTarefa);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela") 
                MostrarTitulo("Visualização de Tarefas Cadastradas");

            List<Tarefa> tarefas = _repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }

        private Tarefa ObterTarefa()
        {
            string relevancia;
            do {
                Console.Write("Digite a relevância da tarefa (Alta/Normal/Baixa): ");
                relevancia = Console.ReadLine();
            } while (relevancia != "Alta" && relevancia != "Normal" && relevancia != "Baixa");

            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a data de criação da tarefa: ");
            string data = Console.ReadLine();

            Console.Write("Digite  a data de conclusão da tarefa: ");
            string data2 = Console.ReadLine();

            Console.Write("Digite o percentual feito da tarefa (%) ");
            decimal percentual = Convert.ToDecimal(Console.ReadLine());

            DateTime dataCriacao = Convert.ToDateTime(data);
            DateTime dataConclusao = Convert.ToDateTime(data2);

            return new Tarefa(relevancia, titulo, dataCriacao, dataConclusao, percentual);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da tarefa que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioTarefa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da tarefa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}