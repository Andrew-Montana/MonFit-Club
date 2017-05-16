using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.Models
{
    class TrainRoutine : INotifyPropertyChanged
    {
        private int id;
        private int employee_id;
        private int client_id;
        private string programm;
        private string train_type;
        private string data_created;

        public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public int Employee_Id { get { return employee_id; } set { employee_id = value; OnPropertyChanged("Employee_Id"); } }
        public int Client_Id { get { return client_id; } set { client_id = value; OnPropertyChanged("Client_Id"); } }
        public string Programm { get { return programm; } set { programm = value; OnPropertyChanged("Programm"); } }
        public string Train_Type { get { return train_type; } set { train_type = value; OnPropertyChanged("Train_Type"); } }
        public string Data_Created { get { return data_created; } set { data_created = value; OnPropertyChanged("Data_Created"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
