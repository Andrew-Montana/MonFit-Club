using MonFit_Club.ViewModel.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MonFit_Club.View.Instructor
{
    /// <summary>
    /// Interaction logic for Inst_EditWindow.xaml
    /// </summary>
    public partial class Inst_EditWindow : Window
    {

        public Inst_EditWindow(int id, int client_id, int employee_id, string programm, string train_type, string date_created)
        {
            InitializeComponent();
            DataContext = new Inst_EditViewModel(id, client_id, employee_id, programm, train_type, date_created);
        }
    }
}
