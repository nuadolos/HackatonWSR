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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        #region Закрытые поля

        private Users profUser = new Users();
        private Account profAccount = new Account();

        #endregion

        #region Конструктор страницы ProfilePage

        public ProfilePage(Users infoUser, Account infoAccount)
        {
            InitializeComponent();

            profUser = infoUser;
            profAccount = infoAccount;

            DataContext = profUser;
        }

        #endregion

        #region Смена пароля

        #region События ChangeCheck, запрещающие контакт с полями ввода пароля и наоборот

        private void ChangeCheck_Checked(object sender, RoutedEventArgs e)
        {
            PasTBox.IsEnabled = true;
            PassCheck.IsEnabled = true;
        }

        private void ChangeCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            PasTBox.IsEnabled = false;
            RePasTBox.IsEnabled = false;
            PassCheck.IsEnabled = false;

            PassPBox.Password = null;
            RePassPBox.Password = null;
            PasTBox.Visibility = Visibility.Visible;
            RePasTBox.Visibility = Visibility.Visible;
            PasTBox.Foreground = Brushes.Gray;
            RePasTBox.Foreground = Brushes.Gray;
            PasTBox.Text = "Password";
            RePasTBox.Text = "Re-enter password";
        }

        #endregion

        #region Отображение зашифрованного пароля и наоборот

        private void PassCheck_Checked(object sender, RoutedEventArgs e)
        {
            if (PasTBox.Text != "Password")
            {
                PasTBox.Text = PassPBox.Password;
                RePasTBox.Text = RePassPBox.Password;

                PasTBox.Visibility = Visibility.Visible;
                RePasTBox.Visibility = Visibility.Visible;
            }
        }

        private void PassCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            if (PasTBox.Text != "Password")
            {
                PassPBox.Password = PasTBox.Text;
                RePassPBox.Password = RePasTBox.Text;

                PasTBox.Visibility = Visibility.Hidden;
                RePasTBox.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Изменение свойств полей ввода при изменении фокуса
        private void PasTBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PassCheck.IsChecked == false)
            {
                PasTBox.Visibility = Visibility.Hidden;
                PassPBox.Focus();
            }
            if (PasTBox.Text == "Password" || PasTBox.Text == null)
            {
                PasTBox.FontStyle = FontStyles.Normal;
                PasTBox.Foreground = Brushes.Black;
                PasTBox.Text = null;
            }
        }

        private void PassPBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PassPBox.Password == "")
            {
                PasTBox.Visibility = Visibility.Visible;
                PasTBox.Text = "Password";
                PasTBox.FontStyle = FontStyles.Italic;
                PasTBox.Foreground = Brushes.Gray;
            }
        }

        private void RePasTBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PassCheck.IsChecked == false)
            {
                RePasTBox.Visibility = Visibility.Hidden;
                RePassPBox.Focus();
            }
            if (RePasTBox.Text == "Re-enter password" || RePasTBox.Text == null)
            {
                RePasTBox.Text = null;
                RePasTBox.FontStyle = FontStyles.Normal;
                RePasTBox.Foreground = Brushes.Black;
            }
        }

        private void RePassPBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (RePassPBox.Password == "")
            {
                RePasTBox.Visibility = Visibility.Visible;
                RePasTBox.Text = "Re-enter password";
                RePasTBox.FontStyle = FontStyles.Italic;
                RePasTBox.Foreground = Brushes.Gray;
            }
        }

        #endregion

        #region Кнопки сохранения пароля и перехода на предыдущую страницу

        private void SavePasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PasTBox.Text.Length < PassPBox.Password.Length)
                PasTBox.Text = PassPBox.Password;
            if (RePasTBox.Text.Length < RePassPBox.Password.Length)
                RePasTBox.Text = RePassPBox.Password;

            if (PasTBox.Text == RePasTBox.Text)
            {
                profAccount.Password = PasTBox.Text;
                Transition.Context.SaveChanges();
                MessageBox.Show("Пароль изменен", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.GoBack();
        }

        #endregion

        #region Мониторинг ошибок ввода пароля с помощью метода PassChanged

        private void PasTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasTBox.Text != null && PasTBox.Text != "Password")
                PassChanged(PasTBox.Text);
        }

        private void PassPBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PassPBox.Password != null && PassPBox.Password != "Password")
                PassChanged(PassPBox.Password);
        }

        private void PassChanged(string password)
        {
            StringBuilder error = new StringBuilder();

            if (password.Length < 6)
                error.AppendLine("Не меньше 6 символов");

            int tempUpper = 0, tempSymbol = 0, tempDigit = 0;

            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsUpper(password[i]))
                    tempUpper++;
                if (char.IsPunctuation(password[i]))
                    tempSymbol++;
                if (char.IsDigit(password[i]))
                    tempDigit++;
            }

            if (tempUpper == password.Length || tempUpper == 0)
                error.AppendLine("Заглавные и строчные буквы");
            if (tempSymbol == 0)
                error.AppendLine("Не менее одного спецсимвола");
            if (tempDigit == 0)
                error.AppendLine("Не менее одной цифры");

            if (error.Length > 0)
            {
                PasTBox.BorderBrush = Brushes.Red;
                PassPBox.BorderBrush = Brushes.Red;
                PasTBox.BorderThickness = new Thickness(2);
                PassPBox.BorderThickness = new Thickness(2);

                PasTBox.ToolTip = "Пароль не соотвествует требованиям:\n" + error;
                PassPBox.ToolTip = "Пароль не соотвествует требованиям:\n" + error;

                RePasTBox.IsEnabled = false;
                RePassPBox.IsEnabled = false;
            }
            else
            {
                PasTBox.BorderBrush = Brushes.GreenYellow;
                PassPBox.BorderBrush = Brushes.GreenYellow;

                PasTBox.ToolTip = null;
                PassPBox.ToolTip = null;

                RePasTBox.IsEnabled = true;
                RePassPBox.IsEnabled = true;
            }
        }

        #endregion

        #endregion
    }
}
