using CalendarEvent.ApplicationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CalendarEvent.View
{
    /// <summary>
    /// Lógica interna para MainProgram.xaml
    /// </summary>
    public partial class MainProgram : Window, IWindowServices
    {
        // Indica se a janela deve ser fechada.
        bool closeWindow;

        public MainProgram()
        {
            InitializeComponent();

            // Faz com que o ViewModel referencie a janela como um IWindowServices
            viewModel.WindowServices = this;
        }

        public void PutFocusOnForm()
        {
            // Coloca o foco no campo de nome do formulário.
            txtEventName.Focus();
        }

        public bool ConfirmSave()
        {
            // Exibe um dialog perguntando se é preciso salvar.
            MessageBoxResult result = MessageBox.Show(this, "Deseja salvar as alterações no evento?", "Salvar alterações?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public bool ConfirmDelete()
        {
            // Exibe um dialog perguntando se realmente é para excluir o cliente.
            MessageBoxResult result = MessageBox.Show(this, "Deseja excluir o Evento?", "Excluir?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        public void CloseWindow()
        {
            // Fecha a janela.
            closeWindow = true;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeWindow)
            {
                // Se a janela está fechando, primeiro dá a chance do ViewModel de fazer as últimas checagens.
                viewModel.ProcessarSaida();
            }
        }

        public void UpdateBindings()
        {
            // Atualiza os bindings do formulário manualmente.
            // Isto é necessário para que os erros de validação já apareçam assim que o formulário é ativado.

            txtEventName.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtStartTime.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtEndTime.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            txtDescricao.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }



        private void HandleSpecialChars(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Trata a digitação de caracteres especiais em alguns campos do formulário.

            // Converte o objeto que gerou o evento para um TextBox.
            TextBox textBox = sender as TextBox;

            if (e.Text.Length > 0)
            {
                bool allowed = false;

                if (textBox == txtStartTime)
                {
                    // Permite apenas números no campo "número".
                    if (char.IsDigit(e.Text, e.Text.Length - 1))
                    {
                        allowed = true;
                    }
                }
                else if (textBox == txtEndTime)
                {
                    if (char.IsDigit(e.Text, e.Text.Length - 1))
                    {
                        allowed = true;
                    }
                }
                
                // Indica se o evento foi tratado ou não. Se o evento for tratado, o caractere não aparece no campo do formulário.
                e.Handled = !allowed;
            }
        }

        private void HandleSpaceChar(object sender, KeyEventArgs e)
        {
            // Não permite espaços em branco nos campos "telefone", "CEP" e "número";
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // Start calendar Functions

    }
}
