using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.Models
{
    class Leave_Out : INotifyPropertyChanged
    {
        private int employee_id;
        private int client_id;
        private string action_datetime;
        private string action_type;
        private string precedent;
        private string full_name;

        public string Full_Name
        {
            get { return full_name; }
            set
            {
                full_name = value;
                OnPropertyChanged("Full_Name");
            }
        }

        public int Employee_Id
        {
            get { return employee_id; }
            set
            {
                employee_id = value;
                OnPropertyChanged("Employee_Id");
            }
        }

        public int Client_Id
        {
            get { return client_id; }
            set
            {
                client_id = value;
                OnPropertyChanged("Client_Id");
            }
        }

        public string Action_DateTime
        {
            get { return action_datetime; }
            set
            {
                action_datetime = value;
                OnPropertyChanged("Action_DateTime");
            }
        }

        public string Action_Type
        {
            get { return action_type; }
            set
            {
                action_type = value;
                OnPropertyChanged("Action_Type");
            }
        }

        public string Precedent
        {
            get { return precedent; }
            set
            {
                precedent = value;
                OnPropertyChanged("Precedent");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        
    }
}
