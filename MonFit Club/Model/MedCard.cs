using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.Models
{
    class MedCard : INotifyPropertyChanged
    {
        private int id;
        private int client_id;
        private double weight;
        private string recommend;
        private double height;
        private string problems;
        private string bodytype;

        public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public int Client_Id { get { return client_id; } set { client_id = value; OnPropertyChanged("Client_Id"); } }
        public double Weight { get { return weight; } set { weight = value; OnPropertyChanged("Weight"); } }
        public string Recommend { get { return recommend; } set { recommend = value; OnPropertyChanged("Recommend"); } }
        public double Height { get { return height; } set { height = value; OnPropertyChanged("Height"); } }
        public string Problems { get { return problems; } set { problems = value; OnPropertyChanged("Problems"); } }
        public string BodyType { get { return bodytype; } set { bodytype = value; OnPropertyChanged("BodyType"); } }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
