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
    /// Логика взаимодействия для RegistrationModerJury.xaml
    /// </summary>
    public partial class RegistrationModerJury : Page
    {
        #region Закрытые поля

        private Users newUser = new Users();
        private Account newAccount = new Account();
        private int pozitionNumPhone = 3;
        private string textdo = "+7(___)___-__-__";

        #endregion

        #region Конструктор страницы RegistrationModerJury

        public RegistrationModerJury()
        {
            InitializeComponent();

            GenderCBox.ItemsSource = Transition.Context.Gender.ToList();
            RoleCBox.ItemsSource = Transition.Context.Role.Where(p => p.Name == "Moderator" || p.Name == "Jury").ToList();
            EventCBox.ItemsSource = Transition.Context.Event.ToList();

            DataContext = newUser;
        }

        #endregion

        #region Смена пароля

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
            if (PasTBox.Text == "Password")
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
            if (RePasTBox.Text == "Re-enter password")
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

                PasTBox.ToolTip = "Пароль не соотвествует требованиям:\n" + error;
                PassPBox.ToolTip = "Пароль не соотвествует требованиям:\n" + error;
            }
            else
            {
                PasTBox.BorderBrush = Brushes.Green;
                PassPBox.BorderBrush = Brushes.Green;

                PasTBox.ToolTip = null;
                PassPBox.ToolTip = null;
            }
        }

        #endregion

        #endregion

        #region Сохранение новых данных о пользователе и его аккаунте

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            try
            {
                string[] FIO = FIOTBox.Text.Split(' ');
                newUser.Surname = FIO[0];
                newUser.Name = FIO[1];
                newUser.Middlename = FIO[2];
            }
            catch (Exception)
            {
                error.AppendLine("Укажите ФИО;");
            }

            if (string.IsNullOrWhiteSpace(newUser.Gender.Name))
                error.AppendLine("Выберите пол;");
            if (string.IsNullOrWhiteSpace(newUser.Role.Name))
                error.AppendLine("Выберите роль;");
            if (string.IsNullOrWhiteSpace(newUser.Email))
                error.AppendLine("Укажите почту;");

            newUser.Phone = PhoneMaskTBox.Text;
            if (string.IsNullOrWhiteSpace(newUser.Phone))
                error.AppendLine("Укажите телефон;");

            if (PasTBox.Text.Length < PassPBox.Password.Length)
                PasTBox.Text = PassPBox.Password;
            if (RePasTBox.Text.Length < RePassPBox.Password.Length)
                RePasTBox.Text = RePassPBox.Password;

            if (PasTBox.Text != RePasTBox.Text)
                error.AppendLine("Пароли не совпадают");

            if (error.Length > 0)
            {
                MessageBox.Show($"При сохранении возникли следующие ошибки:\n{error}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            newAccount.Id = 0;
            newAccount.NumberId = 0;
            newAccount.Password = PasTBox.Text;

            if (newUser.Id == 0)
            {
                Transition.Context.Users.Add(newUser);
                Transition.Context.Account.Add(newAccount);
            }

            try
            {
                Transition.Context.SaveChanges();
                MessageBox.Show("Данные сохранены", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Transition.MainFrame.GoBack();
            }
            catch (Exception er)
            {
                MessageBox.Show($"При сохранении данных произошла ошибка: {er.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Transition.MainFrame.GoBack();
        }

        #endregion

        #region Разрешение на привязку пользователя к мероприятию

        private void EventCheck_Checked(object sender, RoutedEventArgs e)
        {
            EventCBox.IsEnabled = true;
        }

        private void EventCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            EventCBox.IsEnabled = false;
        }

        #endregion

        #region Маска для ввода телефона
        //Реализовать улучшенную маску со всеми исключениями при завершении работы над проектом
        private void PhoneMaskTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string mask = "+7(___)___-__-__";

            if (PhoneMaskTBox.Text == "noth")
                PhoneMaskTBox.Text = mask;

            StringBuilder tempStr = new StringBuilder(PhoneMaskTBox.Text);

            bool aga = false;
            if (textdo.Length > tempStr.Length)
            {
                for (int j = 0; j < tempStr.Length; j++)
                {
                    if (textdo[j] != tempStr[j])
                    {
                        tempStr[j] = '_';
                        aga = true;
                    }
                }

                if (!aga)
                    tempStr.Append("_");
            }

            int tempSymbol = 0;
            for (int j = 0; j < tempStr.Length; j++)
            {

                if (tempStr[j] == '_')
                    tempSymbol++;
            }
            if (tempSymbol == 0)
                PhoneMaskTBox.MaxLength = 16;
            else
                PhoneMaskTBox.MaxLength = 17;

            if (tempStr.Length < mask.Length)
            {
                PhoneMaskTBox.Text = mask;
            }

            try
            {
                if (tempStr.ToString() != mask)
                {
                    if (PhoneMaskTBox.SelectionStart > 4)
                        pozitionNumPhone = PhoneMaskTBox.SelectionStart - 1;
                    if (PhoneMaskTBox.Text[pozitionNumPhone] == ')' || PhoneMaskTBox.Text[pozitionNumPhone] == '-')
                        pozitionNumPhone++;
                    PhoneMaskTBox.Select(pozitionNumPhone, 0);
                }
            }
            catch (Exception)
            {

            }

            if (pozitionNumPhone != tempStr.Length)
                if (char.IsDigit(tempStr[pozitionNumPhone]))
                {
                    if (tempStr[pozitionNumPhone + 1] == '_')
                    {
                        char number = tempStr[pozitionNumPhone];
                        tempStr.Remove(pozitionNumPhone + 1, 1);

                        pozitionNumPhone++;
                    }

                    textdo = tempStr.ToString();
                    PhoneMaskTBox.Text = textdo;
                }
        }

        private void PhoneMaskTBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PhoneMaskTBox.Text == "+7(___)___-__-__")
            {
                PhoneMaskTBox.Select(pozitionNumPhone, 0);
            }
        }

        #endregion
    }
}