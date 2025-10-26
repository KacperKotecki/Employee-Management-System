using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Employee_Management_System.Models;
using Employee_Management_System.ViewModels;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;

namespace Employee_Management_System.Views
{
    /// <summary>
    /// Interaction logic for AddEditEmployeesView.xaml
    /// </summary>
    public partial class AddEditEmployeesView : MetroWindow
    {
        public AddEditEmployeesView(Employee employee)
        {
            InitializeComponent();
            
            // Tworzymy ViewModel, przekazując mu pracownika i listę działów
            var viewModel = new AddEditEmployeesViewModel(employee);
            
            // Ustawiamy DataContext okna na nasz nowy ViewModel
            DataContext = viewModel;
        }
    }
}
