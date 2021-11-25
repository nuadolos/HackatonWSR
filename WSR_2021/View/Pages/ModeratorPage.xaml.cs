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
    /// Логика взаимодействия для ModeratorPage.xaml
    /// </summary>
    public partial class ModeratorPage : Page
    {
        #region Закрытые поля

        private List<EventActivity> listActEvent = Transition.Context.EventActivity.ToList();

        private Users user = new Users();

        #endregion

        #region Конструктор страницы ModeratorPage

        public ModeratorPage(Account transferAccount)
        {
            InitializeComponent();

            user = Transition.Context.Users.FirstOrDefault(p => p.Id == transferAccount.NumberId);

            var allDirection = Transition.Context.Direction.ToList();
            allDirection.Insert(0, new Direction
            {
                Name = "Все направления"
            });
            DirectionCBox.ItemsSource = allDirection;
            DirectionCBox.SelectedIndex = 0;

            var allEvents = Transition.Context.Event.ToList();
            allEvents.Insert(0, new Event { Title = "Все мероприятия" });
            EventCBox.ItemsSource = allEvents;
            EventCBox.SelectedIndex = 0;

            ActivityGrid.ItemsSource = listActEvent;

            //Формирование текста приветствия пользователя в WelcomeText

            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 11)
                WelcomeText.Text = "Доброе утро!";
            else if (DateTime.Now.Hour >= 11.01 && DateTime.Now.Hour <= 18)
                WelcomeText.Text = "Добрый день!";
            else if (DateTime.Now.Hour >= 18.01 && DateTime.Now.Hour <= 24)
                WelcomeText.Text = "Добрый вечер!";

            switch (user.GenderId)
            {
                case 1:
                    WelcomeText.Text += $"\nMr {user.Name}";
                    break;

                case 2:
                    WelcomeText.Text += $"\nMrs {user.Name}";
                    break;
            }
        }

        #endregion

        #region Сортировка и фильтрация ActivityGrid

        private void UpdateActivityGrid()
        {
            var tempData = listActEvent;

            if (DirectionCBox.SelectedIndex > 0)
                tempData = tempData.Where(p => p.Event.Direction.Name == (DirectionCBox.SelectedItem as Direction).Name).ToList();

            if (EventCBox.SelectedIndex > 0)
                tempData = tempData.Where(p => p.Event.Id == (EventCBox.SelectedItem as Event).Id).ToList();

            ActivityGrid.ItemsSource = tempData;
        }

        private void EventCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateActivityGrid();
        }

        private void DirectionCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateActivityGrid();
        }

        #endregion

        #region Переход на другие страницы

        //Переход на страницу для подачи заявки на новую активность
        private void RequestBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        //Переход на страницу MyActivityPage
        private void MyActivityBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
