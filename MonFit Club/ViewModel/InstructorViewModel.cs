using MonFit_Club.Command;
using MonFit_Club.Models;
using MonFit_Club.View.Instructor;
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
    class InstructorViewModel : INotifyPropertyChanged
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


        // InsertData fields and properties
        private string _client_id; private string _programm;
        private string _train_type; private string _date_created;

        public string _Client_id { get { return _client_id; } set { _client_id = value; OnPropertyChanged("_Client_id"); } }
        public string _Programm { get { return _programm; } set { _programm = value; OnPropertyChanged("_Programm"); } }
        public string _Train_type { get { return _train_type; } set { _train_type = value; OnPropertyChanged("_Train_type"); } }
        public string _Date_created { get { return _date_created; } set { _date_created = value; OnPropertyChanged("_Date_created"); } }


        // CellInfo. For getting index of the row in Просмотр
        private int itemindex;
        public int ItemIndex { get { return itemindex; } set { itemindex = value; OnPropertyChanged("ItemIndex"); } }
        //employee_id
        static private int employee_id;
        static public int Employee_Id { get { return employee_id; } set { employee_id = value; } }

        // collections
        public ObservableCollection<Employee> employees { get; set; }
        public ObservableCollection<TrainRoutine> trainRoutines { get; set; }
        public ObservableCollection<TrainRoutine> AddProgrammColl { get; set; }
        public ObservableCollection<Schedule> Schedules { get; set; }



        public InstructorViewModel()
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
                        Client_Id_List = reader["client_id_list"].ToString().Replace("{","").Replace("}","").ToString()
                    }
                      );
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }
            
            // ##

            Employee employeeModel = new Employee();
            employees = new ObservableCollection<Employee>();
            employees = employeeModel.GetEmployee(employee_id, employees);

            trainRoutines = new ObservableCollection<TrainRoutine>();
            TrainRoutine trModel = new TrainRoutine();
            trainRoutines = trModel.GetTrainRoutines(employee_id, trainRoutines);

            _date_created = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // mvvm commands
        //ShowProgrammCommand
        private RelayCommand showProgrammCommand;
        public RelayCommand ShowProgrammCommand
        {
            get
            {
                return showProgrammCommand ??
                    (showProgrammCommand = new RelayCommand(obj =>
                    {
                        ProgrammViewModel.Programm = obj.ToString();
                        ProgrammWindow window = new ProgrammWindow();
                        window.Show();
                    }));
            }
        }

        //

        private RelayCommand inst_showEditWindowCommand;
        public RelayCommand inst_ShowEditWindowCommand
        {
            get
            {
                return inst_showEditWindowCommand ??
                    (inst_showEditWindowCommand = new RelayCommand(obj =>
                    {
                        Inst_EditWindow window = new Inst_EditWindow(trainRoutines[ItemIndex].Id, trainRoutines[ItemIndex].Client_Id, trainRoutines[ItemIndex].Employee_Id, trainRoutines[ItemIndex].Programm.ToString(), trainRoutines[ItemIndex].Train_Type.ToString(), trainRoutines[ItemIndex].Date_Created.ToString());
                        window.Show();
                    }));
            }
        }

        //

        private RelayCommand insertDataCommand;
        public RelayCommand InsertDataCommand
        {
            get
            {
                return insertDataCommand ??
                    (insertDataCommand = new RelayCommand(obj =>
                    {
                        if (_client_id.ToString() != null && Convert.ToString(Employee_Id) != null && _programm != null && _train_type != null && _date_created != null)
                        { SendDataToDB(_Client_id, Employee_Id, _Programm, _Train_type, _Date_created); }
                        else
                        {
                            MessageBox.Show("Заполнены не все поля", "Ошибка!");
                        }
                    }));
            }
        }

        private void SendDataToDB(string _client_id, int _employee_id, string _programm, string _train_type, string _date_created)
        {
            string query = string.Format(@"INSERT INTO trainRoutine(client_id, employee_id, programm, train_type, date_created) VALUES('{0}','{1}','{2}','{3}','{4}');", _client_id, _employee_id, _programm, _train_type, _date_created);
            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Программа тренировок успешно добавлена", "Сообщение");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }
        }

    }
}
