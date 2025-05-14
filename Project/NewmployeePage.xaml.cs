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
    /// Логика взаимодействия для NewmployeePage.xaml
    /// </summary>
    public partial class NewmployeePage : Page
    {
        private User _user = new User();
        public NewmployeePage()
        {
            InitializeComponent();
            DataContext = _user;
            CmbRole.ItemsSource = DeliveryServiceDBEntities2.GetContext().UserRoles.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Employee());
        }

        private void ButtonNew_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedRole = CmbRole.SelectedItem as UserRole; // теперь выбираем объект UserRole

            if (selectedRole == null || TxtPass1.Text != TxtPass.Text)
            {
                MessageBox.Show("Выберите роль или убедитесь, что пароли совпадают!");
                return;
            }

            var user = new User
            {
                FirstName = TxtName.Text,
                LastName = TxtFamily.Text,
                PhoneNumber = TxtPhone.Text,
                Email = TxtEmail.Text, // тут была ошибка, использовано TxtPhone.Text вместо TxtEmail.Text
                Password = TxtPass.Text,
                UserRole = selectedRole, // теперь передаем объект роли
                StatusId = 1
            };

            if (TxtPass1.Text != TxtPass.Text)
            {
                MessageBox.Show("пароли должны совпадать");
            }
            DeliveryServiceDBEntities2.GetContext().Users.Add(user);
            try
            {
                DeliveryServiceDBEntities2.GetContext().SaveChanges();
                MessageBox.Show("Сотрудник добавлен");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }
    }
}
