using Project.Model;
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

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private User _user = new User();
        public LoginPage()
        {
            InitializeComponent();
            DataContext = _user;
        }

        private void Button_Click(object sender, object e)
        {
            var login=TxtLogin.Text;
            var password = TxtPassword.Text;

            User loggedInUser = Authenticate(login, password);
            if (loggedInUser != null)
            {
                var post = loggedInUser.StatusId;
                int? idRole = loggedInUser.RoleId;

                IdOfUser.Value = loggedInUser.UserId;

                if (post==1)
                {
                    MessageBox.Show("Обратитесь к менеджеру");
                }
                MessageBox.Show($"Пользователь успешно вошел: {loggedInUser.FirstName}");
               
                 if (idRole == 1)
                {
                    Manager.MainFrame.Navigate(new ChangePageAdmin());
                }
                if (idRole == 2)
                {
                    Manager.MainFrame.Navigate(new ChangeOrders());
                }
                if (idRole == 3)
                {
                    Manager.MainFrame.Navigate(new courierPage());
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        public User Authenticate(string username, string password)
        {
            using (var dbContext = new DeliveryServiceDBEntities2())
            {
                var user = dbContext.Users.FirstOrDefault(u =>
                    u.Email == username &&
                    u.Password == password);

                return user;
            }
        }
    }
}
