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
    /// Логика взаимодействия для ChangeOrders.xaml
    /// </summary>
    public partial class ChangeOrders : Page
    {
        private static int? userID;
        //менеджер
        public ChangeOrders()
        {
            InitializeComponent();
            userID = IdOfUser.Value;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new NewOrder());
        }

        private void ChangeOrders_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                DeliveryServiceDBEntities2.GetContext().ChangeTracker.Entries().ToList().ForEach(p=>p.Reload());
                GridSource.ItemsSource = DeliveryServiceDBEntities2.GetContext().Orders.ToList().Where(p=>p.ManagerID==userID);   
            }
        }
    }
}
