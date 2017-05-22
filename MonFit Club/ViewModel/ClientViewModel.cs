using MonFit_Club.Command;
using MonFit_Club.Models;
using MonFit_Club.View;
using MonFit_Club.View.Client;
using MonFit_Club.ViewModel.Client_folder;
using MonFit_Club.ViewModel.Doctor;
using MonFit_Club.ViewModel.Instructor;
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
        public ObservableCollection<MonFit_Club.Models.Client> Person { get; set; }
        public ObservableCollection<Schedule> Schedules { get; set; }
        public ObservableCollection<MedCard> MedCards { get; set; }
        public ObservableCollection<ClientProgrammViewModel> ClientProgramm { get; set; }

        static public int Client_Id { get { return client_id; } set { client_id = value; } }

        // Конструктор
        public ClientViewModel()
        {

            // medcard
            string full_name = "", gender = "", phone_number = "", card_type = "", card_period_begin = "", card_begin_end = "", password = "";

           // NpgsqlConnection connect = new NpgsqlConnection() { ConnectionString = DataBase.connect_params };

            string query = string.Format(@"SELECT phone_number, password, gender, full_name, card_type, card_period[1][1] as begin, card_period[2][1] as end FROM client WHERE id = {0};",client_id);

            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
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
                reader.Close();
                Person = new ObservableCollection<MonFit_Club.Models.Client>
                {
                     new MonFit_Club.Models.Client { Id = client_id, Phone_Number = phone_number, Password = password, Gender = gender, Full_Name = full_name, Card_Type = card_type, Card_Period = card_period_begin + " - " + card_begin_end}
                };
  
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }

            // Вкладка Расписание
            query = string.Format(@"SELECT s.time_visit, s.date_visit, e.full_name, s.visit_type FROM schedule s, employee e
                                    WHERE client_id_list @> ARRAY[{0}] AND e.id = s.employee_id;", client_id);
            command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                Schedules = new ObservableCollection<Schedule>();
                while (reader.Read())
                {
                    Schedules.Add(
                    new Schedule() {
                        Date_Visit = reader["date_visit"].ToString(),
                        Time_Visit = reader["time_visit"].ToString(),
                        Visit_Type = reader["visit_type"].ToString(),
                        Employee_Full_Name = reader["full_name"].ToString()
                    }
                      );
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }

            // ### Мед.Карта Вкладка
            query = string.Format(@"SELECT weight, recommend, height, problems, bodytype FROM medcard WHERE client_id = {0};", client_id);
            command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                string _width = "";
                string _height = "";
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                MedCards = new ObservableCollection<MedCard>();
                while (reader.Read())
                {
                    _width = reader["weight"].ToString();
                    _height = reader["height"].ToString();
                    if (_width.Contains(',')) _width = _width.Replace(',', '.');
                    if (_height.Contains(',')) _height = _height.Replace(',', '.');
                    //
                    MedCards.Add(
                    new MedCard()
                    {
                        Recommend = reader["recommend"].ToString(),
                        Weight = _width,
                        Height = _height,
                        Problems = reader["problems"].ToString(),
                        BodyType = reader["bodytype"].ToString()
                    }
                      );
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }


            // # Programm
            query = string.Format(@"SELECT tr.date_created, tr.programm, tr.train_type, e.full_name as inst_full_name FROM trainRoutine tr, employee e WHERE tr.client_id = {0} AND tr.employee_id = e.id;", Client_Id);
            command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                ClientProgramm = new ObservableCollection<ClientProgrammViewModel>();
                while (reader.Read())
                {
                    ClientProgramm.Add(
                    new ClientProgrammViewModel()
                    {
                        Date_Created = reader["date_created"].ToString(),
                        Programm = reader["programm"].ToString(),
                        Train_Type = reader["train_type"].ToString(),
                        Instuctor_FullName = reader["inst_full_name"].ToString()
                    }
                      );
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }

            // ####
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
                        Client_CardHistoryViewModel.Client_Id = client_id;
                        Client_CardHistoryWindow window = new Client_CardHistoryWindow();
                        window.Show();
                    }));
            }
        }

        // Recommend window

        //

        private RelayCommand showRecommendCommand;
        public RelayCommand ShowRecommendCommand
        {
            get
            {
                return showRecommendCommand ??
                    (showRecommendCommand = new RelayCommand(obj =>
                    {
                        RecommendViewModel.Recommend = obj.ToString();
                        ClientRecommend window = new ClientRecommend();
                        window.Show();
                    }));
            }
        }

        // programm command
        private RelayCommand client_ShowProgrammCommand;
        public RelayCommand Client_ShowProgrammCommand
        {
            get
            {
                return client_ShowProgrammCommand ??
                    (client_ShowProgrammCommand = new RelayCommand(obj =>
                    {
                        ProgrammViewModel.Programm = obj.ToString();
                        ClientProgrammWindow client_pwindow = new ClientProgrammWindow();
                        client_pwindow.Show();
                    }));
            }
        }


        //



    }
}
