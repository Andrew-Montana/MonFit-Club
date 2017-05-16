using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.Models
{
    class CardHistory : INotifyPropertyChanged
    {
        private string data_set;
        private string card_type;
        private int client_id;
        private string condition;
        private double payment;

        public string Data_Set
        {
            get { return data_set; }
            set
            {
                data_set = value;
                OnPropertyChanged("Data_Set");
            }
        }

        public string Card_Type
        {
            get { return card_type; }
            set
            {
                card_type = value;
                OnPropertyChanged("Card_Type");
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

        public string Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                OnPropertyChanged("Condition");
            }
        }

        public double Payment
        {
            get { return payment; }
            set
            {
                payment = value;
                OnPropertyChanged("Payment");
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
