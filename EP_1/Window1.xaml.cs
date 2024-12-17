using EP_1.AplicationData;
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
using System.Windows.Shapes;

namespace EP_1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            //проверка на одинаковые значения
            if (AppConnect.model0db.User.Count(x => x.Login==txbLogin.Text)>0)
            {
                MessageBox.Show("пользователь с таким логином есть!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //конструкция на добавление данных в бд
            try
            {
                User user0bj = new User()
                {
                    Name =txbName.Text,
                    Login =txbLogin.Text,
                    Password=psbPass.Password,
                    ID_role=3
                };
                AppConnect.model0db.User.Add(user0bj);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Данные успешно добавлены!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            //если выводится ошибка, завершение приложения
            catch
            {
                MessageBox.Show("Ошибка при добавлении данных!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }

    private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
    {
        string password = psbPass.Password;
        bool isValid = password.Length >= 4 && !Regex.IsMatch(password, @"[^\w\s]");

        //проверка пароля на вводимый текст и валидацию
        if (psbPass.Password != psbPass1.Password || !isValid)
        {
            btnCreate.IsEnabled = false;
            psbPass1.Background=Brushes.LightCoral;
            psbPass1.BorderBrush = Brushes.Red;
        }
        else
        {
            btnCreate.IsEnabled = true;
            psbPass1.Background =Brushes.LightGreen;
            psbPass1.BorderBrush = Brushes.LightGreen;
        }
    }
}
