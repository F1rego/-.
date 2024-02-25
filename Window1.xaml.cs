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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string currentUser;
        Entities5 entities = new Entities5();
        public Window1(string username)
        {
            InitializeComponent();
            currentUser = username;
            listbox1.ItemsSource = entities.Оборудование.ToList();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedoborydovania = listbox1.SelectedItem as Оборудование;
            if (selectedoborydovania != null)
            {
                textbox1.Text = selectedoborydovania.Название_;
                textbox2.Text = selectedoborydovania.Модель;
                textbox3.Text = selectedoborydovania.Серийный_номер;
                textbox4.Text = selectedoborydovania.Статус;
                textbox5.Text = Convert.ToString(selectedoborydovania.Дата_покупки);
                textbox6.Text = selectedoborydovania.Срок_службы;




            }
            else
            {
                textbox1.Text = "";
                textbox2.Text = "";
                textbox3.Text = "";
                textbox4.Text = "";
                textbox5.Text = "";
                textbox6.Text = "";
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var oborydovanie = listbox1.SelectedItem as Оборудование;
            if (string.IsNullOrEmpty(textbox1.Text) || string.IsNullOrEmpty(textbox5.Text) || textbox5.SelectedDate == null || string.IsNullOrEmpty(textbox6.Text))
            {
                MessageBox.Show("Вы должны заполнить все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                
            }
            else
            {

                if (oborydovanie == null)
                {
                    oborydovanie = new Оборудование();
                    entities.Оборудование.Add(oborydovanie);
                }

                oborydovanie.Название_ = textbox1.Text;
                oborydovanie.Модель = textbox2.Text;
                oborydovanie.Серийный_номер = textbox3.Text;
                oborydovanie.Статус = textbox4.Text;
                oborydovanie.Срок_службы = textbox6.Text;

                DateTime selectedDate = textbox5.SelectedDate.Value;
                oborydovanie.Дата_покупки = new DateTime(2024, selectedDate.Month, selectedDate.Day);
               
             
              

            }
            try
            {
                entities.SaveChanges();
                MessageBox.Show("Запись успешно добавлена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                listbox1.ItemsSource = entities.Оборудование.ToList();
                textbox1.Text = "";
                textbox2.Text = "";
                textbox3.Text = "";
                textbox4.Text = "";
                textbox5.Text = "";
                textbox6.Text = "";
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var deleteoborydovanie = listbox1.SelectedItem as Оборудование;

            if (deleteoborydovanie != null)
            {
                try
                {
                    entities.Оборудование.Remove(deleteoborydovanie);
                    entities.SaveChanges();
                    MessageBox.Show("Запись успешно удалена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    listbox1.ItemsSource = entities.Оборудование.ToList();
                    textbox1.Text = "";
                    textbox2.Text = "";
                    textbox3.Text = "";
                    textbox4.Text = "";
                    textbox5.Text = "";
                    textbox6.Text = "";

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Запись удалить нельзя!\nСуществуют данные оборудования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Close();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            listbox1.SelectedIndex = -1;
            textbox1.Focus();
            textbox1.Text = "";
            textbox2.Text = "";
            textbox3.Text = "";
            textbox4.Text = "";
            textbox5.Text = "";
            textbox6.Text = "";
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
    

