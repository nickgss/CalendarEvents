using CalendarEvent.ApplicationService;
using CalendarEvent.Model.Db;
using CalendarEvent.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CalendarEvent.Model;
using System.ComponentModel;
using System.Security;
using System.Windows.Input;

namespace CalendarEvent.ViewModel
{
    class AuthenticateViewModel : ViewModelBase
    {
        private string _PasswordInVM;
        private string _UserName;
        // DAO Aceeso para dados do Banco de Dados
        AuthenticateDAO dao;
        //private Authenticate auth = new Authenticate();
        // private SecureString _password;


        public string PasswordInVM
        {
            get { return _PasswordInVM; }
            set
            {
                _PasswordInVM = value;
                OnPropertyChanged("PasswordInVM");
            }
        }

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }








        public ILoginServices LoginServices { get; set; }


        // Comandos
        //public RelayCommand ValidarLoginCommand { get; set; }
        public ICommand ValidarLoginCommand { get; private set; }

        public ICommand Cancelar { get; set; }

        public AuthenticateViewModel()
        {
            try
            {
                ValidarLoginCommand = new RelayCommand(ValidarLogin);
                Cancelar = new Command(CloseWindow);
                //Cria o Dao
                dao = Db.DaoFactory.CreateDao<AuthenticateDAO>();

            }
            catch (Exception e)
            {
                Debug.WriteLine("Erro: " + e.Message);
            }
        }

        private void ValidarLogin(object parameter)
        {


            var passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                var secureString = passwordContainer.Password;
                PasswordInVM = ConvertToUnsecureString(secureString);
            }
            //int count = 2;
            int count = dao.LoginAuthenticate(_UserName, _PasswordInVM);
            if (count == 1)
            {
                if (LoginServices != null)
                {
                    LoginServices.LoginOk();
                }
            }
            else
            {
                if (LoginServices != null)
                {
                    LoginServices.LoginErrado();
                }
            }
        }


        //Encerra o programa
        public void CloseWindow()
        {
            if (LoginServices != null)
            {
                LoginServices.CloseWindow();
            }
        }
        /// <summary>
        /// Conversor de Secure String para uma simples String
        /// </summary>
        /// <param name="securePassword"></param>
        /// <returns></returns>
        private string ConvertToUnsecureString(System.Security.SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return System.Runtime.InteropServices.Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
