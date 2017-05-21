using MonFit_Club.Models;
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
        //employee_id
        static private int employee_id;
        static public int Employee_Id { get { return employee_id; } set { employee_id = value; } }

        // collections
        public ObservableCollection<Employee> employees { get; set; }
        public ObservableCollection<TrainRoutine> trainRoutines { get; set; }

        public InstructorViewModel()
        {
            Employee employeeModel = new Employee();
            employees = new ObservableCollection<Employee>();
            employees = employeeModel.GetEmployee(employee_id, employees);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
