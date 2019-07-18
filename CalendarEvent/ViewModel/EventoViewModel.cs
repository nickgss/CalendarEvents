using CalendarEvent.ApplicationService;
using CalendarEvent.Model;
using CalendarEvent.Model.Db;
using CalendarEvent.Utils;
using Db;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarEvent.ViewModel
{
    class EventoViewModel : Bindable
    {
        // DAO para acesso a dados.
        EventoDao dao;

        public IWindowServices WindowServices { get; set; }

        // Lista de eventos
        private List<Evento> eventos;
        public List<Evento> Eventos
        {
            get { return eventos; }
            set { SetValue(ref eventos, value); }
        }

        // Evento sendo inserido ou alterado.
        private Evento evento;
        public Evento Evento
        {
            get { return evento; }
            set { SetValue(ref evento, value); }
        }

        // Índice que corresponde ao evento selecionado na lista de eventos.
        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                SetValue(ref selectedIndex, value);

                if (selectedIndex >= 0)
                {
                    // Atribui à propriedade Evento o objeto evento selecionado.
                    Evento = Eventos[selectedIndex];
                }

                // Ajusta o estado dos comandos.
                NovoEventoCommand.CanExecute = IsViewing;
                ExcluirEventoCommand.CanExecute = IsViewing;
                EditarEventoCommand.CanExecute = IsViewing;
            }
        }

        // Indica se o evento está em modo de edição.
        bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                SetValue(ref isEditing, value);

                // Ajusta o estado dos comandos.
                NovoEventoCommand.CanExecute = IsViewing;
                ExcluirEventoCommand.CanExecute = IsViewing && selectedIndex >= 0;
                EditarEventoCommand.CanExecute = IsViewing && SelectedIndex >= 0;
                CancelarEdicaoEventoCommand.CanExecute = isEditing;
                GravarEventoCommand.CanExecute = isEditing;

                // Notifica as properties IsEditing e IsViewing para serem reavaliadas.
                OnPropertyChanged("IsViewing");
            }
        }

        // Parte do nome para pesquisar.
        private string textoPesquisa;
        public string TextoPesquisa
        {
            get { return textoPesquisa; }
            set { SetValue(ref textoPesquisa, value); }
        }

        // Indica se o evento está em modo de visualização.
        public bool IsViewing
        {
            get { return !IsEditing; }
        }

        // Comandos.
        public Command NovoEventoCommand { get; set; }
        public Command ExcluirEventoCommand { get; set; }
        public Command EditarEventoCommand { get; set; }
        public Command GravarEventoCommand { get; set; }
        public Command CancelarEdicaoEventoCommand { get; set; }
        public Command SairCommand { get; set; }
        public Command PesquisarCommand { get; set; }

        // Construtor.
        public EventoViewModel()
        {
            try
            {
                NovoEventoCommand = new Command(NovoEvento);
                ExcluirEventoCommand = new Command(ExcluirEvento);
                EditarEventoCommand = new Command(EditarEvento);
                GravarEventoCommand = new Command(GravarEvento);
                CancelarEdicaoEventoCommand = new Command(CancelarEdicaoEvento);
                SairCommand = new Command(Sair);
                PesquisarCommand = new Command(Pesquisar);

                // Cria o DAO.
                dao = DaoFactory.CreateDao<EventoDao>();

                IsEditing = false;
                SelectedIndex = -1;

                // Obtém a lista inicial de eventos.
                Eventos = dao.ListarEventos(null);
            }
            catch (Exception e)
            {
                // Este catch é necessário para evitar erro no processo de criação do XAML.
                Debug.WriteLine("Erro: " + e.Message);
            }
        }

        // Um novo evento está sendo adicionado.
        void NovoEvento()
        {
            // Inicia edição.
            IsEditing = true;

            // Não seleciona nenhum evento da lista.
            SelectedIndex = -1;

            // Cria um novo evento.
            Evento = new Evento();

            // Registra o interesse nas mudanças dos erros de validação.
            Evento.ErrorsChanged += OnErrorsChanged;
           

            if (WindowServices != null)
            {
                // Atualiza os bindings.
                WindowServices.UpdateBindings();

                // Coloca o foco no formulário.
                WindowServices.PutFocusOnForm();
            }
        }

        void OnErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            // Se os erros de validação mudarem, verifica se a gravação é ativada ou não.
            GravarEventoCommand.CanExecute = !Evento.HasErrors;
        }

        // Exclui um evento.
        void ExcluirEvento()
        {
            bool confirm = true;
            if (WindowServices != null)
            {
                // Pede confirmação.
                confirm = WindowServices.ConfirmDelete();
            }

            if (confirm)
            {
                // Exclui do banco de dados.
                dao.Excluir(Evento.Id.Value);

                // Atualiza a lista de eventos.
                Eventos = dao.ListarEventos(TextoPesquisa);

                // Cria um novo evento para limpar os dados do formulário.
                Evento = new Evento();
            }
        }

        // Edita um evento existente.
        void EditarEvento()
        {
            // Entra em modo de edição.
            IsEditing = true;

            if (WindowServices != null)
            {
                // Coloca o foco no formulário.
                WindowServices.PutFocusOnForm();
            }
        }

        // Grava as alterações de um evento.
        void GravarEvento()
        {
            if (Evento.Id == null)
            {
                // Se o evento não tiver um ID, é uma inserção.
                dao.Inserir(Evento);
            }
            else
            {
                // Se o evento tiver um ID é uma alteração.
                dao.Alterar(evento);
            }

            // Entra em modo de visualização.
            IsEditing = false;

            // Atualiza a lista de eventos.
            Eventos = dao.ListarEventos(TextoPesquisa);

            // Cria um novo evento para limpar os dados do formulário.
            Evento = new Evento();
        }

        // Cancela a edição de um evento.
        void CancelarEdicaoEvento()
        {
            // Entra em modo de visualização.
            IsEditing = false;

            if (Evento.Id != null)
            {
                // Se o evento tem ID, é porque ele existe no banco de dados. Então lê seus dados do banco de dados novamente.
                dao.LerEvento(Evento.Id.Value, Evento);

                // Remove o interesse na mudança dos erros de validação.
                Evento.ErrorsChanged -= OnErrorsChanged;
                
            }
            else
            {
                // Se o evento não tem ID, é porque não existe no banco de dados. Então basta descartar os dados.
                // Remove o interesse na mudança dos erros de validação.
                Evento.ErrorsChanged -= OnErrorsChanged;
                

                Evento = new Evento();
            }
        }

        // Verifica se é preciso salvar os dados antes de sair.
        public void ProcessarSaida()
        {
            if (IsEditing && GravarEventoCommand.CanExecute)
            {
                // Se está em modo de edição e a gravação está habilitada, pergunta se os dados serão salvos.
                bool confirm = true;
                if (WindowServices != null)
                {
                    confirm = WindowServices.ConfirmSave();
                }

                if (confirm)
                {
                    // Grava os dados do evento
                    GravarEvento();
                }
            }
        }

        // Inicia a saída da aplicação.
        void Sair()
        {
            // Faz o processamento final, gravando os dados se necessário.
            ProcessarSaida();

            if (WindowServices != null)
            {
                // Fecha a janela.
                WindowServices.CloseWindow();
            }
        }

        // Pesquisa eventos de acordo com um padrão de nome.
        void Pesquisar()
        {
            // Atualiza a lista de acordo com o texto fornecido.
            Eventos = dao.ListarEventos(TextoPesquisa);
        }
    }
}
