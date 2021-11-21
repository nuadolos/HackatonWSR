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
using System.Windows.Shapes;
using WSR_2021.Utils;
using WSR_2021.View.Pages;
using WSR_2021.View.Windows;

namespace WSR_2021.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для FrameWindow.xaml
    /// </summary>
    public partial class FrameWindow : Window
    {
        /// <summary>
        /// Статическое свойство MainWin, используемое для закрытие окна вне класса FrameWindow
        /// </summary>
        public static FrameWindow MainWin { get; set; }

        #region Конструктор окна FrameWindow

        public FrameWindow()
        {
            InitializeComponent();

            MainWin = this;

            MainFrame.Navigate(new Authorization());
            Transition.MainFrame = MainFrame;
        }

        #endregion

        #region Отображение кнопок, если возможно сделать переход на предыдущую страницу

        //Кнопка BtnBack работает иначе. Она отображается при соблюдении двух условий:
        //Переход на предыдущую страницу и отображаемая странице не является OrganizerPage

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Transition.MainFrame.CanGoBack && !Transition.MainFrame.Content.ToString().Contains("OrganizerPage"))
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
                BtnBack.Visibility = Visibility.Hidden;

            if (Transition.MainFrame.CanGoBack)
                BtnLogout.Visibility = Visibility.Visible;
            else
                BtnLogout.Visibility = Visibility.Hidden;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.GoBack();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            while (Transition.MainFrame.CanGoBack)
                Transition.MainFrame.GoBack();
        }

        #endregion

        #region При нажатии на крестик пользователь возвращается на начальное окно

        private void MainWind_Closed(object sender, EventArgs e)
        {
            MainWindow.StartWindow.Visibility = Visibility.Visible;
        }

        #endregion
    }
}
