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
    /// Логика взаимодействия для EditChangepage.xaml
    /// </summary>
    public partial class EditChangepage : Page
    {
        public EditChangepage()
        {
            InitializeComponent();
            CmbEmployee.ItemsSource = DeliveryServiceDBEntities2.GetContext().Users.ToList().Where(p => p.RoleId == 2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedCourier = CmbEmployee.SelectedItem as User;
            DateTime selectedDateStart = DateStart.SelectedDate ?? DateTime.Now;
            DateTime selectedDateEnd = DateEnd.SelectedDate ?? DateTime.Now;

            long unixTimestampStart = ((DateTimeOffset)selectedDateStart).ToUnixTimeSeconds();
            long unixTimestampEnd = ((DateTimeOffset)selectedDateEnd).ToUnixTimeSeconds();

            var shft = new Shift
            {
                StartDateTime = (int)unixTimestampStart,
                EndDateTime = (int)unixTimestampEnd,
                Description = DescriptionTxt.Text
            };

            DeliveryServiceDBEntities2.GetContext().Shifts.Add(shft);
            try
            {
                DeliveryServiceDBEntities2.GetContext().SaveChanges();
                MessageBox.Show("Успешно добавлен новый заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.Navigate(new ChangeOrders(null));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            using (var db = DeliveryServiceDBEntities2.GetContext()) 
            {
                int lastInsertedId = db.Shifts.Max(s => s.ShiftID);

                var anyShift = new EmployeeShift
                {
                    EmployeeID = selectedCourier.UserId,
                    ShiftID=lastInsertedId
                };

                DeliveryServiceDBEntities2.GetContext().EmployeeShifts.Add(anyShift);
                try
                {
                    DeliveryServiceDBEntities2.GetContext().SaveChanges();
                    MessageBox.Show("Успешно добавлен новый заказ", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    Manager.MainFrame.Navigate(new ChangeOrders(null));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ChangePageAdmin());
        }
    }
}
