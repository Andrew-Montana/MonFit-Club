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
using MahApps.Metro.Controls;
using MonFit_Club.ViewModel.Doctor;

namespace MonFit_Club.View.Doctor
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow
    {
        public EditWindow(int id, int client_id, double weight, string recommend, double height, string problems, string bodytype)
        {
            InitializeComponent();
            DataContext = new EditViewModel(id, client_id, weight, recommend, height, problems, bodytype);
        }
    }
}
