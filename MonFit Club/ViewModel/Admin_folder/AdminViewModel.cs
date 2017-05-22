using MonFit_Club.Command;
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
        // fields for sending client info

       private string cl_Id, cl_Full_Name, cl_Gender, cl_Phone_Number, cl_Card_Type, cl_Card_Period, cl_Password;
       public string Cl_Id { get { return cl_Id; } set { cl_Id = value; OnPropertyChanged("Cl_Id"); } }
       public string Cl_Full_Name { get { return cl_Full_Name; } set { cl_Full_Name = value; OnPropertyChanged("Cl_Full_Name"); } }
       public string Cl_Gender { get { return cl_Gender; } set { cl_Gender = value; OnPropertyChanged("Cl_Gender"); } }
       public string Cl_Phone_Number { get { return cl_Phone_Number; } set { cl_Phone_Number = value; OnPropertyChanged("Cl_Phone_Number"); } }
       public string Cl_Card_Type { get { return cl_Card_Type; } set { cl_Card_Type = value; OnPropertyChanged("Cl_Card_Type"); } }
       public string Cl_Card_Period { get { return cl_Card_Period; } set { cl_Card_Period = value; OnPropertyChanged("Cl_Card_Period"); } }
       public string Cl_Password { get { return cl_Password; } set { cl_Password = value; OnPropertyChanged("Cl_Password"); } }

        // fields for sending emp info
       // , , , , , , Emp_Position
       private string emp_Full_Name, emp_Gender, emp_Phone_Number, emp_Experience_Start, emp_Salary, emp_Password, emp_Position;
       public string Emp_Full_Name { get { return emp_Full_Name; } set { emp_Full_Name = value; OnPropertyChanged("Emp_Full_Name"); } }
       public string Emp_Gender { get { return emp_Gender; } set { emp_Gender = value; OnPropertyChanged("Emp_Gender"); } }
       public string Emp_Phone_Number { get { return emp_Phone_Number; } set { emp_Phone_Number = value; OnPropertyChanged("Emp_Phone_Number"); } }
       public string Emp_Experience_Start { get { return emp_Experience_Start; } set { emp_Experience_Start = value; OnPropertyChanged("Emp_Experience_Start"); } }
       public string Emp_Salary { get { return emp_Salary; } set { emp_Salary = value; OnPropertyChanged("Emp_Salary"); } }
       public string Emp_Password { get { return emp_Password; } set { emp_Password = value; OnPropertyChanged("Emp_Password"); } }
       public string Emp_Position { get { return emp_Position; } set { emp_Position = value; OnPropertyChanged("Emp_Position"); } }
        
        //employee_id
        static private int employee_id;
        static public int Employee_Id { get { return employee_id; } set { employee_id = value; } }

        // collections
        public ObservableCollection<Client> clients { get; set; }
        public ObservableCollection<Employee> employees { get; set; }
        public ObservableCollection<Employee> Person { get; set; }
        public ObservableCollection<Leave_Out> leaveouts { get; set; }

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


            // Employee ###
            employees = new ObservableCollection<Employee>();
            int empid = 0;
            string empfull_name = "", empgender = "", empphone_number = "", empposition = "", empskill = "";
            double empsalary = 0;

            query = string.Format(@"select id, full_name, position, phone_number, experience_start, gender, salary from employee;");

             command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    empid = reader.GetInt32(0);
                    empfull_name = reader["full_name"].ToString();
                    empgender = reader["gender"].ToString();
                    empphone_number = reader["phone_number"].ToString();
                    empposition = reader["position"].ToString();
                    empskill = reader["experience_start"].ToString();

                    string x = reader["salary"].ToString();
                    if (x.Contains(",")) x = x.Replace(",", "'.").ToString();
                    empsalary = Convert.ToDouble(x);



                    employees.Add(
    new Employee() { Id = empid, Full_Name = empfull_name, Gender = empgender, Phone_Number = empphone_number, Position = empposition, Experience_Start = empskill, Salary = empsalary }
    );
                }
                reader.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }


            // ##### leave out
            leaveouts = new ObservableCollection<Leave_Out>();
            query = string.Format(@"Select l.employee_id, e.full_name, l.action_datetime, l.action_type FROM leave_out l, employee e WHERE l.employee_id = e.id;");

             command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    leaveouts.Add(
    new Leave_Out() { Full_Name = reader["full_name"].ToString(), Action_DateTime = reader["action_datetime"].ToString(), Employee_Id = Convert.ToInt32(reader["employee_id"].ToString()), Action_Type = reader["action_type"].ToString() }
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

        // mvvm commands
        // SendClientDataCommand
        private RelayCommand sendClientDataCommand;
        public RelayCommand SendClientDataCommand
        {
            get
            {
                return sendClientDataCommand ??
                    (sendClientDataCommand = new RelayCommand(obj =>
                    {
                        if (Cl_Card_Period.Length > 22)
                        {
                            if (cl_Full_Name != null && cl_Gender != null && cl_Phone_Number != null && cl_Card_Type != null && cl_Card_Period != null && cl_Password != null)
                            {
                                if (cl_Card_Type == "Gold" || cl_Card_Type == "Silver" || cl_Card_Type == "Bronze")
                                {
                                    // 
                                    Cl_Card_Period = "{" + Cl_Card_Period + "}";
                                    string query = string.Format(@"INSERT into client(full_name, gender, phone_number, card_type, card_period, password) values('{0}','{1}','{2}','{3}','{4}','{5}');", Cl_Full_Name, Cl_Gender, Cl_Phone_Number, Cl_Card_Type, Cl_Card_Period, Cl_Password);
                                    NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);

                                    DataBase.connect.Open();
                                    try
                                    {
                                        command.ExecuteNonQuery();
                                        MessageBox.Show("Клиент успешно добавлен", "Сообщение");
                                    }
                                    catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                                    finally { DataBase.connect.Close(); }
                                }
                                else
                                {
                                    MessageBox.Show("Возможные варианты: Gold, Silver, Bronze");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Не все поля заполнены!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неправильно оформленный срок");
                        }


      
                    }));
            }
        }

        // ### employee insert
        // mvvm commands
        // SendClientDataCommand
        private RelayCommand sendEmployeeDataCommand;
        public RelayCommand SendEmployeeDataCommand
        {
            get
            {
                return sendEmployeeDataCommand ??
                    (sendClientDataCommand = new RelayCommand(obj =>
                    {
                        if (emp_Full_Name != null && emp_Gender != null && emp_Phone_Number != null && emp_Experience_Start != null && emp_Salary != null && emp_Password != null && emp_Position != null)
                        {
                            // 
                            string query = string.Format(@"INSERT into employee(full_name, gender, phone_number, experience_start, salary, password, position) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}');", Emp_Full_Name, Emp_Gender, Emp_Phone_Number, Emp_Experience_Start, emp_Salary, emp_Password, emp_Position);
                            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);

                            DataBase.connect.Open();
                            try
                            {
                                command.ExecuteNonQuery();
                                MessageBox.Show("Сотрудник успешно добавлен", "Сообщение");
                            }
                            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                            finally { DataBase.connect.Close(); }
                        }
                        else
                        {
                            MessageBox.Show("Не все поля заполнены");
                        }
                    }));
            }
        }


    }
}
