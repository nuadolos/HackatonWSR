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
    /// Логика взаимодействия для OrganizerPage.xaml
    /// </summary>
    public partial class OrganizerPage : Page
    {
        #region Закрытые поля

        private Users tempUser = new Users();
        private Account tempAccount = new Account();

        #endregion

        #region Конструктор страницы OrganizerPage

        public OrganizerPage(Users transferUser, Account transferAccount)
        {
            InitializeComponent();

            tempUser = transferUser;
            tempAccount = transferAccount;

            //Формирование текста приветствия пользователя в WelcomeText

            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 11)
                WelcomeText.Text = "Доброе утро!";
            else if (DateTime.Now.Hour >= 11.01 && DateTime.Now.Hour <= 18)
                WelcomeText.Text = "Добрый день!";
            else if (DateTime.Now.Hour >= 18.01 && DateTime.Now.Hour <= 24)
                WelcomeText.Text = "Добрый вечер!";

            switch (tempUser.GenderId)
            {
                case 1:
                    WelcomeText.Text += $"\nMr {tempUser.Name}";
                    break;

                case 2:
                    WelcomeText.Text += $"\nMrs {tempUser.Name}";
                    break;
            }
        }

        #endregion

        #region Кнопки для перехода на другие страницы

        private void MyProfBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.Navigate(new ProfilePage(tempUser, tempAccount));
        }

        private void EventsBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.Navigate(new EventPage());
        }

        private void ParticipantsBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void JuryBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion
    }
}
