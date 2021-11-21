using System;
using System.Collections.Generic;
using System.IO;
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
        #region Закрытые поля

        private TimeSpan start = new TimeSpan();
        private TimeSpan end = new TimeSpan();

        private List<Activity> selectedAct = new List<Activity>();
        private Event newEvent = new Event();
        private Direction newDrirection = new Direction();
        private City newCity = new City();

        private int CountPressingDirBtn = 0;
        private int CountPressingCityBtn = 0;

        #endregion

        #region Конструктор страницы AddEventPage

        public AddEventPage()
        {
            InitializeComponent();

            DirectionCBox.ItemsSource = Transition.Context.Direction.ToList();
            CityCBox.ItemsSource = Transition.Context.City.ToList();
            EventCBox.ItemsSource = Transition.Context.Event.ToList();

            ActivityGrid.ItemsSource = Transition.Context.Activity.ToList();
            JuryCBox.ItemsSource = Transition.Context.Users.Where(p => p.Role.Name == "Jury").ToList();

            DataContext = newEvent;
        }

        #endregion

        #region Появление выпадающего списока со схожими значениями вводимого названия мероприятия

        private void TitleEventTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var title = Transition.Context.Event.Where(p => p.Title.ToLower().Contains(TitleEventTBox.Text.ToLower())).ToList();

            if (!string.IsNullOrWhiteSpace(TitleEventTBox.Text))
            {
                if (title.Count > 0)
                {
                    TitleEventCBox.IsDropDownOpen = true;
                    TitleEventCBox.ItemsSource = title;
                }
                else
                    TitleEventCBox.IsDropDownOpen = false;
            }
            else
                TitleEventCBox.IsDropDownOpen = false;
        }

        private void TitleEventCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TitleEventTBox.Text = (TitleEventCBox.SelectedItem as Event).Title;
        }

        #endregion

        #region Переключение выпадающего списка на поле для ввода

        //С целью добавление записи в таблицу "Направление"
        private void AddDirectionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CountPressingDirBtn % 2 == 0)
            {
                DirectionCBox.SelectedItem = null;
                DirectionCBox.Visibility = Visibility.Hidden;
            }
            else
            {
                DirectionTBox.Text = null;
                DirectionCBox.Visibility = Visibility.Visible;
            }

            CountPressingDirBtn++;
            if (CountPressingDirBtn == 10)
                CountPressingDirBtn = 0;
        }

        //С целью добавление записи в таблицу "Город"
        private void AddCityBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CountPressingCityBtn % 2 == 0)
            {
                CityCBox.SelectedItem = null;
                CityCBox.Visibility = Visibility.Hidden;
            }
            else
            {
                CityTBox.Text = null;
                CityCBox.Visibility = Visibility.Visible;
            }

            CountPressingCityBtn++;
            if (CountPressingCityBtn == 10)
                CountPressingCityBtn = 0;
        }

        #endregion

        #region Создание коллекции с доступным временем для каждой активности

        private void StartEventTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                start = TimeSpan.Parse(StartEventTBox.Text.Split(' ')[1]);
                TimeActCBox.ItemsSource = ListTimeEvent(start, end);
            }
            catch (Exception)
            {

            }
        }

        private void EndEventTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                end = TimeSpan.Parse(EndEventTBox.Text.Split(' ')[1]);
                TimeActCBox.ItemsSource = ListTimeEvent(start, end);
            }
            catch (Exception)
            {

            }
        }

        private List<TimeSpan> ListTimeEvent(TimeSpan start, TimeSpan end)
        {
            List<TimeSpan> listTime = new List<TimeSpan>();
            if (end.Hours == 0)
                end = new TimeSpan(24, 0, 0);

            for (TimeSpan i = start; i < end; i += TimeSpan.FromMinutes(105))
            {
                if (i + TimeSpan.FromMinutes(105) > end)
                    break;
                listTime.Add(i);
            }

            return listTime;
        }

        #endregion

        #region Сохранение нового мероприятия с ее активностями

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            try
            {
                newEvent.StartEvent = TimeSpan.Parse(StartEventTBox.Text.Split(' ')[1]);
                newEvent.EndEvent = TimeSpan.Parse(EndEventTBox.Text.Split(' ')[1]);
            }
            catch (Exception)
            {
                error.AppendLine("Укажите дату и время начала и окончания мероприятия;");
            }

            newEvent.Title = TitleEventTBox.Text;
            if (string.IsNullOrWhiteSpace(newEvent.Title))
                error.AppendLine("Укажите название");

            if (CountPressingDirBtn % 2 == 0)
            {
                if (string.IsNullOrWhiteSpace(DirectionTBox.Text))
                    error.AppendLine("Укажите направление");
                else
                    newDrirection.Name = DirectionTBox.Text;
            }
            else
            {
                if (DirectionCBox.SelectedItem == null)
                    error.AppendLine("Выберите направление");
            }
            if (CountPressingCityBtn % 2 == 0)
            {
                if (string.IsNullOrWhiteSpace(CityTBox.Text))
                    error.AppendLine("Укажите город");
                else
                    newDrirection.Name = CityTBox.Text;
            }
            else
            {
                if (CityCBox.SelectedItem == null)
                    error.AppendLine("Выберите город");
            }

            //Реализовать добавление активностей без помощи DataGrid
            //Возможно понадобится метод добавления элементов управления, выполняющийся после кнопки "+"
        }

        #endregion

        #region Сохранение выбраного мероприятия в выпадающем списке в файл с расширением .csv

        private void SaveCSVBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EventCBox.SelectedItem != null)
            {
                var eventWrite = EventCBox.SelectedItem as Event;
                string path = $@"C:\Users\nuadolos\Desktop\Event_{eventWrite.Title}.csv";

                using (StreamWriter csvFile = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
                {
                    csvFile.WriteLine("Название;Направление;Город;Дата мероприятия;Начало;Окончание;Описание");
                    csvFile.WriteLine($"{eventWrite.Title};{eventWrite.Direction.Name};{eventWrite.City.Name};{eventWrite.DateEvent};" +
                        $"{eventWrite.StartEvent};{eventWrite.EndEvent};{eventWrite.Description}");
                }
                MessageBox.Show("Файл сохранен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Для сохранения файла .csv необходимо выбрать мероприятие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #region Переход на страницу 

        private void BoardBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
