using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Npgsql;
using System.Windows;

namespace MonFit_Club.Models
{
    class Employee : INotifyPropertyChanged
    {
        private int id;
        private string full_name;
        private string position;
        private string phone_number;
        private string experience_start;
        private string gender;
        private double salary;
        private string password;

        #region properties

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

        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
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

        public string Experience_Start
        {
            get { return experience_start; }
            set
            {
                experience_start = value;
                OnPropertyChanged("Experience_Start");
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

        public double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
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

        #endregion

        //
        public ObservableCollection<Employee> GetEmployee(int employee_id, ObservableCollection<Employee> collection)
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
                    //
                    collection.Add(
                        new Employee() { Experience_Start = experience_start, Full_Name = full_name, Gender = gender, Id = employee_id, Password = password, Phone_Number = phone_number, Position = position, Salary = salary }
                        );
                }
                reader.Close();
                return collection;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); return null; }
            finally { DataBase.connect.Close(); }
        }

        // refresh collection
        // insert data
        // update data

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


    }
}
