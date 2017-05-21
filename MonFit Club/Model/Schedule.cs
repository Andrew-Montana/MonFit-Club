using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.Models
{
    class Schedule : INotifyPropertyChanged
    {
        private int id;
        private string time_visit;
        private string date_visit;
        private int employee_id;
        private string client_id_list;
        private string visit_type;
        private string employee_full_name; // for column

        public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public string Time_Visit { get { return time_visit; } set { time_visit = value; OnPropertyChanged("Time_Visit"); } }
        public string Date_Visit { get { return date_visit; } set { date_visit = value; OnPropertyChanged("Date_Visit"); } }
        public int Employee_Id { get { return employee_id; } set { employee_id = value; OnPropertyChanged("Employee_Id"); } }
        public string Client_Id_List { get { return client_id_list; } set { client_id_list = value; OnPropertyChanged("Client_Id_List"); } }
        public string Visit_Type { get { return visit_type; } set { visit_type = value; OnPropertyChanged("Visit_Type"); } }
        public string Employee_Full_Name { get { return employee_full_name; } set { employee_full_name = value; OnPropertyChanged("Employee_Full_Name"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
