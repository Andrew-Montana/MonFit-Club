using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.ViewModel.Client
{
    class ClientProgrammViewModel : INotifyPropertyChanged
    {
        private int client_id;
        private string programm;
        private string train_type;
        private string date_created;

        public int Client_Id { get { return client_id; } set { client_id = value; OnPropertyChanged("Client_Id"); } }
        public string Programm { get { return programm; } set { programm = value; OnPropertyChanged("Programm"); } }
        public string Train_Type { get { return train_type; } set { train_type = value; OnPropertyChanged("Train_Type"); } }
        public string Date_Created { get { return date_created; } set { date_created = value; OnPropertyChanged("Date_Created"); } }
        private string instuctor_FullName;
        public string Instuctor_FullName { get { return instuctor_FullName; } set { instuctor_FullName = value; OnPropertyChanged("Instuctor_FullName"); } }
        //

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
