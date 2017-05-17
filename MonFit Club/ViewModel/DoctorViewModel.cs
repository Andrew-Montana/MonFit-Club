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

namespace MonFit_Club.ViewModel
{
    class DoctorViewModel : INotifyPropertyChanged
    {
        static private int employee_id;

        public ObservableCollection<Employee> Person { get; set; }

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
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
