using MonFit_Club.ViewModel.Doctor;
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

namespace MonFit_Club.View.Doctor
{
    /// <summary>
    /// Interaction logic for RecommendWindow.xaml
    /// </summary>
    public partial class RecommendWindow
    {
        public RecommendWindow()
        {
            InitializeComponent();
            DataContext = new RecommendViewModel();
        }
    }
}
