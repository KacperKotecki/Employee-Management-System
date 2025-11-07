using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Employee_Management_System.Commands;
using Employee_Management_System.Models;
using Employee_Management_System.Models.Domains;
using Employee_Management_System.Models.Wrappers;
using Employee_Management_System.Services;

namespace Employee_Management_System.ViewModels
{
    public class AddEditEmployeesViewModel : BaseViewModel, IDataErrorInfo
    {
        private DataService dataService = new DataService();

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

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ObservableCollection<Department> Departments { get; set; }
        public ObservableCollection<Position> Positions { get; set; }

        private Department _selectedDepartment;
        public Department SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                Employee.Department.Id = value?.Id ?? 0;
                Employee.Department.Name = value?.Name;
                OnPropertyChanged();
            }
        }

        private Position _selectedPosition;
        public Position SelectedPosition
        {
            get { return _selectedPosition; }
            set
            {
                _selectedPosition = value;
                Employee.Position.Id = value?.Id ?? 0;
                Employee.Position.Name = value?.Name;
                OnPropertyChanged();
            }
        }

        public AddEditEmployeesViewModel(EmployeeWrapper employee)
        {
            SaveCommand = new RelayCommand(Save, CanSave);
            CancelCommand = new RelayCommand(Cancel);

            Employee = employee ?? new EmployeeWrapper();

            // ROZWIĄZANIE: Inicjalizuj zagnieżdżone obiekty, jeśli są null
            if (Employee.Position == null)
                Employee.Position = new PositionWrapper();
            if (Employee.Department == null)
                Employee.Department = new DepartmentWrapper();

            // ROZWIĄZANIE BŁĘDU DATY: Ustaw domyślną datę dla nowego pracownika
            if (Employee.Id == 0)
                Employee.HireDate = DateTime.Now;

            var departments = dataService.GetDepartments();
            departments.Insert(0, new Department { Id = 0, Name = "Brak" });
            Departments = new ObservableCollection<Department>(departments);

            var positions = dataService.GetPositions();
            positions.Insert(0, new Position { Id = 0, Name = "Brak" });
            Positions = new ObservableCollection<Position>(positions);

            // Ustawienie początkowe dla edycji
            if (Employee.Id != 0)
            {
                _selectedDepartment = Departments.FirstOrDefault(d => d.Id == Employee.Department.Id);
                _selectedPosition = Positions.FirstOrDefault(p => p.Id == Employee.Position.Id);
            }
        }

        private void Save(object obj)
        {
            try
            {
                dataService.AddUpdateEmployee(Employee);
                CloseWindow(obj as Window, true);
            }
            catch (Exception ex)
            {
                // Ulepszone logowanie błędów
                var errorMessage = new StringBuilder();
                errorMessage.AppendLine("Wystąpił błąd podczas zapisu:");

                var currentEx = ex;
                while (currentEx != null)
                {
                    errorMessage.AppendLine($"- {currentEx.Message}");
                    currentEx = currentEx.InnerException;
                }

                MessageBox.Show(errorMessage.ToString(), "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSave(object obj)
        {
            return IsValid;
        }

        private void Cancel(object obj)
        {
            CloseWindow(obj as Window, false);
        }

        private void CloseWindow(Window window, bool dialogResult)
        {
            if (window != null)
            {
                window.DialogResult = dialogResult;
                window.Close();
            }
        }

        #region Validation

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(Employee.FirstName):
                        if (string.IsNullOrWhiteSpace(Employee.FirstName))
                            error = "Imię jest wymagane.";
                        break;
                    case nameof(Employee.LastName):
                        if (string.IsNullOrWhiteSpace(Employee.LastName))
                            error = "Nazwisko jest wymagane.";
                        break;
                    case nameof(Employee.Salary):
                        if (Employee.Salary <= 0)
                            error = "Wynagrodzenie musi być większe od zera.";
                        break;
                    case nameof(SelectedDepartment):
                        if (SelectedDepartment == null || SelectedDepartment.Id == 0)
                            error = "Dział jest wymagany.";
                        break;
                    case nameof(SelectedPosition):
                        if (SelectedPosition == null || SelectedPosition.Id == 0)
                            error = "Stanowisko jest wymagane.";
                        break;
                }
                return error;
            }
        }

        public string Error { get { return null; } }

        public bool IsValid
        {
            get
            {
                return string.IsNullOrWhiteSpace(this[nameof(Employee.FirstName)]) &&
                       string.IsNullOrWhiteSpace(this[nameof(Employee.LastName)]) &&
                       string.IsNullOrWhiteSpace(this[nameof(Employee.Salary)]) &&
                       string.IsNullOrWhiteSpace(this[nameof(SelectedDepartment)]) &&
                       string.IsNullOrWhiteSpace(this[nameof(SelectedPosition)]);
            }
        }

        #endregion
    }
}
