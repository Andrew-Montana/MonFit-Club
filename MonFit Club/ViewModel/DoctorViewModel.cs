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

namespace MonFit_Club.ViewModel
{
    class DoctorViewModel : INotifyPropertyChanged
    {
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

        static public int Employee_Id { get { return employee_id; } set { employee_id = value; } }

        // constructor
        public DoctorViewModel()
        {
            string full_name = "", position = "", phone_number = "", experience_start = "", gender = "", password = "";
            double salary = 0;

            string query = string.Format(@"SELECT * FROM employee WHERE id = {0};", employee_id);

            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
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
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                MedCards = new ObservableCollection<MedCard>();
                while (reader.Read())
                {
                    MedCards.Add(
                    new MedCard() {
                        BodyType = reader["bodytype"].ToString(),
                        Problems = reader["problems"].ToString(),
                        Weight = Convert.ToDouble(reader["weight"].ToString()),
                        Height = Convert.ToDouble(reader["height"].ToString()),
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
                        SendDataToDB(idP, weightP, heightP, problemsP, recommendP, bodytypeP);
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
