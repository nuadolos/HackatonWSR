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
using System.Windows.Threading;
using WSR_2021.Utils;

namespace WSR_2021.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        #region Свойства и поля

        private DispatcherTimer timer = new DispatcherTimer();
        public DateTime OneMinuteTimer { get; set; }

        public int CountTry { get; set; } = 3;
        public string CaptchaText { get; set; }
        public int IdNumber
        {
            get
            {
                return Properties.Settings.Default.IdNumber;
            }
            set
            {
                Properties.Settings.Default.IdNumber = value;
            }
        }
        public string Password
        {
            get
            {
                return Properties.Settings.Default.Password;
            }
            set
            {
                Properties.Settings.Default.Password = value;
            }
        }
        public bool SaveAccount
        {
            get
            {
                return Properties.Settings.Default.SaveAccount;
            }
            set
            {
                Properties.Settings.Default.SaveAccount = value;
            }
        }

        #endregion

        #region Конструктор страницы Authorization

        public Authorization()
        {
            InitializeComponent();

            if (SaveAccount)
            {
                AccountCheck.IsChecked = true;
                LogTBox.Text = IdNumber.ToString();
                PasTBox.Text = Password;
            }

            timer.Tick += new EventHandler(TimerTickEvent);
            timer.Interval = new TimeSpan(1000);
        }

        #endregion

        #region Кнопки
        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            var visitingUser = Transition.Context.Account.FirstOrDefault(p => p.NumberId.ToString() == LogTBox.Text || p.Password == PasTBox.Text);

            if (visitingUser != null)
            {
                if (AccountCheck.IsChecked == true)
                {
                    IdNumber = visitingUser.NumberId;
                    Password = visitingUser.Password;
                    SaveAccount = true;

                    Properties.Settings.Default.Save();
                }
                else
                    Properties.Settings.Default.Reset();

                MessageBox.Show($"Добро пожаловать, {visitingUser.Users.Surname} {visitingUser.Users.Name} {visitingUser.Users.Middlename}!");
                //Transition.MainFrame.Navigate();
            }
            else
            {
                CountTry--;
                if (CountTry == 0)
                {
                    LogTBox.IsReadOnly = true;
                    PasTBox.IsReadOnly = true;
                    LogBtn.IsEnabled = false;
                    OneMinuteTimer = DateTime.Now.AddSeconds(60);
                    CountTry = 3;

                    timer.Start();
                    return;
                }

                MessageBox.Show($"Логин или пароль были введенны неправильно!\nОсталось {CountTry} попыток", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region Удаление и восстановление подсказки для пользователя в полях ввода

        private void PasTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PasTBox.Text == "")
            {
                PasTBox.Foreground = Brushes.Gray;
                PasTBox.Text = "Enter your password";
            }
        }

        private void PasTBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasTBox.Foreground = Brushes.Black;
            PasTBox.Text = null;
        }

        private void LogTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            LogTBox.Foreground = Brushes.Black;
            if (LogTBox.Text == "")
            {
                LogTBox.Foreground = Brushes.Gray;
                LogTBox.Text = "Enter your IdNumber";
            }
        }

        private void LogTBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LogTBox.Foreground = Brushes.Black;
            LogTBox.Text = null;
        }

        #endregion

        #region Таймер при исчерпании всех попыток

        private void TimerTickEvent(object sender, EventArgs e)
        {
            if (OneMinuteTimer >= DateTime.Now)
                TimerText.Text = $"Повторите попытку через {(OneMinuteTimer - DateTime.Now).Seconds} секунд!";
            else
            {
                LogTBox.IsReadOnly = false;
                PasTBox.IsReadOnly = false;
                LogBtn.IsEnabled = true;
                TimerText.Text = null;

                timer.Stop();
            }
        }

        #endregion
    }
}
