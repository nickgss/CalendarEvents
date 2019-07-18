using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarEvent.ApplicationService
{
    interface IWindowServices
    {
        void PutFocusOnForm();
        bool ConfirmSave();
        bool ConfirmDelete();
        void CloseWindow();
        void UpdateBindings();
        
    }
}
