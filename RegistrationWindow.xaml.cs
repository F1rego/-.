using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Net;
using System.Net.Mail;



namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\User\\Desktop\\Новая папка1234\\kyrsovproekt.mdf\";Integrated Security = True;Connect Timeout = 30";
        

        public RegistrationWindow() 
        {
            InitializeComponent(); 
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
        private async void txtEmail1_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEmail1.Focusable = false;
            await Task.Delay(50); 
            Dispatcher.Invoke(() => textlogin.Focus());
            txtEmail1.Focusable = true;
        }
        private async void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewUserToDatabase(string login, string password, string email)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Login, Password, Email) VALUES (@login, @password, @email)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@email", email);
                    command.ExecuteNonQuery();
                }
            }
        }
     
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            this.Close();
            window.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string newLogin = txtEmail.Text;
            string newPassword = passwordBox.Password;
            string newEmail = txtEmail1.Text;
            AddNewUserToDatabase(newLogin, newPassword, newEmail);
            try
            {
                MessageBox.Show("Успешная регистрация!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки сообщения: {ex.Message}");
            }
        }
    
 
     
    }
}
