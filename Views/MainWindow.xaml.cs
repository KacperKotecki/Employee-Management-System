using Employee_Management_System.ViewModels;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Employee_Management_System.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); //DataContext to specjalna właściwość, którą posiada każdy element interfejsu w WPF. Mówi ona: "Oto obiekt, z którego będę czerpać wszystkie dane do wyświetlenia". Kiedy ustawisz DataContext dla całego okna, wszystkie elementy wewnątrz tego okna (przyciski, pola tekstowe itp.) dziedziczą ten sam DataContext.
        }
    }
}
