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
using Project.Model;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для ChangePageAdmin.xaml
    /// </summary>
    public partial class ChangePageAdmin : Page
    {
        public ChangePageAdmin()
        {
            InitializeComponent();
        }

        private void BtnUvol_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditChangepage());

        }

        private void ChangePageAdmin_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DeliveryServiceDBEntities2.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataGridChange.ItemsSource = DeliveryServiceDBEntities2.GetContext().EmployeeShifts.ToList();
            }
        }

        private void BtnChange_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ChangePageAdmin());
        }

        private void BtnOrder_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new OrdersPage());
        }

        private void BtnEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Employee());
        }
    }
}
