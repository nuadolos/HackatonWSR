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
        private List<Event> ListEvent { get; set; } = Transition.Context.Event.ToList();

        public EventPage()
        {
            InitializeComponent();

            EventGrid.ItemsSource = ListEvent;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var tempData = ListEvent;

            //EventGrid.ItemsSource = tempData.Where(p => p.Direction == (DirectionCBox.SelectedItem as ) || p.DateEvent == (DateEventCBox.SelectedItem as )).ToList();
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameWindow window = new FrameWindow();
            window.Show();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
