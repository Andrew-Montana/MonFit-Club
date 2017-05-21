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
using MonFit_Club.ViewModel;

namespace MonFit_Club.View
{
    /// <summary>
    /// Interaction logic for InstructorWindow.xaml
    /// </summary>
    public partial class InstructorWindow
    {
        public InstructorWindow()
        {
            InitializeComponent();
            DataContext = new InstructorViewModel();
        }
    }
}
