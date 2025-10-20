using Employee_Management_System.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Employee_Management_System.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand AddEmployeeCommand { get; set; }

        private string _editTextBox = "Watrość domyślna";

        public string EditTextBox 
        {
            get { return _editTextBox; }
            set 
            { 
                _editTextBox = value; 
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            AddEmployeeCommand = new RelayCommand(AddEmployees, CanAddEmployees);
        }

        private bool CanAddEmployees(object obj)
        {
            return true; // zawsze można dodać pracownika
        }

        private void AddEmployees(object obj)
        {
            MessageBox.Show("Test");
            EditTextBox = "Nowa wartość po dodaniu pracownika";
        }
    }
}
