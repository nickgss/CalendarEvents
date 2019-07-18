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
    /// Lógica interna para LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window,ILoginServices, IHavePassword
    {
        public LoginScreen()
        {
            InitializeComponent();
            viewModel.LoginServices = this;
        }

        public System.Security.SecureString Password
        {
            get
            {
                return UserPassword.SecurePassword;
            }
        }

        public void CloseWindow()
        {
            // Fecha a janela.

            this.Close();
        }

        public void LoginErrado()
        {
            MessageBox.Show(this, "User or Password incorrect!");
            txtUsername.Text = "";
            UserPassword.Clear();
            txtUsername.Focus();
        }

        public void LoginOk()
        {
            //System.Windows.Forms.MessageBox.Show("Test");
            MessageBox.Show(this, "Authentication is Ok!");
            MainProgram mp = new MainProgram();
            mp.Show();
            this.Close();
        }

        public void NovaConta()
        {
            Cliente cliente = new Cliente();
            cliente.Show();
            this.Close();
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = new Cliente();
            cliente.Show();
            this.Close();
        }
    }
}
