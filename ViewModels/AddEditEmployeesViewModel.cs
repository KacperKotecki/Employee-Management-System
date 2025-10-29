using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Employee_Management_System.Commands;
using Employee_Management_System.Models;
using Employee_Management_System.Models.Wrappers;
using Employee_Management_System.Services;

namespace Employee_Management_System.ViewModels
{
    public class AddEditEmployeesViewModel : BaseViewModel
    {
        // Właściwość przechowująca pracownika do edycji lub dodania
        private EmployeeWrapper _employee;
        public EmployeeWrapper Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }

        // Komendy dla przycisków
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        
        public ObservableCollection<DepartmentWrapper> Departments { get; set; }

        public AddEditEmployeesViewModel(EmployeeWrapper employee)
        {
            
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);

            
            Departments = DataService.Departments;

           
            Employee = employee ?? new EmployeeWrapper();
        }

        private void Save(object obj)
        {
            // Tutaj będzie logika zapisu. Na razie tylko zamykamy okno.
            // W przyszłości dodamy tu walidację i zapis do bazy.
            CloseWindow(obj as System.Windows.Window, true);
        }

        private void Cancel(object obj)
        {
            // Zamykamy okno bez zapisywania
            CloseWindow(obj as System.Windows.Window, false);
        }

        private void CloseWindow(System.Windows.Window window, bool dialogResult)
        {
            if (window != null)
            {
                window.DialogResult = dialogResult;
                window.Close();
            }
        }
    }
}
