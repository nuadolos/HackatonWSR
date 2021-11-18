using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static bool NavigateToWindow { get; set; }

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

                LogTBox.Foreground = System.Windows.Media.Brushes.Black;
                LogTBox.Text = IdNumber.ToString();

                PasTBox.Foreground = System.Windows.Media.Brushes.Black;
                PasTBox.Text = Password;
            }

            timer.Tick += new EventHandler(TimerTickEvent);
            timer.Interval = new TimeSpan(1000);

            CaptchaImage.Source = Captcha((int)CaptchaImage.Width, (int)CaptchaImage.Height);
        }

        #endregion

        #region Кнопка авторизации и отмены
        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            var visitingUser = Transition.Context.Account.FirstOrDefault(p => p.NumberId.ToString() == LogTBox.Text || p.Password == PasTBox.Text);

            if (visitingUser != null )
            {
                if (CaptchaText.ToLower() == CaptchaTBox.Text.ToLower())
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

                    NavigateToWindow = true;
                    Transition.MainFrame.Navigate(new OrganizerPage());
                }
                else
                    MessageBox.Show($"Неверно введен текст из картинки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

                MessageBox.Show($"Логин или пароль были введенны неправильно!\n\tОсталось {CountTry} попыток!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                PasTBox.Foreground = System.Windows.Media.Brushes.Gray;
                PasTBox.Text = "Enter your password";
            }
        }

        private void PasTBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasTBox.Foreground = System.Windows.Media.Brushes.Black;
            PasTBox.Text = null;
        }

        private void LogTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            LogTBox.Foreground = System.Windows.Media.Brushes.Black;
            if (LogTBox.Text == "")
            {
                LogTBox.Foreground = System.Windows.Media.Brushes.Gray;
                LogTBox.Text = "Enter your IdNumber";
            }
        }

        private void LogTBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LogTBox.Foreground = System.Windows.Media.Brushes.Black;
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

        #region Создание Captcha

        private ImageSource Captcha(int width, int height)
        {
            Random rnd = new Random();

            Bitmap bitMap = new Bitmap(width, height);
            Graphics grap = Graphics.FromImage(bitMap);
            grap.Clear(System.Drawing.Color.White);

            string symbols = "1234567890qwERtYuIOpASdFGhJKLZxCVBNm";
            for (int i = 0; i < 5; i++)
                CaptchaText += symbols[rnd.Next(symbols.Length)];

            grap.DrawString(CaptchaText, new Font("Segoe UI", 36), System.Drawing.Brushes.Black, new PointF(0, -4));
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (rnd.Next() % 400 == 0)
                        grap.DrawLine(Pens.White, new PointF(i, j), new PointF(rnd.Next(i + 5), rnd.Next(j + 5)));
                    //if (rnd.Next(width) % 800 == 0)
                    //    grap.DrawArc(Pens.Gray, (float)i, (float)j, width/2, height/2, 60, 90);
                    if (rnd.Next(height) % 10 == 0)
                        bitMap.SetPixel(i, j, System.Drawing.Color.Gray);
                }
            }

            var SourcePicture = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bitMap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            return SourcePicture;
        }

        #endregion
    }
}
