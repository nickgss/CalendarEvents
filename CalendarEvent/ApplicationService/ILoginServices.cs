using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarEvent.ApplicationService
{
    interface ILoginServices
    {

        void LoginOk();
        void LoginErrado();
        void CloseWindow();
        void NovaConta();


    }
}
