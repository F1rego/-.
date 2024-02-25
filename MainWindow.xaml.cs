using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
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
using System.Data.Entity.Validation;

namespace WpfApp20
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\User\\Desktop\\Новая папка1234\\kyrsovproekt.mdf\";Integrated Security = True;Connect Timeout = 30";

        private SqlConnection connection;
        private string currentUser;
        Entities5 entities = new Entities5();

        public MainWindow(string username)
        {
            InitializeComponent();


            currentUser = username;
            CheckAccessRights();

            foreach (var Название_операции in entities.Операции)
                listbox1.Items.Add(Название_операции);
            foreach (var Название_отдела in entities.Оборудование)
                combobox1.Items.Add(Название_отдела);
            foreach (var ФИО_сотрудника in entities.Сотрудники)
                combobox2.Items.Add(ФИО_сотрудника);
            foreach (var Название_поставщика in entities.Поставщики)
                combobox3.Items.Add(Название_поставщика);
        }

        private void CheckAccessRights()
        {
            if (currentUser == "admin")
            {
                MessageBox.Show("Добро пожаловать пользователь admin. Вам доступен полный функционал", "Приветствие");
            }
            else if (currentUser == "otchetnik")
            {
                MessageBox.Show("Добро пожаловать пользователь otchetnik. Вам доступен просмотр записей на главном окне(кнопка очистить записи и переход к главное таблице печати)", "Приветствие");
                button1.IsEnabled = false;
                button2.IsEnabled = false;
                button3.IsEnabled = false;
                button4.IsEnabled = false;
               //button11.IsEnabled = false;
                ToggleButton.IsEnabled = false;
                button1.Opacity = 0.5;
                button2.Opacity = 0.5;
                button3.Opacity = 0.5;
                button4.Opacity = 0.5;
                //button11.Opacity = 0.5;
                ToggleButton.Opacity = 0.5;

            }
            else if (currentUser == "rabotnik")
            {
                MessageBox.Show("Добро пожаловать пользователь rabotnik. Вам доступен полный  функционал", "Приветствие");
            }


            if (currentUser == "2")
            {
                button1.IsEnabled = false;
                button2.IsEnabled = false;
                button3.IsEnabled = false;
                button4.IsEnabled = false;
                //button11.IsEnabled = false;
                ToggleButton.IsEnabled = false;
                button1.Opacity = 0.5;
                button2.Opacity = 0.5;
                button3.Opacity = 0.5;
                button4.Opacity = 0.5;
                //button11.Opacity = 0.5;
                ToggleButton.Opacity = 0.5;
            }

            if (currentUser == "1")
            {
                button1.IsEnabled = false;
                button2.IsEnabled = false;
                button3.IsEnabled = false;
                button4.IsEnabled = false;
                //button11.IsEnabled = false;
                ToggleButton.IsEnabled = false;
                button1.Opacity = 0.5;
                button2.Opacity = 0.5;
                button3.Opacity = 0.5;
                button4.Opacity = 0.5;
                //button11.Opacity = 0.5;
                ToggleButton.Opacity = 0.5;

            }
        }


        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_oper = listbox1.SelectedItem as Операции;
            if (selected_oper != null)
            {
                textbox1.Text = selected_oper.Тип_операции;
                textbox6.Text = selected_oper.Дата_операции.ToString();
                textbox3.Text = selected_oper.Количество.ToString();
                textbox4.Text = selected_oper.Стоимость.ToString();
                textbox7.Text = selected_oper.Описание;
                combobox1.SelectedItem = selected_oper.Оборудование;
                combobox2.SelectedItem = selected_oper.Сотрудники;
                combobox3.SelectedItem = selected_oper.Поставщики;
                if (!string.IsNullOrEmpty(selected_oper.photo))
                {
                    Photo.Source = new BitmapImage(new Uri(selected_oper.photo));
                }
                else
                {
                    Photo.Source = null;
                }
            }

            else
            {
                textbox1.Text = "";
                textbox6.Text = "";
                textbox3.Text = "";
                textbox4.Text = "";
                textbox7.Text = "";
                combobox1.SelectedIndex = -1;
                combobox2.SelectedIndex = -1;
                combobox3.SelectedIndex = -1;
                Photo.ClearValue(Image.SourceProperty);
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string username = "";
            var window1 = new Window1(username);
            window1.ShowDialog();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string username = "";
            var window3 = new Window3(username);
            window3.ShowDialog();
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            string username = "";
            var window4 = new Window4(username);
            window4.ShowDialog();
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new LoginWindow();


            foreach (Window window in Application.Current.Windows)
            {
                if (window != newWindow)
                {
                    window.Close();
                }
            }

            newWindow.Show();
        }



        private void button4_Click_1(object sender, RoutedEventArgs e)
        {
            var izm_oper = listbox1.SelectedItem as Операции;

            if (string.IsNullOrEmpty(textbox1.Text) || string.IsNullOrEmpty(textbox6.Text) || combobox1.SelectedIndex == -1)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                izm_oper.Тип_операции = textbox1.Text;
                izm_oper.Описание = textbox7.Text;

                if (!textbox6.SelectedDate.HasValue || textbox6.SelectedDate.Value.Year < 2023)
                {
                    MessageBox.Show("Неверная дата операции. Год должен быть не ниже 2023", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

               izm_oper.Дата_операции = textbox6.SelectedDate.Value.Date;

                if (int.TryParse(textbox3.Text, out int количество))
                {
                    izm_oper.Количество = количество;
                }
                else
                {
                    MessageBox.Show("Поле количество должно содержать числовое значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

               int? стоимость = null;
                if (!string.IsNullOrEmpty(textbox4.Text))
                {
                    if (int.TryParse(textbox4.Text, out int parsedValue))
                    {
                        стоимость = parsedValue;
                    }
                    else
                    {
                        MessageBox.Show("Поле стоимость должно содержать числовое значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
               izm_oper.Стоимость = стоимость;
                izm_oper.Оборудование = combobox1.SelectedItem as Оборудование;
                izm_oper.Сотрудники = combobox2.SelectedItem as Сотрудники;
                izm_oper.Поставщики = combobox3.SelectedItem as Поставщики;

                try
                {
                    entities.SaveChanges();
                    MessageBox.Show("Запись успешно изменена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                    listbox1.Items.Refresh();
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
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var Window2 = new Window2(currentUser);
            Window2.ShowDialog();
        }
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textbox1.Text = "";
            textbox6.Text = "";
            textbox3.Text = "";
            textbox4.Text = "";
            textbox7.Text = "";
            
            listbox1.SelectedIndex = -1;
            textbox1.Focus();
            combobox1.SelectedIndex = -1;
            combobox2.SelectedIndex = -1;
            combobox3.SelectedIndex = -1;
            Photo.ClearValue(Image.SourceProperty);
        }



        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                string rootDirectory = @"C:\Images\";

                string relativePath = imagePath.Replace(rootDirectory, string.Empty);
                var selected_rabotnik = listbox1.SelectedItem as Операции;

                if (selected_rabotnik != null)
                {
                    selected_rabotnik.photo = relativePath;

                    listbox1.Items.Refresh();
                    MessageBox.Show("Фото успешно выбрано! \nДля сохранения нажмите кнопку изменить запись.", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);


                }
            }
        }


        private void textbox7_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (MenuPopup.IsOpen)
            {
                MenuPopup.IsOpen = false;
            }
            else
            {
                MenuPopup.IsOpen = true;
            }
        }


        private void button1_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                var add = listbox1.SelectedItem as Операции;
                if (string.IsNullOrEmpty(textbox1.Text) || string.IsNullOrEmpty(textbox3.Text) || combobox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (add == null)
                        add = new Операции();

                    add.Тип_операции = textbox1.Text;

                    if (!textbox6.SelectedDate.HasValue || textbox6.SelectedDate.Value.Year < 2020)
                    {
                        MessageBox.Show("Неверная дата операции. Год должен быть не ниже 2020", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    add.Дата_операции = textbox6.SelectedDate.Value.Date;

                    if (int.TryParse(textbox3.Text, out int количество))
                    {
                        add.Количество = количество;
                    }
                    else
                    {
                        MessageBox.Show("Поле количество должно содержать числовое значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    int? стоимость = null;
                    if (!string.IsNullOrEmpty(textbox4.Text))
                    {
                        if (int.TryParse(textbox4.Text, out int parsedValue))
                        {
                            стоимость = parsedValue;
                        }
                        else
                        {
                            MessageBox.Show("Поле стоимость должно содержать числовое значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }

                    add.Стоимость = стоимость;
                    add.Описание = textbox7.Text;

                    add.Оборудование = combobox1.SelectedItem as Оборудование;
                    add.Сотрудники = combobox2.SelectedItem as Сотрудники;
                    add.Поставщики = combobox3.SelectedItem as Поставщики;

                    entities.Операции.Add(add);
                    entities.SaveChanges();

                    var операции = entities.Операции.ToList(); 

                    listbox1.ItemsSource = null; 
                    listbox1.Items.Clear(); 

                    foreach (var операция in операции)
                    {
                        listbox1.Items.Add(операция); 
                    }

                    listbox1.Items.Refresh();
                    listbox1.SelectedIndex = -1;

                    MessageBox.Show("Запись успешно добавлена", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Ошибка в поле '{validationError.PropertyName}': {validationError.ErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        
        private void button2_Click_2(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы хотите удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            var delete_oper = listbox1.SelectedItem as Операции;
            if (delete_oper != null)
            {
                entities.Операции.Remove(delete_oper);
                entities.SaveChanges();
                MessageBox.Show("Запись успешно удалена!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
                if (listbox1.ItemsSource == null)
                {
                    listbox1.Items.Clear();
                }
                else
                {
                    listbox1.ItemsSource = null;
                }
                listbox1.ItemsSource = entities.Операции.ToList();
                textbox1.Clear();
                textbox3.Clear();
                textbox4.Clear();
                textbox7.Clear();
                textbox6.SelectedDate = null;
                combobox1.SelectedIndex = -1;
                combobox2.SelectedIndex = -1;
                combobox3.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeletePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            var selected_rabotnik = listbox1.SelectedItem as Операции;
            if (selected_rabotnik != null)
            {
                selected_rabotnik.photo = null; 
                listbox1.Items.Refresh();
                MessageBox.Show("Фото успешно удалено!", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
    }
    
    
    
    
    

