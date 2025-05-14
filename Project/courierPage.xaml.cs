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
    /// Логика взаимодействия для courierPage.xaml
    /// </summary>
    public partial class courierPage : Page
    {
        public courierPage()
        {
            InitializeComponent();
            DataGridCourier.ItemsSource = DeliveryServiceDBEntities2.GetContext().Orders.ToList();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditOrder((sender as Button).DataContext as Order));
        }
    }
}
