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
    /// Логика взаимодействия для ViewEventPage.xaml
    /// </summary>
    public partial class ViewEventPage : Page
    {
        #region Закрытые поля

        private Event infoEvent = new Event();

        #endregion

        #region Конструктор страницы ViewEventPage

        public ViewEventPage(Event selectedEvent)
        {
            InitializeComponent();

            infoEvent = selectedEvent;

            DataContext = infoEvent;
        }

        #endregion

        #region Кнопки

        private void DateBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TownBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OrganizerBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
