using MonFit_Club.Models;
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

namespace MonFit_Club.ViewModel.Admin_folder
{
    class AdminViewModel : INotifyPropertyChanged
    {
        //employee_id
        static private int employee_id;
        static public int Employee_Id { get { return employee_id; } set { employee_id = value; } }

        // collections
        public ObservableCollection<Client> clients { get; set; }
        public ObservableCollection<Client> employees { get; set; }
        public ObservableCollection<Employee> Person { get; set; }

        public AdminViewModel()
        {

            // Clients ###
            clients = new ObservableCollection<Client>();
            int id = 0;
            string full_name = "", gender = "", phone_number = "", card_type = "", card_period = "", password = "";

            string query = string.Format(@"SELECT id, full_name, gender, phone_number, card_type, password, card_period[1][1] as startdate, card_period[2][1] as enddate FROM client;");

            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                    full_name = reader["full_name"].ToString();
                    gender = reader["gender"].ToString();
                    phone_number = reader["phone_number"].ToString();
                    card_type = reader["card_type"].ToString();
                    card_period = reader["startdate"].ToString() + " - " + reader["enddate"].ToString();
                    password = reader["password"].ToString();

                    clients.Add(
    new Client() { Card_Period = card_period, Card_Type = card_type, Full_Name = full_name, Gender = gender, Id = id, Password = password, Phone_Number = phone_number }
    );
                }
                reader.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }

            // ### Person ( current employee )
            string per_full_name = "", per_position = "", per_phone_number = "", per_experience_start = "", per_gender = "", per_password = "";
            double per_salary = 0;

            query = string.Format(@"SELECT * FROM employee WHERE id = {0};", employee_id);

            command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    per_full_name = reader["full_name"].ToString();
                    per_gender = reader["gender"].ToString();
                    per_phone_number = reader["phone_number"].ToString();
                    per_position = reader["position"].ToString();
                    per_experience_start = reader["experience_start"].ToString();
                    per_salary = Convert.ToDouble(reader["salary"].ToString());
                    per_password = reader["password"].ToString();
                }
                reader.Close();
                Person = new ObservableCollection<Employee>
                {
                     new Employee { Experience_Start = per_experience_start, Full_Name = per_full_name, Gender = per_gender, Id = Employee_Id, Password = per_password, Phone_Number = per_phone_number, Position = per_position, Salary = per_salary}
                };

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


    }
}
