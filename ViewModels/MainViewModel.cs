using Employee_Management_System.Commands;
using Employee_Management_System.Models;
using Employee_Management_System.Views;
using Employee_Management_System.Services;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Employee_Management_System.Models.Wrappers;

namespace Employee_Management_System.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand AddEmployeeCommand { get; set; }

        public ICommand EditEmployeeCommand { get; set; }

        public ICommand DismissEmployeeCommand { get; set; }

        private ObservableCollection<EmployeeWrapper> _allEmployees; // Przechowuje WSZYSTKICH pracowników

        // Kolekcja tylko do wyświetlania w DataGrid
        private ObservableCollection<EmployeeWrapper> _displayedEmployees;
        public ObservableCollection<EmployeeWrapper> DisplayedEmployees
        {
            get { return _displayedEmployees; }
            set
            {
                _displayedEmployees = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<DepartmentWrapper> Departments { get; set; }

        // Właściwość przechowująca dział wybrany w ComboBox
        private DepartmentWrapper _selectedDepartment;
        public DepartmentWrapper SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();
                FilterEmployees(); // Wywołaj filtrowanie po każdej zmianie
            }
        }
        private EmployeeWrapper _selectedEmployee;
        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set 
            { 
                _selectedEmployee = value; 
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            


            AddEmployeeCommand = new RelayCommand(AddEditEmployees);
            EditEmployeeCommand = new RelayCommand(AddEditEmployees, CanEditEmployee);
            DismissEmployeeCommand = new RelayCommand(async (obj) =>
            {
                // Poprawnie wyszukujemy aktywne okno typu MetroWindow
                var metroWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.IsActive);
                if (metroWindow == null) return; // Zabezpieczenie, jeśli żadne okno nie jest aktywne

                var result = await metroWindow.ShowMessageAsync("Zwalnienie pracownika",
                    $"Czy na pewno chcesz zwolnić {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
                    MessageDialogStyle.AffirmativeAndNegative);

                // Porównujemy z właściwym typem wyniku dla MahApps
                if (result == MessageDialogResult.Affirmative)
                {
                    SelectedEmployee.DismissalDate = DateTime.Now;
                    // Odświeżamy widok, aby pokazać zmianę
                    FilterEmployees();
                }
            }, 
            // Ulepszona logika CanExecute - przycisk nieaktywny dla już zwolnionych
            (obj) => SelectedEmployee != null && SelectedEmployee.DismissalDate == null);

            // POBIERAMY DANE Z SERWISU, ZAMIAST JE TWORZYĆ
            Departments = DataService.Departments;
            _allEmployees = DataService.AllEmployees;

            // Na starcie, lista wyświetlana to wszyscy pracownicy
            DisplayedEmployees = new ObservableCollection<EmployeeWrapper>(_allEmployees);
        }
        private void AddEditEmployees(object obj)
        {
            // ZMIANA: Już nie przekazujemy listy działów!
            var addEditEmployeesWindow = new AddEditEmployeesView(obj as EmployeeWrapper);
            addEditEmployeesWindow.ShowDialog();
            FilterEmployees(); // Odśwież po zamknięciu
        }

        private bool CanEditEmployee(object obj)
        {
            return SelectedEmployee != null && SelectedEmployee.DismissalDate == null;
        }

       
        private void FilterEmployees()
        {
            // Jeśli "Wszyscy" (lub nic) jest wybrane, pokaż wszystkich
            if (SelectedDepartment == null || SelectedDepartment.Id == 0) 
            {
                DisplayedEmployees = new ObservableCollection<EmployeeWrapper>(_allEmployees);
            }
            else
            {
                // 7. Filtruj listę z zabezpieczeniem
                var filtered = _allEmployees.Where(emp => emp.Department != null && emp.Department.Id == SelectedDepartment.Id).ToList();
                DisplayedEmployees = new ObservableCollection<EmployeeWrapper>(filtered);
            }
        }
    }
}
