using MonFit_Club.Command;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonFit_Club.View.Admin
{
    class Admin_EmployeeEditVM : INotifyPropertyChanged
    {

        public Admin_EmployeeEditVM(int _id, string _full_name, string _position, string _phone_number, string _exp_start, string _gender, string _salary, string _password)
        {
            this.id = _id;
            this.full_name = _full_name;
            this.position = _position;
            this.phone_number = _phone_number;
            this.experience_start = _exp_start;
            this.gender = _gender;
            this.salary = _salary;
            this.password = _password;
        }

        private int id;
        private string full_name;
        private string position;
        private string phone_number;
        private string experience_start;
        private string gender;
        private string salary;
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

        public string Salary
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // MVVM commands

        // employee edit
        private RelayCommand _Admin_EmployeeEditCommand;
        public RelayCommand Admin_EmployeeEditCommand
        {
            get
            {
                return _Admin_EmployeeEditCommand ??
                    (_Admin_EmployeeEditCommand = new RelayCommand(obj =>
                    {

                        string query = string.Format(@"UPDATE employee SET full_name ='{0}', position='{1}', phone_number='{2}', experience_start ='{3}', gender ='{4}', salary ={5}, password='{6}' WHERE id={7};", full_name, position, phone_number, experience_start, gender, salary, password, id);

                        NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
                        DataBase.connect.Open();
                        try
                        {
                            command.ExecuteNonQuery();
                            MessageBox.Show("Запись успешно обновлена!", "Сообщение!");

                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                        finally { DataBase.connect.Close(); }

                    }));
            }
        }

    }
}

