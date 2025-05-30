﻿using System;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            DataGridOrders.ItemsSource=DeliveryServiceDBEntities2.GetContext().Orders.ToList();
        }

        private void ButtonEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new Employee()); 
        }

        private void BtnOrders_OnClick(object sender, RoutedEventArgs e)
        {
           Manager.MainFrame.Navigate(new OrdersPage());
        }

        private void BtnChange_OnClick(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ChangePageAdmin());  
        }
    }
}
