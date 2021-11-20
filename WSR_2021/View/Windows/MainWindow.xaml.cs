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
using WSR_2021.View.Pages;
using WSR_2021.Utils;

namespace WSR_2021
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Статическое свойство StartWindow, используемое для закрытие окна вне класса MainWindow
        /// </summary>
        public static MainWindow StartWindow { get; set; }

        #region Конструктор окна MainWindow

        public MainWindow()
        {
            InitializeComponent();

            StartWindow = this;
            OnePage.Navigate(new EventPage());
            Transition.MainFrame = OnePage;
        }

        #endregion

        #region Отображение кнопки "Назад", если возможно сделать переход на предыдущую страницу

        private void OnePage_ContentRendered(object sender, EventArgs e)
        {
            if (Transition.MainFrame.CanGoBack)
                BtnBack.Visibility = Visibility.Visible;
            else
                BtnBack.Visibility = Visibility.Hidden;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.GoBack();
        }

        #endregion

        #region При закрытие окна FrameWindow Frame класса Transition находится под контролем OnePage

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
                Transition.MainFrame = OnePage;
        }

        #endregion
    }
}
