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

namespace MonFit_Club.View.Admin
{
    /// <summary>
    /// Interaction logic for Admin_ClientEditWin.xaml
    /// </summary>
    public partial class Admin_EmployeeEditWin
    {

        public Admin_EmployeeEditWin(int _id, string _full_name, string _position, string _phone_number, string _exp_start, string _gender, string _salary, string _password)
        {
            InitializeComponent();
            DataContext = new Admin_EmployeeEditVM(_id, _full_name, _position, _phone_number, _exp_start, _gender, _salary, _password);
        }
    }
}
