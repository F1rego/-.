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

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        private string currentUser;
        Entities5 entities = new Entities5();
        public Window4(string username)
        {
            InitializeComponent();
            currentUser = username;
            listbox1.ItemsSource = entities.Поставщики.ToList();
        }

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedpostav = listbox1.SelectedItem as Поставщики;
            if (selectedpostav != null)
            {
                textbox1.Text = selectedpostav.Название_поставщика_;
                textbox2.Text = selectedpostav.Адрес;
                textbox3.Text = selectedpostav.Телефон;
            }
            else
            {
                textbox1.Text = "";
                textbox2.Text = "";
                textbox3.Text = "";
            }
        }
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Close();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {

            var postav = listbox1.SelectedItem as Поставщики;
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                MessageBox.Show("Вы должны заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

                if (postav == null)
                {
                    postav = new Поставщики();
                    entities.Поставщики.Add(postav);
                }
                postav.Название_поставщика_ = textbox1.Text;
                postav.Адрес = textbox2.Text;
                postav.Телефон = textbox3.Text;



            }
            try
            {
                entities.SaveChanges();
                MessageBox.Show("Запись успешно добавлена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                listbox1.ItemsSource = entities.Поставщики.ToList();
                textbox1.Text = "";
                textbox2.Text = "";
                textbox3.Text = "";
            }

            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var deletepostav = listbox1.SelectedItem as Поставщики;

            if (deletepostav != null)
            {
                try
                {
                    entities.Поставщики.Remove(deletepostav);
                    entities.SaveChanges();
                    MessageBox.Show("Запись успешно удалена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    listbox1.ItemsSource = entities.Поставщики.ToList();
                    textbox1.Text = "";
                    textbox2.Text = "";
                    textbox3.Text = "";

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Запись удалить нельзя!\nСуществуют данные поставщики!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {

            listbox1.SelectedIndex = -1;
            textbox1.Focus();
            textbox1.Text = "";
            textbox2.Text = "";
            textbox3.Text = "";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow(currentUser);


            foreach (Window window in Application.Current.Windows)
            {
                if (window != newWindow)
                {
                    window.Close();
                }
            }

            newWindow.Show();
        }
    }
}
    

