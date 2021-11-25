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
    /// Логика взаимодействия для MyActivityPage.xaml
    /// </summary>
    public partial class MyActivityPage : Page
    {
        #region Закрытые поля

        private Users user = new Users();

        private int navigatePage = 1;
        private int totalPage;

        int indexActivity;
        private int displayActivities = 0;
        private Button[] activityBtn;
        private Activity[] activities;

        #endregion

        #region Конструктор страницы MyActivityPage

        public MyActivityPage(Users transferUser)
        {
            InitializeComponent();

            user = transferUser;

            activities = Transition.Context.Activity.Where(p => p.JuryId == user.Id).ToArray();
            activityBtn = new Button[activities.Length];

            totalPage = (int)Math.Ceiling((decimal)activityBtn.Length / 3);

            if (totalPage <= 1)
                NextBtn.Visibility = Visibility.Hidden;

            CreateActivity();
            DisplayActivity();

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

        #region Работа с ресурсами определенной активности

        private void AddDocBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewDocBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Кнопки активностей модератора

        #region Прокрутка доступных активностей по 3 элемента

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            navigatePage--;

            for (int i = 0; i < 3; i++)
            {
                if (displayActivities > 0)
                {
                    displayActivities--;
                    NextBtn.Visibility = Visibility.Visible;
                }
                else
                    break;
            }

            if (navigatePage == 1)
                PreviousBtn.Visibility = Visibility.Hidden;

            DisplayActivity();
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            navigatePage++;

            for (int i = 0; i < 3; i++)
            {
                if (displayActivities < activityBtn.Length)
                {
                    displayActivities++;
                    PreviousBtn.Visibility = Visibility.Visible;
                }
                else
                    break;
            }

            if (navigatePage == totalPage)
                NextBtn.Visibility = Visibility.Hidden;

            DisplayActivity();
        }

        #endregion

        #region Создание, отображение и обработка нажатия кнопок активностей модератора

        private void CreateActivity()
        {
            for (int i = 0; i < activityBtn.Length; i++)
            {
                activityBtn[i] = new Button()
                {
                    Height = 50,
                    Content = $"{i + 1}.{activities[i].Title}, {activities[i].TimeActivity}",
                    Width = 275
                };
                activityBtn[i].Click += ActivityBtn_Click;
                ActivityBtnPanel.Children.Add(activityBtn[i]);
            }
        }

        private void DisplayActivity()
        {
            for (int i = 0; i < activityBtn.Length; i++)
                activityBtn[i].Visibility = Visibility.Collapsed;

            for (int i = displayActivities; i < displayActivities + 3; i++)
                activityBtn[i].Visibility = Visibility.Visible;
        }

        private void ActivityBtn_Click(object sender, RoutedEventArgs e)
        {
            indexActivity = int.Parse((sender as Button).Content.ToString().Split('.')[0]);
            MenuActGrid.Visibility = Visibility.Visible;
            ParticipantActivity.Visibility = Visibility.Visible;
            ResourcesActivity.Visibility = Visibility.Hidden;

            var item = activities[indexActivity - 1];
            ParticipantLV.ItemsSource = Transition.Context.ActivityParticipant.Where(p => p.ActivityId == item.Id).ToList();
        }

        #endregion

        #endregion

        #region Отображение данных вкладок "Участники" и "Ресурсы" для определенной активности 

        private void ParMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ResourcesActivity.Visibility = Visibility.Hidden;
            ParticipantActivity.Visibility = Visibility.Visible;

            var item = activities[indexActivity - 1];
            ParticipantLV.ItemsSource = Transition.Context.ActivityParticipant.Where(p => p.ActivityId == item.Id).ToList();
        }

        private void ResMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ParticipantActivity.Visibility = Visibility.Hidden;
            ResourcesActivity.Visibility = Visibility.Visible;

            var item = activities[indexActivity - 1];
            ResourcesLV.ItemsSource = Transition.Context.ActivityDocument.Where(p => p.ActivityId == item.Id).ToList();
        }

        #endregion

        #region Переход на страницу

        private void KanbanBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
