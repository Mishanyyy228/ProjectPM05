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
    /// Логика взаимодействия для NewOrder.xaml
    /// </summary>
    public partial class NewOrder : Page
    {
        private static int idUser;
        public NewOrder(int number)
        {
            InitializeComponent();
            CmbCourier.ItemsSource=DeliveryServiceDBEntities2.GetContext().Users.ToList().Where(p=>p.RoleId==3);
            CmbPay.ItemsSource = DeliveryServiceDBEntities2.GetContext().PaymentMethods.ToList();
            idUser = number;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ChangeOrders(null));
        }
        
        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            var time = DateTimeOffset.Now.ToUnixTimeSeconds();
            var selectedCourier = CmbCourier.SelectedItem as User;
            var selectedPay = CmbPay.SelectedItem as PaymentMethod;

            if (selectedCourier == null || selectedPay == null)
            {
                MessageBox.Show("Выберите курьера и способ оплаты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var order = new Order
            {
                CourierID = selectedCourier.UserId,
                OrderDateTime = (int)time,          
                ManagerID = idUser,               
                StatusId = 1,                        
                PaymentMethodID = selectedPay.IdPaymentMethod,
                Client = TxtClient.Text,      
                AddressDelivery = TxtAdres.Text
            };
            
            DeliveryServiceDBEntities2.GetContext().Orders.Add(order);
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
}
