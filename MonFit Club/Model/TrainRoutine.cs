using MonFit_Club.Command;
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

namespace MonFit_Club.Models
{
    class TrainRoutine : INotifyPropertyChanged
    {
        private int id;
        private int employee_id;
        private int client_id;
        private string programm;
        private string train_type;
        private string date_created;

        //
        public ObservableCollection<TrainRoutine> GetTrainRoutines(int employee_id, ObservableCollection<TrainRoutine> collection)
        {
            int p_id, p_employee_id, p_client_id; string _p_programm, p_train_type, p_data_created;

            string query = string.Format(@"SELECT tr.* FROM trainRoutine tr, client c WHERE tr.employee_id = {0} AND tr.client_id = c.id;", employee_id);

            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    p_id = Convert.ToInt32(reader["id"].ToString());
                    p_employee_id = Convert.ToInt32(reader["employee_id"].ToString());
                    p_client_id = Convert.ToInt32(reader["client_id"].ToString());
                    _p_programm = reader["programm"].ToString();
                    p_train_type = reader["train_type"].ToString();
                    p_data_created = reader["date_created"].ToString();
                    //
                    collection.Add(
                        new TrainRoutine() { Id = p_id, Employee_Id = p_employee_id, Client_Id = p_client_id, Programm = _p_programm, Train_Type = p_train_type, Date_Created = p_data_created }
                        );
                }
                reader.Close();
                return collection;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return null; }
            finally { DataBase.connect.Close(); }
        }

        //

        public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public int Employee_Id { get { return employee_id; } set { employee_id = value; OnPropertyChanged("Employee_Id"); } }
        public int Client_Id { get { return client_id; } set { client_id = value; OnPropertyChanged("Client_Id"); } }
        public string Programm { get { return programm; } set { programm = value; OnPropertyChanged("Programm"); } }
        public string Train_Type { get { return train_type; } set { train_type = value; OnPropertyChanged("Train_Type"); } }
        public string Date_Created { get { return date_created; } set { date_created = value; OnPropertyChanged("Date_Created"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // Insert DB
        private RelayCommand insertDataCommand;
        public RelayCommand InsertDataCommand
        {
            get
            {
                return insertDataCommand ??
                    (insertDataCommand = new RelayCommand(obj =>
                    {
                       //
                    }));
            }
        }

        // Update DB
        private RelayCommand updateDataCommand;
        public RelayCommand UpdateDataCommand
        {
            get
            {
                return updateDataCommand ??
                    (updateDataCommand = new RelayCommand(obj =>
                    {
                        if (Convert.ToString(Client_Id) != null && Convert.ToString(Weight) != null && Convert.ToString(Height) != null && Problems != null && Recommend != null && BodyType != null)
                        { UpdateDataToDB(Client_Id, Weight, Height, Problems, Recommend, BodyType, Id); }
                        else
                        {
                            MessageBox.Show("Заполнены не все поля", "Ошибка!");
                        }
                    }));
            }
        }

        private void UpdateDataToDB(int p1, string p2, string p3, string p4, string p5, string p6, int p7)
        {
            var _width = p2.ToString();
            var _height = p3.ToString();
            if (_width.Contains(','))
            {
                _width.Replace(',', '.');
                _width = _width.Replace(',', '.');
            }
            if (_height.Contains(','))
            {
                _height.Replace(',', '.');
                _height = _width.Replace(',', '.');
            }

            string query = string.Format(@"UPDATE medCard SET weight = {0}, height = {1}, problems = '{2}', recommend = '{3}', bodytype = '{4}' WHERE client_id = {5} AND id = {6};", _width, _height, p4, p5, p6, p1, p7);
            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Мед.Карта успешно обновлена", "Сообщение");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }
        }


    }
}
