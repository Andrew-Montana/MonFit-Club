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
using MahApps.Metro.Controls;

namespace MonFit_Club.View.Instructor
{
    /// <summary>
    /// Interaction logic for ProgrammWindow.xaml
    /// </summary>
    public partial class ProgrammWindow
    {
        public ProgrammWindow()
        {
            InitializeComponent();
            DataContext = new ProgrammViewModel();
        }
    }
}
