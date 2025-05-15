using Project.Model;
using System.Data.Entity;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project
{
    public partial class EditOrder : Page
    {
        private static int? userId;
        private Order thisOrder;
        public EditOrder(Order selectedorder)
        {
            InitializeComponent();
            if (selectedorder != null)
            {
                thisOrder = selectedorder;
            }
            DataContext = thisOrder;
            userId = IdOfUser.Value;

            CmbCourier.ItemsSource = DeliveryServiceDBEntities2.GetContext().Users.ToList();
            CmbPay.ItemsSource = DeliveryServiceDBEntities2.GetContext().PaymentMethods.ToList();
            CmbStatus.ItemsSource = DeliveryServiceDBEntities2.GetContext().OrderStatus.ToList();
        }

        private void BtnSave_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selectedCourier = CmbCourier.SelectedItem as User;
            var selectedpay = CmbPay.SelectedItem as PaymentMethod;
            var selectedStatus = CmbStatus.SelectedItem as OrderStatu;

            thisOrder = new Order
            {
                CourierID = selectedCourier.UserId,
                StatusId = selectedStatus.IdStatus,
                PaymentMethodID = selectedpay.IdPaymentMethod,
                Client = TxtClient.Text,
                AddressDelivery = TxtAdres.Text
            };

            DeliveryServiceDBEntities2.GetContext().Orders.Add(thisOrder);
            try
            {
                DeliveryServiceDBEntities2.GetContext().SaveChanges();
                MessageBox.Show("Заказ изменен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.Navigate(new ChangeOrders());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ButtonBase_OnClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new courierPage());
        }
    }
}