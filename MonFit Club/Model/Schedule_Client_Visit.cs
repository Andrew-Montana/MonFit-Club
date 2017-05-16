using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.Models
{
    class Schedule_Client_Visit : INotifyPropertyChanged
    {
        private int schedule_id;
        private int client_id;

        public int Schedule_Id { get { return schedule_id; } set { schedule_id = value; OnPropertyChanged("Schedule_Id"); } }
        public int Client_Id { get { return client_id; } set { client_id = value; OnPropertyChanged("Client_Id"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
