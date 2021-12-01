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

        private int addControlElement = 0;
        private TextBox[] titleActivityTBox = new TextBox[13];
        private ComboBox[] timeActivityCBox = new ComboBox[13];
        private ComboBox[] juryActivityCBox = new ComboBox[13];
        private Activity[] addNewAct = new Activity[13];

        private TimeSpan start = new TimeSpan();
        private TimeSpan end = new TimeSpan();

        private List<Activity> selectedAct = new List<Activity>();
        private Event newEvent = new Event();
        private Direction newDrirection = new Direction();
        private City newCity = new City();

        private int countPressingDirBtn = 0;
        private int countPressingCityBtn = 0;

        #endregion

        #region Конструктор страницы AddEventPage

        public AddEventPage()
        {
            InitializeComponent();

            DirectionCBox.ItemsSource = Transition.Context.Direction.ToList();
            CityCBox.ItemsSource = Transition.Context.City.ToList();
            EventCBox.ItemsSource = Transition.Context.Event.ToList();

            CreateControlElement();

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
            countPressingDirBtn++;

            if (countPressingDirBtn % 2 == 0)
            {
                DirectionTBox.Text = null;
                DirectionCBox.Visibility = Visibility.Visible;
            }
            else
            {
                DirectionCBox.SelectedItem = null;
                DirectionCBox.Visibility = Visibility.Hidden;
            }

            if (countPressingDirBtn == 10)
                countPressingDirBtn = 0;
        }

        //С целью добавление записи в таблицу "Город"
        private void AddCityBtn_Click(object sender, RoutedEventArgs e)
        {
            countPressingCityBtn++;

            if (countPressingCityBtn % 2 == 0)
            {
                CityTBox.Text = null;
                CityCBox.Visibility = Visibility.Visible;
            }
            else
            {
                CityCBox.SelectedItem = null;
                CityCBox.Visibility = Visibility.Hidden;
            }

            if (countPressingCityBtn == 10)
                countPressingCityBtn = 0;
        }

        #endregion

        #region Создание коллекции с доступным временем для каждой активности

        private void StartEventTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                start = TimeSpan.Parse(StartEventTBox.Text.Split(' ')[1]);

                foreach (var item in timeActivityCBox)
                {
                    if (item != null)
                        item.ItemsSource = ListTimeEvent(start, end);
                }
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

                foreach (var item in timeActivityCBox)
                {
                    if (item != null)
                        item.ItemsSource = ListTimeEvent(start, end);
                }
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
                if (StartEventTBox.Text.Split(' ')[0] == EndEventTBox.Text.Split(' ')[0])
                    newEvent.DateEvent = DateTime.Parse(StartEventTBox.Text.Split(' ')[0]);
                else
                    error.AppendLine("Даты должны совпадать");
                                      
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
            char[] titleSymbol = TitleEventTBox.Text.ToCharArray();
            foreach (var symbol in titleSymbol)
            {
                if (!char.IsWhiteSpace(symbol) && !char.IsLetterOrDigit(symbol))
                {
                    error.AppendLine($"Символ {symbol} в названии мероприятия не допустим");
                }
            }

            if (countPressingDirBtn % 2 == 0)
            {
                if (DirectionCBox.SelectedItem == null)
                    error.AppendLine("Выберите направление");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(DirectionTBox.Text))
                    error.AppendLine("Укажите направление");
                else
                    newDrirection.Name = DirectionTBox.Text;
            }
            if (countPressingCityBtn % 2 == 0)
            {
                if (CityCBox.SelectedItem == null)
                    error.AppendLine("Выберите город");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(CityTBox.Text))
                    error.AppendLine("Укажите город");
                else
                    newCity.Name = CityTBox.Text;
            }

            if (error.Length > 0)
            {
                MessageBox.Show($"При сохранении возникли следующие ошибки:\n{error}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (AddItemActivity() == false)
                return;

            if (newEvent.Id == 0)
            {
                foreach (var item in addNewAct)
                {
                    if (item != null)
                    {
                        EventActivity evAct = new EventActivity()
                        {
                            EventId = 0,
                            ActivityId = item.Id
                        };

                        Transition.Context.EventActivity.Add(evAct);
                    } 
                }

                if (countPressingDirBtn % 2 != 0)
                    Transition.Context.Direction.Add(newDrirection);
                if (countPressingCityBtn % 2 != 0)
                    Transition.Context.City.Add(newCity);

                Transition.Context.Event.Add(newEvent);
            }

            try
            {
                Transition.Context.SaveChanges();
                MessageBox.Show("Данные сохранены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Transition.MainFrame.GoBack();
            }
            catch (Exception er)
            {
                MessageBox.Show($"При сохранении данных произошла ошибка: {er.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод AddItemActivity, отвечающий за добавление новых экземляров класса Activity в таблицу Activity
        /// </summary>
        private bool AddItemActivity()
        {
            for (int i = 0; i < addControlElement; i++)
            {
                addNewAct[i] = new Activity();

                addNewAct[i].Title = titleActivityTBox[i].Text;
                if (timeActivityCBox[i].SelectedValue != null)
                    addNewAct[i].TimeActivity = TimeSpan.Parse(timeActivityCBox[i].SelectedValue.ToString());
                if (juryActivityCBox[i].SelectedItem != null)
                    addNewAct[i].JuryId = (juryActivityCBox[i].SelectedItem as Users).Id;

                if (!string.IsNullOrWhiteSpace(addNewAct[i].Title) || timeActivityCBox[i].SelectedValue != null || juryActivityCBox[i].SelectedItem != null)
                {
                    StringBuilder error = new StringBuilder();

                    if (string.IsNullOrWhiteSpace(addNewAct[i].Title))
                        error.AppendLine($"Укажите название {i + 1} активности");
                    if (timeActivityCBox[i].SelectedValue == null)
                        error.AppendLine($"Выберите время {i + 1} активности");
                    if (juryActivityCBox[i].SelectedItem == null)
                        error.AppendLine($"Выберите жюри {i + 1} активности");

                    if (error.Length > 0)
                    {
                        MessageBox.Show($"При сохранении активностей возникли следующие ошибки:\n{error}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }

                    Transition.Context.Activity.Add(addNewAct[i]);
                }
                else
                    addNewAct[i] = null;
            }
            
            try
            {
                Transition.Context.SaveChanges();
                return true;
            }
            catch (Exception er)
            {
                MessageBox.Show($"При добавлении активностей произошла ошибка: {er.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
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
                    csvFile.WriteLine($"{eventWrite.Title};{eventWrite.Direction.Name};{eventWrite.City.Name};{eventWrite.DateEvent.ToShortDateString()};" +
                        $"{eventWrite.StartEvent};{eventWrite.EndEvent};{eventWrite.Description}");
                }
                MessageBox.Show("Файл сохранен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Для сохранения файла .csv необходимо выбрать мероприятие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

        #region Переход на страницу KanbanBoard

        private void BoardBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.Navigate(new KanbanBoard());
        }

        #endregion

        #region Добавление элементов управления при нажатии на кнопку "+"

        private void AddActBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateControlElement();
        }

        private void CreateControlElement()
        {
            if (addControlElement < addNewAct.Length)
            {
                titleActivityTBox[addControlElement] = new TextBox() { Width = 360 };
                timeActivityCBox[addControlElement] = new ComboBox() { Width = 150, Margin = new Thickness(20, 0, 15, 0), ItemsSource = ListTimeEvent(start, end) };
                juryActivityCBox[addControlElement] = new ComboBox() { Width = 150, DisplayMemberPath = "Surname", ItemsSource = Transition.Context.Users.Where(p => p.Role.Name == "Jury").ToList() };

                WrapPanel ActivityWrapP = new WrapPanel() { HorizontalAlignment = HorizontalAlignment.Stretch };

                ActivityWrapP.Children.Add(titleActivityTBox[addControlElement]);
                ActivityWrapP.Children.Add(timeActivityCBox[addControlElement]);
                ActivityWrapP.Children.Add(juryActivityCBox[addControlElement]);

                ActivityLV.Items.Add(ActivityWrapP);

                addControlElement++;
            }
            else
                MessageBox.Show("Количество активностей превышает норму", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        #endregion
    }
}
