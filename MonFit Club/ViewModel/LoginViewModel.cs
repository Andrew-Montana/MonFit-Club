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
using Npgsql;
using MonFit_Club.View;

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

        // DataBase command. Auth

        static public void Auth(string table, string id, string password)
        {
            int count = 0;
            string position = "";
            bool isAuth = false;

            //NpgsqlConnection connect = new NpgsqlConnection() { ConnectionString = DataBase.connect_params };

            string query = string.Format(@"SELECT * FROM {0} WHERE id='{1}' AND password='{2}';", table, id, password);

            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count++;
                    if (table != "client" && table == "employee")
                        position = reader["position"].ToString();
                }
                reader.Close();
                if (count == 1)
                {
                    MessageBox.Show("Вы успешно авторизировались");
                    isAuth = true;
                }
                else
                {
                    MessageBox.Show("Неправильный ID или пароль");
                    isAuth = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DataBase.connect.Close();
            }

            if (table == "client" && isAuth == true)
            {
                App.Current.MainWindow.Hide();
                int x;
                x = Convert.ToInt32(id);
                ClientViewModel.Client_Id = x;
                ClientWindow clientWindow = new ClientWindow();
                clientWindow.Show();
            }
            else
            {
                if (table == "employee" && isAuth == true)
                {
                    if (position.Contains("Инструктор"))
                    {
                        MessageBox.Show("Окно инструктора не готово");
                        // а вообще, передать параметры в свойства класса.
                    }
                    if (position == "Врач")
                    {
                        MessageBox.Show("Окно доктора не готово");
                    }
                    if (position == "Администратор")
                    {
                        MessageBox.Show("Окно администратора не готово");
                    }
                    App.Current.MainWindow.Hide();
                    
                }
            }

        }

        // MVVM Commands. Action binded with button

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
                            string table = "";
                            if(selectedValue == "Клиент")
                                table = "client";
                            else
                                table = "employee";
                            if (login != "" && password != null)
                            {
                                int x;
                                bool isNumeric = int.TryParse(login, out x);
                                if (isNumeric)
                                {
                                    Auth(table, login, password);
                                }
                                else
                                {
                                    MessageBox.Show("Логин должен состоять исключительно из цифр", "Ошибка");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите логин или пароль", "Ошибка");
                            }
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
