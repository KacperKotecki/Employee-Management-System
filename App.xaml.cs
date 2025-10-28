using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Employee_Management_System
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var metroWindow = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault(x => x.IsActive);
            if (metroWindow == null) return; // Zabezpieczenie, jeśli żadne okno nie jest aktywne

            var result = await metroWindow.ShowMessageAsync("Nieoczekiwany wyjątek",
                $"Wystąpił nioczekiwany wyjątek {e.Exception.Message}",
                MessageDialogStyle.Affirmative);

            e.Handled = true;
        }
    }
}
