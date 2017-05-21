using MonFit_Club.Command;
using MonFit_Club.Models;
using MonFit_Club.View.Doctor;
using MonFit_Club.ViewModel.Doctor;
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
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MonFit_Club.ViewModel
{
    class DoctorViewModel : INotifyPropertyChanged
    {
        // Расписание
        private string _date_visit;
        private string _client_id_list;
        private string _time_visit;
        private string _visit_type;

        public string _Date_Visit { get { return _date_visit; } set { _date_visit = value; OnPropertyChanged("_Date_Visit"); } }
        public string _Client_Id_List { get { return _client_id_list; } set { _client_id_list = value; OnPropertyChanged("_Client_Id_List"); } }
        public string _Time_Visit { get { return _time_visit; } set { _time_visit = value; OnPropertyChanged("_Time_Visit"); } }
        public string _Visit_Type { get { return _visit_type; } set { _visit_type = value; OnPropertyChanged("_Visit_Type"); } }

        // CellInfo. For getting index of the row in Просмотр
        private int itemindex;
        public int ItemIndex { get { return itemindex; } set { itemindex = value; OnPropertyChanged("ItemIndex");} }
        //parametrs for sending to database
        private string idP;
        private string weightP;
        private string recommendP;
        private string heightP;
        private string problemsP;
        private string bodytypeP;

        // For SendDataDB(); Data that user writes for sending command
        public string IdP { get { return idP; } set { idP = value; OnPropertyChanged("IdP"); } }
        public string WeightP { get { return weightP; } set { weightP = value; OnPropertyChanged("WeightP"); } }
        public string RecommendP { get { return recommendP; } set { recommendP = value; OnPropertyChanged("RecommendP"); } }
        public string HeightP { get { return heightP; } set { heightP = value; OnPropertyChanged("HeightP"); } }
        public string ProblemsP { get { return problemsP; } set { problemsP = value; OnPropertyChanged("ProblemsP"); } }
        public string BodyTypeP { get { return bodytypeP; } set { bodytypeP = value; OnPropertyChanged("BodyTypeP"); } }

        // id
        static private int employee_id;
        // collections
        public ObservableCollection<Employee> Person { get; set; }
        public ObservableCollection<MedCard> MedCards { get; set; }
        public ObservableCollection<Schedule> Schedules { get; set; }

        static public int Employee_Id { get { return employee_id; } set { employee_id = value; } }

        // constructor
        public DoctorViewModel()
        {
            // Вкладка Расписание
            string query = "";

            query = string.Format(@"SELECT s.time_visit, s.date_visit, s.visit_type, s.client_id_list::TEXT FROM schedule s, employee e
                                    WHERE s.employee_id = {0} AND e.id = {0};", Employee_Id);
            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                Schedules = new ObservableCollection<Schedule>();
                while (reader.Read())
                {
                    Schedules.Add(
                    new Schedule()
                    {
                        Date_Visit = reader["date_visit"].ToString(),
                        Time_Visit = reader["time_visit"].ToString(),
                        Visit_Type = reader["visit_type"].ToString(),
                        Client_Id_List = reader["client_id_list"].ToString().Replace("{", "").Replace("}", "").ToString()
                    }
                      );
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }


            //
            string full_name = "", position = "", phone_number = "", experience_start = "", gender = "", password = "";
            double salary = 0;

             query = string.Format(@"SELECT * FROM employee WHERE id = {0};", employee_id);

             command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    full_name = reader["full_name"].ToString();
                    gender = reader["gender"].ToString();
                    phone_number = reader["phone_number"].ToString();
                    position = reader["position"].ToString();
                    experience_start = reader["experience_start"].ToString();
                    salary = Convert.ToDouble(reader["salary"].ToString());
                    password = reader["password"].ToString();
                }
                reader.Close();
                Person = new ObservableCollection<Employee>
                {
                     new Employee { Experience_Start = experience_start, Full_Name = full_name, Gender = gender, Id = employee_id, Password = password, Phone_Number = phone_number, Position = position, Salary = salary}
                };

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }

            // # Мед.Карта > Просмотреть
            query = string.Format(@"SELECT * FROM medcard;");
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
                    new MedCard() {
                        BodyType = reader["bodytype"].ToString(),
                        Problems = reader["problems"].ToString(),
                        Weight = _width,
                        Height = _height,
                        Client_Id = Convert.ToInt32(reader["client_id"].ToString()),
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Recommend = reader["recommend"].ToString()
                    }
                      );
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // MVVM Command 
        // 
        private RelayCommand schedule_SendDataCommand;
        public RelayCommand Schedule_SendDataCommand
        {
            get
            {
                return schedule_SendDataCommand ??
                    (schedule_SendDataCommand = new RelayCommand(obj =>
                    {
                        if (_Date_Visit != null && _Time_Visit != null && _Client_Id_List != null  && _Visit_Type != null)
                        { schedule_SendDataToDB(Employee_Id, _Date_Visit, _Time_Visit, _Visit_Type, _Client_Id_List ); }
                        else
                        {
                            MessageBox.Show("Заполнены не все поля", "Ошибка!");
                        }
                    }));
            }
        }

        private void schedule_SendDataToDB(int _empid, string _date, string _time, string _visit, string _client_list)
        {
            _client_list = "{" + _client_list + "}";
            string query = string.Format(@"INSERT INTO schedule(employee_id, date_visit, time_visit, visit_type, client_id_list) VALUES('{0}','{1}','{2}','{3}','{4}');", _empid, _date, _time, _visit, _client_list);
            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("День успешно назначен", "Сообщение");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }
        }

        // #######

        private RelayCommand showEditWindowCommand;
        public RelayCommand ShowEditWindowCommand
        {
            get
            {
                return showEditWindowCommand ??
                    (showEditWindowCommand = new RelayCommand(obj =>
                    {
                        EditWindow window = new EditWindow(MedCards[ItemIndex].Id, MedCards[ItemIndex].Client_Id, MedCards[ItemIndex].Weight, MedCards[ItemIndex].Recommend, MedCards[ItemIndex].Height, MedCards[ItemIndex].Problems, MedCards[ItemIndex].BodyType);
                        window.Show();
                    }));
            }
        }

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
                        RecommendWindow window = new RecommendWindow();
                        window.Show();
                    }));
            }
        }

        //

        private RelayCommand sendDataCommand;
        public RelayCommand SendDataCommand
        {
            get
            {
                return sendDataCommand ??
                    (sendDataCommand = new RelayCommand(obj =>
                    {
                        if (idP != null && weightP != null && heightP != null && problemsP != null && recommendP != null && bodytypeP != null)
                        { SendDataToDB(idP, weightP, heightP, problemsP, recommendP, bodytypeP);}
                        else
                        {
                            MessageBox.Show("Заполнены не все поля","Ошибка!");
                        }
                    }));
            }
        }

        private void SendDataToDB(string idP, string weightP, string heightP, string problemsP, string recommendP, string bodytypeP)
        {
            string query = string.Format(@"INSERT INTO medCard(client_id, weight, height, problems, recommend, bodytype) VALUES('{0}','{1}','{2}','{3}','{4}','{5}');", idP, weightP, heightP, problemsP, recommendP, bodytypeP);
            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try {
                command.ExecuteNonQuery();
                MessageBox.Show("Мед.Карта успешно добавлена","Сообщение");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }
        }

    }
}
