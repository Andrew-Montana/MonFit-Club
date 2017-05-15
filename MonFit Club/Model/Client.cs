using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MonFit_Club.Models
{
    class Client : INotifyPropertyChanged
    {
        private int id;
        private string full_name;
        private string gender;
        private string phone_number;
        private string card_type;
        private string card_period_begin;
        private string card_period_end;
        private string password;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Full_Name
        {
            get { return full_name; }
            set
            {
                full_name = value;
                OnPropertyChanged("Full_Name");
            }
        }

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        public string Phone_Number
        {
            get { return phone_number; }
            set
            {
                phone_number = value;
                OnPropertyChanged("Phone_Number");
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

        public string Card_Period_Begin
        {
            get { return card_period_begin; }
            set
            {
                card_period_begin = value;
                OnPropertyChanged("Card_Period_Begin");
            }
        }

        public string Card_Period_End
        {
            get { return card_period_end; }
            set
            {
                card_period_end = value;
                OnPropertyChanged("Card_Period_End");
            }
        }

        public string Password
        {
            get { return password; }
            set 
            {
                password = value;
                OnPropertyChanged("Password");
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
