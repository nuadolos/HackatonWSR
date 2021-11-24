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
        #region Закрытые поля

        private List<Users> listModerJury = Transition.Context.Users.Where(p => p.Role.Name == "Moderator" || p.Role.Name == "Jury").ToList();

        #endregion

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

            ModerJuryGrid.ItemsSource = listModerJury;
        }

        #endregion

        #region Сортировка и фильтрация ModerJuryGrid

        private void UpdateModerJuryGrid()
        {
            var tempData = listModerJury;

            if (SurnameCBox.SelectedIndex > 0)
                tempData = tempData.Where(p => p.Surname == (SurnameCBox.SelectedItem as Users).Surname).ToList();

            if (EventCBox.SelectedIndex > 0)
            {
                int bb = (EventCBox.SelectedItem as Event).Id;
                var tempEvent = Transition.Context.EventActivity.Where(p => p.EventId == bb).ToList();

                foreach (var item in tempEvent)
                {
                    tempData = tempData.Where(p => p.Id == item.Activity.JuryId).ToList();
                }
            }

            ModerJuryGrid.ItemsSource = tempData;
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

        #region Переход на страницу

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
