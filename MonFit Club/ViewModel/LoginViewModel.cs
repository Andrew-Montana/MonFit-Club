using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MonFit_Club.Command;

namespace MonFit_Club.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        private string selectedValue;
        private string login;
        private string password;

        public string SelectedValue
        {
            get { return selectedValue; }
            set
            {
                selectedValue = value;
                OnPropertyChanged("SelectedValue");
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
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
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // MVVM Commands

        #region LoginCommand

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(obj =>
                    {
                        if (selectedValue != null)
                        {
                            
                        }
                        else
                        {
                            MessageBox.Show("Выберите кем Вы являетесь (Клиент/Сотрудник)","Ошибка");
                        }
                    }));
            }
        }

        #endregion

    }
}
