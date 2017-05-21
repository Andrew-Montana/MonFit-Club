using MonFit_Club.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.ViewModel.Instructor
{
    class Inst_EditViewModel : INotifyPropertyChanged
    {
        private TrainRoutine trainRoutine_field;

        public TrainRoutine TrainRoutine_property { get { return trainRoutine_field; } set { trainRoutine_field = value; OnPropertyChanged("TrainRoutine_property"); } }

        public Inst_EditViewModel(int id, int client_id, int employee_id, string programm, string train_type, string date_created)
        {
            // инициализация для автозаполнения контролов и последующего неоходимого изменения
            trainRoutine_field = new TrainRoutine() { Id = id, Client_Id = client_id, Employee_Id = employee_id, Programm = programm, Train_Type = train_type, Date_Created = date_created  };
        }

        // mvvm command

       

        //

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
