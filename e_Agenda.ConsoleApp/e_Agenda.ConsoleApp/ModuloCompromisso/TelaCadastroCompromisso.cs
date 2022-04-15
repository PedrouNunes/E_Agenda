using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace e_Agenda.ConsoleApp.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Compromisso> _repositorioCompromisso;
        private readonly Notificador _notificador;

        public TelaCadastroCompromisso(IRepositorio<Compromisso> repositorioCompromisso, Notificador notificador)
            : base("Cadastro de Compromisso")
        {
            _repositorioCompromisso = repositorioCompromisso;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Compromisso");

            Compromisso novoCompromisso = ObterCompromisso();

            _repositorioCompromisso.Inserir(novoCompromisso);

            _notificador.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Compromisso");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroGenero = ObterNumeroRegistro();

            Compromisso compromissoAtualizado = ObterCompromisso();

            bool conseguiuEditar = _repositorioCompromisso.Editar(numeroGenero, compromissoAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Compromisso editado com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum Compromisso cadastrado para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioCompromisso.Excluir(numeroCompromisso);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Compromisso excluído com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Compromissos Cadastrados");

            List<Compromisso> compromissos = _repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }

        private Compromisso ObterCompromisso()
        {
            Console.Write("Digite o assunto: ");
            string assunto = Console.ReadLine();

            Console.Write("Digite o local: ");
            string local = Console.ReadLine();

            Console.Write("Digite o dia: ");
            string dia = Console.ReadLine();

            DateTime diaCompromisso = Convert.ToDateTime(dia);

            Console.Write("Digite a hora de Inicio: ");
            string hrInicio = Console.ReadLine();

            Console.Write("Digite a hora de encerramento: ");
            string hrEncerramento = Console.ReadLine();

            return new Compromisso(assunto, local, diaCompromisso, hrInicio, hrEncerramento);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID do compromisso que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioCompromisso.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID do compromisso não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
