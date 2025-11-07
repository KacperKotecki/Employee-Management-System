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
using Employee_Management_System.Models.Domains;
using Position = Employee_Management_System.Models.Domains.Position;

namespace Employee_Management_System.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand AddEmployeeCommand { get; set; }

        public ICommand EditEmployeeCommand { get; set; }

        public ICommand DismissEmployeeCommand { get; set; }

        private DataService dataService = new DataService();

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

        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Position> Positions { get; set; }

        
        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();
                RefreshEmployees(); // Wywołaj odświeżanie po każdej zmianie
            }
        }

        private Position _selectedPosition;
        public Position SelectedPosition
        {
            get { return _selectedPosition; }
            set
            {
                _selectedPosition = value;
                OnPropertyChanged();
                RefreshEmployees();
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
                var metroWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.IsActive);
                if (metroWindow == null) return; // Zabezpieczenie, jeśli żadne okno nie jest aktywne

                var result = await metroWindow.ShowMessageAsync("Zwalnienie pracownika",
                    $"Czy na pewno chcesz zwolnić {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?",
                    MessageDialogStyle.AffirmativeAndNegative);

                
                if (result == MessageDialogResult.Affirmative)
                {
                    dataService.DismissalEmployee(SelectedEmployee.Id);
                    RefreshEmployees(); 
                }

            }, 
            
            (obj) => SelectedEmployee != null && SelectedEmployee.DismissalDate == null);

          
            var departments = dataService.GetDepartments();
            departments.Insert(0, new Department { Id = 0, Name = "Wszyscy" });
            Departments = new ObservableCollection<Department>(departments);

            var positions = dataService.GetPositions();
            positions.Insert(0, new Position { Id = 0, Name = "Wszystkie" });
            Positions = new ObservableCollection<Position>(positions);

            RefreshEmployees();
        }

        private void RefreshEmployees()
        {
            int departmentId = SelectedDepartment?.Id ?? 0;
            int positionId = SelectedPosition?.Id ?? 0;
            var employees = dataService.GetEmployees(departmentId, positionId);
            DisplayedEmployees = new ObservableCollection<EmployeeWrapper>(employees);
        }

        private void AddEditEmployees(object obj)
        {
            
            // ZMIANA: Już nie przekazujemy listy działów!
            var addEditEmployeesWindow = new AddEditEmployeesView(obj as EmployeeWrapper);
            addEditEmployeesWindow.ShowDialog();


            RefreshEmployees(); // Odśwież po zamknięciu
        }

        private bool CanEditEmployee(object obj)
        {
            
            return SelectedEmployee != null && SelectedEmployee.DismissalDate == null;
        }

       
       
    }
}
