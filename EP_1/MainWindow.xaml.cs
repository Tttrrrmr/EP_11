﻿using EP_1.AplicationData;
using EP_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace EP_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.model0db = new Model.EPEntities1();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = txbLogin.Text.Trim();
            if (!Regex.IsMatch(login, "^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Логин может содержать только латинские буквы и цифры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string password = psbPassword.Password;
            bool isValid = password.Length <= 4 && !Regex.IsMatch(password, @"[^\w\s]");


            User user = new User();
            try
            {
                user = AppConnect.model0db.User.FirstOrDefault(x => x.Login == txbLogin.Text && x.Password == psbPassword.Password);
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации!",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    switch (user.ID_role)
                    {
                        case 1:
                            MessageBox.Show("Здравствуйте, администратор " + user.Name + "!",
                                "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, менеджер " + user.Name + "!",
                               "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        case 3:
                            MessageBox.Show("Здравствуйте, клиент " + user.Name + "!",
                               "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }

                }

                Window2 window = new Window2(user.ID_role, user.ID_user);
                window.Show();
                this.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка " + Ex.Message.ToString() + "Критическая работа приложения!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Show();
            this.Close();
        }
    }
}
