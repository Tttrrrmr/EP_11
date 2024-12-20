using EP_1.AplicationData;
using EP_1.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using System.Xml.Linq;

namespace EP_1
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private Concert currentConcert;

        private List<Concert> concertList = AppConnect.model0db.Concert.ToList();
        private List<Type_of_concert> typeList = AppConnect.model0db.Type_of_concert.ToList();
        private List<Organizer> organizerList = AppConnect.model0db.Organizer.ToList();
        private List<Singer> singerList = AppConnect.model0db.Singer.ToList();
        private List<City> cityList = AppConnect.model0db.City.ToList();

        public Window3(Concert e)
        {
            InitializeComponent();

            currentConcert = e;

            DataContext = currentConcert;

            ComboType.Items.Add("Выберите тип концерта");

            var selectedType = 0;
            foreach (var item in typeList)
            {
                if (item.ID_type_of_concert==currentConcert.ID_type_of_concert)
                {
                    selectedType = typeList.IndexOf(item) + 1;
                }
                ComboType.Items.Add(item.Name);
            }
            ComboType.SelectedIndex = selectedType;


            ComboArtist.Items.Add("Выберите артиста");

            var selectedArtist = 0;
            foreach (var item in singerList)
            {
                if (item.ID_singer==currentConcert.ID_singer)
                {
                    selectedArtist = singerList.IndexOf(item) + 1;
                }
                ComboArtist.Items.Add(item.Name);
            }
            ComboArtist.SelectedIndex = selectedArtist;


            ComboCity.Items.Add("Выберите город");

            var selectedCity = 0;
            foreach (var item in cityList)
            {
                if (item.ID_city==currentConcert.ID_city)
                {
                    selectedCity = cityList.IndexOf(item) + 1;
                }
                ComboCity.Items.Add(item.Name);
            }
            ComboCity.SelectedIndex = selectedCity;


            ComboOrganiz.Items.Add("Выберите организатора");

            var selectedOrzaniz = 0;
            foreach (var item in organizerList)
            {
                if (item.ID_organizer==currentConcert.ID_organizer)
                {
                    selectedOrzaniz = organizerList.IndexOf(item) + 1;
                }
                ComboOrganiz.Items.Add(item.Name);
            }
            ComboOrganiz.SelectedIndex = selectedOrzaniz;


            var selectedImage = 0;
            ComboImage.Items.Add("Выберите картинку");
            foreach (var item in concertList)
            {
                if (item.Logo==currentConcert.Logo)
                {
                    selectedImage = concertList.IndexOf(item) + 1;
                }
                ComboImage.Items.Add(item.Logo);
            }
            ComboImage.SelectedIndex = selectedImage;


            ComboType.SelectionChanged += new SelectionChangedEventHandler(OnTypeSelected);
            ComboImage.SelectionChanged += new SelectionChangedEventHandler(OnImageSelected);
            ComboOrganiz.SelectionChanged += new SelectionChangedEventHandler(OnOrganizSelected);
            ComboArtist.SelectionChanged += new SelectionChangedEventHandler(OnArtistSelected);
            ComboCity.SelectionChanged += new SelectionChangedEventHandler(OnCitySelected);
            DatePickerConcert.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(OnSelectDate);
        }

        private void OnArtistSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentConcert.ID_singer = singerList[ComboArtist.SelectedIndex - 1].ID_singer;
            }
            catch (Exception)
            {
                currentConcert.ID_singer = 0;
            }
        }

        private void OnCitySelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentConcert.ID_city = cityList[ComboCity.SelectedIndex - 1].ID_city;
            }
            catch (Exception)
            {
                currentConcert.ID_city = 0;
            }
        }

        private void OnTypeSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentConcert.ID_singer = typeList[ComboType.SelectedIndex - 1].ID_type_of_concert;
            }
            catch (Exception)
            {
                currentConcert.ID_type_of_concert = 0;
            }
        }
        private void OnOrganizSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentConcert.ID_organizer = organizerList[ComboOrganiz.SelectedIndex - 1].ID_organizer;
            }
            catch (Exception)
            {
                currentConcert.ID_organizer = 0;
            }
        }

        private void OnSelectDate(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerConcert.SelectedDate != null)
            {
                currentConcert.Date_concert = (DateTime)DatePickerConcert.SelectedDate;
            }
        }

        private void OnImageSelected(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                currentConcert.Logo = (String)ComboImage.SelectedItem;
            }
            catch (Exception)
            {
                currentConcert.Logo = " ";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentConcert.Name))
                errors.AppendLine("Укажите название концерта");
            if (currentConcert.Cost == 0)
                errors.AppendLine("Укажите цену концерта");
            if (currentConcert.Date_concert == System.DateTime.MinValue)
                errors.AppendLine("Укажите дату концерта");

            string name = TextName.Text.Trim();
            if (!Regex.IsMatch(name, "[а-яА-Я]"))
            {
                MessageBox.Show("Имя  может содерджать только кириллицу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                AppConnect.model0db.Concert.AddOrUpdate(currentConcert);
                AppConnect.model0db.SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
