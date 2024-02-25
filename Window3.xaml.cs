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
    public partial class Window3 : Window
    {
        private string currentUser;
        Entities5 entities = new Entities5();
        public Window3(string username)
        {
            InitializeComponent();
            currentUser = username;
            listbox1.ItemsSource = entities.Сотрудники.ToList();
        }

   

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedsotrydniki = listbox1.SelectedItem as Сотрудники;
            if (selectedsotrydniki != null)
            {
                textbox1.Text = selectedsotrydniki.ФИО;
                textbox2.Text = selectedsotrydniki.Должность_;




            }
            else
            {
                textbox1.Text = "";
                textbox2.Text = "";
            }
        }
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var rabotnik = listbox1.SelectedItem as Сотрудники;
            if (string.IsNullOrEmpty(textbox1.Text))
            {
                MessageBox.Show("Вы должны заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

                if (rabotnik == null)
                {
                    rabotnik = new Сотрудники();
                    entities.Сотрудники.Add(rabotnik);
                }
                rabotnik.ФИО = textbox1.Text;
                rabotnik.Должность_ = textbox2.Text;

            }
            try
            {
                entities.SaveChanges();
                MessageBox.Show("Запись успешно добавлена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                listbox1.ItemsSource = entities.Сотрудники.ToList();
                textbox1.Text = "";
                textbox2.Text = "";
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var deleterabotnik = listbox1.SelectedItem as Сотрудники;

            if (deleterabotnik != null)
            {
                try
                {
                    entities.Сотрудники.Remove(deleterabotnik);
                    entities.SaveChanges();
                    MessageBox.Show("Запись успешно удалена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    listbox1.ItemsSource = entities.Сотрудники.ToList();
                    textbox1.Text = "";
                    textbox2.Text = "";

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Запись удалить нельзя!\nСуществуют данные работники!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            listbox1.SelectedIndex = -1;
            textbox1.Focus();
            textbox1.Text = "";
            textbox2.Text = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
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
    

