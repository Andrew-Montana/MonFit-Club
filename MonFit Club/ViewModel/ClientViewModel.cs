using MonFit_Club.Command;
using MonFit_Club.Models;
using MonFit_Club.View;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonFit_Club.ViewModel
{
    class ClientViewModel : INotifyPropertyChanged
    {
        static private int client_id;
        public ObservableCollection<Client> Person { get; set; }

        static public int Client_Id { get { return client_id; } set { client_id = value; } }

        // Конструктор
        public ClientViewModel()
        {
            string full_name = "", gender = "", phone_number = "", card_type = "", card_period_begin = "", card_begin_end = "", password = "";

            NpgsqlConnection connect = new NpgsqlConnection() { ConnectionString = DataBase.connect_params };

            string query = string.Format(@"SELECT phone_number, password, gender, full_name, card_type, card_period[1][1] as begin, card_period[2][1] as end FROM client WHERE id = {0};",client_id);

            NpgsqlCommand command = new NpgsqlCommand(query, connect);
            connect.Open();
            try {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    full_name = reader["full_name"].ToString();
                    gender = reader["gender"].ToString();
                    phone_number = reader["phone_number"].ToString();
                    card_type = reader["card_type"].ToString();
                    card_period_begin = reader["begin"].ToString();
                    card_begin_end = reader["end"].ToString();
                    password = reader["password"].ToString();
                }
                Person = new ObservableCollection<Client>
                {
                     new Client { Id = client_id, Phone_Number = phone_number, Password = password, Gender = gender, Full_Name = full_name, Card_Type = card_type, Card_Period = card_period_begin + " - " + card_begin_end}
                };
  
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { connect.Close(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // mvvm commands
        private RelayCommand cardHistoryOpenCommand;
        public RelayCommand CardHistoryOpenCommand
        {
            get
            {
                return cardHistoryOpenCommand ??
                    (cardHistoryOpenCommand = new RelayCommand(obj =>
                    {
                        Client_CardHistoryWindow window = new Client_CardHistoryWindow();
                        Client_CardHistoryViewModel.Client_Id = client_id;
                        window.Show();
                    }));
            }
        }

    }
}
