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
    /// Логика взаимодействия для ViewModerJury.xaml
    /// </summary>
    public partial class ViewModerJury : Page
    {
        #region Конструктор страницы ViewModerJury

        public ViewModerJury()
        {
            InitializeComponent();

            var allSurnames = Transition.Context.Users.Where(p => p.Role.Name == "Moderator" || p.Role.Name == "Jury").ToList();
            allSurnames.Insert(0, new Users { Surname = "Все фамилии" });
            SurnameCBox.ItemsSource = allSurnames;
            SurnameCBox.SelectedIndex = 0;

            var allEvents = Transition.Context.Event.ToList();
            allEvents.Insert(0, new Event { Title = "Все мероприятия" });
            EventCBox.ItemsSource = allEvents;
            EventCBox.SelectedIndex = 0;

            ModerJuryGrid.ItemsSource = Transition.Context.Users.Where(p => p.Role.Name == "Moderator" || p.Role.Name == "Jury").ToList();
        }

        #endregion

        #region Сортировка и фильтрация ModerJuryGrid

        private void UpdateModerJuryGrid()
        {
            var tempData = Transition.Context.Users.Where(p => p.Role.Name == "Moderator" || p.Role.Name == "Jury").ToList();

            if (EventCBox.SelectedIndex > 0)
            {
                int idEvent = (EventCBox.SelectedItem as Event).Id;
                var tempEvent = Transition.Context.EventActivity.Where(p => p.EventId == idEvent).ToList();

                tempData.Clear();
                for (int i = 0; i < tempEvent.Count; i++)
                {
                    int idJury = tempEvent[i].Activity.JuryId;
                    var tempUser = Transition.Context.Users.FirstOrDefault(p => p.Id == idJury);
                    tempData.Add(tempUser);
                }
            }

            if (SurnameCBox.SelectedIndex > 0)
                tempData = tempData.Where(p => p.Surname == (SurnameCBox.SelectedItem as Users).Surname).ToList();

            ModerJuryGrid.ItemsSource = tempData.GroupBy(p => p.Id).ToList();
        }

        private void SurnameCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateModerJuryGrid();
        }

        private void EventCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateModerJuryGrid();
        }

        #endregion

        #region Переход на страницу регистрации пользователя RegistrationModerJury

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.Navigate(new RegistrationModerJury());
        }

        #endregion

        #region Обновление данных в ModerJuryGrid при регистрации пользователя

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Transition.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                UpdateModerJuryGrid();
            }
        }

        #endregion
    }
}
