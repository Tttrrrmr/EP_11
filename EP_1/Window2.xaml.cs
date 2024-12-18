using EP_1.AplicationData;
using EP_1.Model;
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
using System.Windows.Shapes;

namespace EP_1
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public int ID_user { get; set; }
        public int Role { get; set; }

        public Window2(int role, int user)
        {
            InitializeComponent();
            ID_user = user;
            Role = role;

            ListOfConcert.ItemsSource = AppConnect.model0db.Concert.ToList();
            ComboFilter.Items.Add("Все цены");
            ComboFilter.Items.Add("от 1000 до 2500");
            ComboFilter.Items.Add("от 2501 до 3500");
            ComboFilter.Items.Add("от 4000");
            ComboSort.Items.Add("от А до Я");
            ComboSort.Items.Add("от Я до А");

            ListOfConcert.SelectionChanged += new SelectionChangedEventHandler((s, e) =>
            {
                BtnEdit.IsEnabled = ListOfConcert.SelectedItem != null && role != 3;
                BtnDelete.IsEnabled = ListOfConcert.SelectedItem != null && role != 3;
                BtnDelete.IsEnabled = ListOfConcert.SelectedItem != null && role != 2;
            });
            FocusableChanged += new DependencyPropertyChangedEventHandler((s, e) =>
            {
                ListOfConcert.ItemsSource = AppConnect.model0db.Concert.ToList();
            });

            
            if (role == 2)
            {
                BtnDelete.IsEnabled = false;
            }

            if (role == 3)
            {
                BtnAdd.IsEnabled = false;
                BtnDelete.IsEnabled = false;
                BtnEdit.IsEnabled = false;
            }
        }

        private void Edit(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Concert)((FrameworkElement)sender).DataContext;
        }

        Concert[] FindConcert()
        {
            var concert = AppConnect.model0db.Concert.ToList();
            var concertall = concert;

            //поиск по названию проекта
            if (TextSearch.Text != null)
            {
                concert = concert.Where(x => x.Name.ToLower().Contains(TextSearch.Text.ToLower())).ToList();
            }

            //фильтрация по типу проекта
            if (ComboFilter.SelectedIndex >= 0)
            {
                switch (ComboFilter.SelectedIndex)
                {
                    case 0:
                        concert=concert.Where(x => x.Cost > 1000 && x.Cost < 2500).ToList();
                        break;
                    case 1:
                        concert=concert.Where(x => x.Cost >= 2501 && x.Cost < 3500).ToList();
                        break;
                    case 2:
                        concert=concert.Where(x => x.Cost >= 4000).ToList();
                        break;
                }
            }

            //сортировка по алфавиту
            if (ComboSort.SelectedIndex >= 0)
            {
                switch (ComboSort.SelectedIndex)
                {
                    case 0:
                        {
                            concert=concert.OrderBy(x => x.Name).ToList();
                            break;
                        }
                    case 1:
                        {
                            concert=concert.OrderByDescending(x => x.Name).ToList();
                            break;
                        }
                }
            }

            //количество найденных элементов

            if (concert.Count() > 0)
            {
                LabelCount.Content = "Найдено " + concert.Count() + " из " + concertall.Count;
            }
            else
            {
                LabelCount.Content = "Ничего не найдено ";
            }
            return concert.ToArray();
        }

        private void TextSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListOfConcert.ItemsSource = FindConcert();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListOfConcert.ItemsSource = FindConcert();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListOfConcert.ItemsSource = FindConcert();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Window3 window = new Window3(new Concert());
            window.Show();
        }

        //кнопка редактировать берет данные из списка 
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Window3 window = new Window3((Concert)ListOfConcert.SelectedItem);
            window.Show();

        }

        //при выборе конктреной записи будет срабатываться кнопка удалить, после чего данные стираются из бд и она обновляется
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            AppConnect.model0db.Concert.Remove((Concert)ListOfConcert.SelectedItem);
            AppConnect.model0db.SaveChanges();
            ListOfConcert.ItemsSource = AppConnect.model0db.Concert.ToList();
        }

    }
}
