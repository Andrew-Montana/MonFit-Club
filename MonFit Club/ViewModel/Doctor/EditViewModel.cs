using MonFit_Club.Command;
using MonFit_Club.Models;
using MonFit_Club.View.Doctor;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonFit_Club.ViewModel.Doctor
{
    class EditViewModel : INotifyPropertyChanged
    {
        private MedCard medCard_field;

        public MedCard MedCard_property { get { return medCard_field; } set { medCard_field = value; OnPropertyChanged("MedCard_property"); } }

        public EditViewModel(int id, int client_id, string weight, string recommend, string height, string problems, string bodytype)
        {
            // инициализация для автозаполнения контролов и последующего неоходимого изменения
            medCard_field = new MedCard() { BodyType = bodytype, Client_Id = client_id, Height = height, Id = id, Problems = problems, Recommend = recommend, Weight = weight };
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
