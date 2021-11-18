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

namespace WSR_2021.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для FrameWindow.xaml
    /// </summary>
    public partial class FrameWindow : Window
    {
        public FrameWindow()
        {
            InitializeComponent();

            MainFrame.Navigate(new Authorization());
            Transition.MainFrame = MainFrame;
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (Transition.MainFrame.CanGoBack && !Authorization.NavigateToWindow)
                BtnBack.Visibility = Visibility.Visible;
            else
                BtnBack.Visibility = Visibility.Hidden;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.GoBack();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Authorization.NavigateToWindow = false;
            while (Transition.MainFrame.CanGoBack)
                Transition.MainFrame.GoBack();
        }
    }
}
