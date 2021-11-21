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
using WSR_2021.Model;
using WSR_2021.Utils;

namespace WSR_2021.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Page
    {
        #region Поля

        private Event newEvent = new Event();

        #endregion

        #region Конструктор страницы AddEventPage

        public AddEventPage()
        {
            InitializeComponent();

            DirectionCBox.ItemsSource = Transition.Context.Direction.ToList();
            CityCBox.ItemsSource = Transition.Context.City.ToList();
            EventCBox.ItemsSource = Transition.Context.Event.ToList();

            ActivityGrid.ItemsSource = Transition.Context.Activity.Where(p => p.EventId == newEvent.Id).ToList();
            JuryCBox.ItemsSource = Transition.Context.Users.Where(p => p.Role.Name == "Jury").ToList();
        }

        #endregion

        #region Появление выпадающего списока со схожими значениями вводимого названия мероприятия

        private void TitleEventTBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        #endregion

        #region Переключение выпадающего списка на поле для ввода

        //С целью добавление записи в таблицу "Направление"
        private void AddDirectionBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        //С целью добавление записи в таблицу "Город"
        private void AddCityBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Сохранение нового мероприятия с ее активностями

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Сохранение выбраного мероприятия в выпадающем списке в файл с расширением .csv

        private void SaveCSVBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Переход на страницу 

        private void BoardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
