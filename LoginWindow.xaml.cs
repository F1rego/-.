using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp20
{
    public partial class LoginWindow : Window
    {
        private string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\User\\Desktop\\Новая папка1234\\kyrsovproekt.mdf\";Integrated Security = True;Connect Timeout = 30";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private bool IsLoginValidFromDatabase(string login, string password, string email)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(1) FROM Users WHERE Login = @login AND Password = @password AND Email = @email";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void textlogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textlogin.Visibility = Visibility.Collapsed;
            txtEmail.Focus();
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string passwordText = textPassword.Text;
            textPassword.Visibility = Visibility.Collapsed;
            passwordBox.Focus();
        }

        private void textlogin1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textlogin1.Visibility = Visibility.Collapsed;
            textlogin.Focus();
        }
         private void CloseApplication(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = txtEmail.Text;
            string password = passwordBox.Password;
            string email = txtEmail1.Text;

            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Неверно введен email-адрес!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsLoginValidFromDatabase(login, password, email))
            {
                MainWindow window1 = new MainWindow(login);
                this.Close();
                window1.Show();

                MessageBox.Show("Добро пожаловать!", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrationWindow window = new RegistrationWindow();
            this.Close();
            window.Show();
        }
    }
}
