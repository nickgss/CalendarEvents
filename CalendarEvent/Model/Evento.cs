using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarEvent.Utils;

namespace CalendarEvent.Model
{
    class Evento : Bindable
    {
        private int? id;
        public int? Id
        {
            get { return id; }
            set { SetValue(ref id, value); }
        }

        private string nome;
        public string Nome
        {
            get { return nome; }
            set
            {
                SetValue(ref nome, value);

                if (string.IsNullOrEmpty(nome))
                {
                    AddError("O nome não pode ser vazio");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }

        private string datainicio;
        public string DataInicio
        {
            get { return datainicio; }
            set
            {
                SetValue(ref datainicio, value);

                if (string.IsNullOrEmpty(datainicio))
                {
                    AddError("Data deverá ser preenchida.");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }

        private string datafinal;
        public string DataFinal
        {
            get { return datafinal; }
            set
            {
                SetValue(ref datafinal, value);

                if (string.IsNullOrEmpty(datafinal))
                {
                    AddError("Data deverá ser preenchida.");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }


        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                SetValue(ref descricao, value);

                if (string.IsNullOrEmpty(descricao))
                {
                    AddError("Descricao nao pode estar vazia.");
                }
                else
                {
                    RemoveErrors();
                }
            }
        }


    }
}
