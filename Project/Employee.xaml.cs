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
    /// Логика взаимодействия для Employee.xaml
    /// </summary>
    public partial class Employee : Page
    {
        private User _user = new User();
        public Employee()
        {
            InitializeComponent();
            DataGridUsers.ItemsSource = DeliveryServiceDBEntities2.GetContext().Users.ToList().Where(p=>p.StatusId==1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ChangePageAdmin());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new OrdersPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Employee());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedUser = DataGridUsers.SelectedItem as User;

                if (selectedUser != null && selectedUser.StatusId.HasValue)
                {
                    bool isActive = selectedUser.StatusId.Value == 1;
                    
                    selectedUser.StatusId = !isActive ? 1 : 2; 

                    DeliveryServiceDBEntities2.GetContext().SaveChanges();
                    DataGridUsers.ItemsSource = DeliveryServiceDBEntities2.GetContext().Users.ToList().Where(p=>p.StatusId==1);
                    MessageBox.Show("Статус пользователя успешно изменён.");
                }
                else
                {
                    MessageBox.Show("Пользователь не выбран или статус не установлен.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении статуса: {ex.Message}");
            }
        }

        private void NewEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new NewmployeePage());
        }
    }
}
