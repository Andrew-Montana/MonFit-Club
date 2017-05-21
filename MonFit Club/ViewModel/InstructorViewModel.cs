using MonFit_Club.Command;
using MonFit_Club.Models;
using MonFit_Club.View.Instructor;
using MonFit_Club.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonFit_Club.ViewModel
{
    class InstructorViewModel : INotifyPropertyChanged
    {
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



        public InstructorViewModel()
        {
            Employee employeeModel = new Employee();
            employees = new ObservableCollection<Employee>();
            employees = employeeModel.GetEmployee(employee_id, employees);

            trainRoutines = new ObservableCollection<TrainRoutine>();
            TrainRoutine trModel = new TrainRoutine();
            trainRoutines = trModel.GetTrainRoutines(employee_id, trainRoutines);
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

    }
}
