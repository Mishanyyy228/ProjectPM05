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

                if (post==2)
                {
                    MessageBox.Show("Обратитесь к менеджеру");
                }

                if (post == 1)
                {
                    if (idRole == 1)
                    {
                        MessageBox.Show($"Администратор успешно вошел: {loggedInUser.FirstName}");
                        Manager.MainFrame.Navigate(new ChangePageAdmin());
                    }
                    if (idRole == 2)
                    {
                        var checkChange = IsCurrentShiftValid(IdOfUser.Value);
                        if (checkChange == true)
                        {
                            MessageBox.Show($"Менеджер успешно вошел: {loggedInUser.FirstName}");
                            Manager.MainFrame.Navigate(new ChangeOrders());
                        }

                        MessageBox.Show("У вас нет действующих смен. Обратитесь к администратору.");
                    }
                    if (idRole == 3)
                    {
                        var checkChange = checkOrdersCourier(IdOfUser.Value);
                        if (checkChange == true)
                        {
                            MessageBox.Show($"Курьер успешно вошел: {loggedInUser.FirstName}");
                            Manager.MainFrame.Navigate(new courierPage());
                        }
                        MessageBox.Show("У вас нет действующих заказов. Обратитесь к администратору.");
                    }
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

        public bool checkOrdersCourier(int userId)
        {
            using (var dbContext = new DeliveryServiceDBEntities2())
            {
                var user = dbContext.Orders.FirstOrDefault(u => u.CourierID == userId);
                if (user!=null)
                {
                    return true;
                }
                return false;
            }
        }
        public bool IsCurrentShiftValid(int userId)
        {
            using (var dbContext = new DeliveryServiceDBEntities2())
            {
                // Получаем текущее время в формате Unix-time
                long currentUnixTime = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        
                var lastShift = dbContext.EmployeeShifts
                    .Where(es => es.EmployeeID == userId && es.Shift != null)
                    .OrderByDescending(es => es.Shift.StartDateTime)
                    .FirstOrDefault();

                if (lastShift != null)
                {
                    // Сравниваем текущее Unix-время с границами смены
                    if (currentUnixTime >= lastShift.Shift.StartDateTime &&
                        currentUnixTime <= lastShift.Shift.EndDateTime)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
