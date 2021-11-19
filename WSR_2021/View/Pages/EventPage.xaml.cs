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
using WSR_2021.View.Windows;
using WSR_2021.Utils;
using WSR_2021.Model;

namespace WSR_2021.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для EventPage.xaml
    /// </summary>
    public partial class EventPage : Page
    {
        #region Закрытые поля

        private List<Event> ListEvent { get; set; } = Transition.Context.Event.ToList();

        #endregion

        #region Конструктор страницы EventPage

        public EventPage()
        {
            InitializeComponent();

            EventGrid.ItemsSource = ListEvent;

            var allDirection = Transition.Context.Direction.ToList();
            allDirection.Insert(0, new Direction
            {
                Name = "Все направления"
            });
            DirectionCBox.ItemsSource = allDirection;
            DirectionCBox.SelectedIndex = 0;
        }

        #endregion

        #region Переход на авторизацию и закрытие данного окна

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameWindow window = new FrameWindow();
            MainWindow.StartWindow.Visibility = Visibility.Hidden;
            if (window.ShowDialog() == true)
                MainWindow.StartWindow.Visibility = Visibility.Visible;
        }

        #endregion

        #region Сортировка и фильтрация EventGrid

        private void UpdateEventGrid()
        {
            var tempData = ListEvent;

            if (DirectionCBox.SelectedIndex > 0)
                tempData = tempData.Where(p => p.Direction.Name == (DirectionCBox.SelectedItem as Direction).Name).ToList();
            if (DateEventDPic.SelectedDate != null)
                tempData = tempData.Where(p => p.DateEvent == DateEventDPic.SelectedDate).ToList();

            EventGrid.ItemsSource = tempData;
        }

        private void DirectionCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateEventGrid();
        }

        private void DateEventDPic_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateEventGrid();
        }

        private void DateEventDPic_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DateEventDPic.Text == "")
                DateEventDPic.Text = "Выберите дату";
        }

        private void DateEventDPic_GotFocus(object sender, RoutedEventArgs e)
        {
            DateEventDPic.Text = null;
        }

        #endregion

        #region Переход на страницу ViewEventPage для просмотра подробной информации

        private void EventGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ViewEventPage(EventGrid.SelectedItem as Event));
        }

        #endregion
    }
}
