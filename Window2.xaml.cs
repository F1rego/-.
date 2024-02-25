using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Data.SqlClient;

namespace WpfApp20
{

    public partial class Window2 : Window
    {
        private string connectionString = "Data Source = (LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\User\\Desktop\\Новая папка1234\\kyrsovproekt.mdf\";Integrated Security = True;Connect Timeout = 30";

        private SqlConnection connection;
        private string currentUser;
        Entities5 entities = new Entities5();
        private string savePath = "C:\\Users\\User\\Desktop\\Новая папка1234\\WpfApp20\\123.xlsx";

        public Window2(string username)
        {
            currentUser = username;
            InitializeComponent();
            LoadData();

            PopulateComboBoxWithYears();
            textboxsearch.TextChanged += (sender, e) => UpdateData();
            comboboxsearch.SelectionChanged += (sender, e) => UpdateData();

            dataGrid.LoadingRow += dataGrid_LoadingRow;
            dataGrid.UnloadingRow += dataGrid_UnloadingRow;
        }

        private void LoadData()
        {
            var query = from z in entities.Операции
                        select new
                        {
                            Тип_операции = z.Тип_операции,
                            Описание = z.Описание,
                            Дата_операции = z.Дата_операции,
                            Стоимость = z.Стоимость,
                            Оборудование = z.Оборудование,
                            Сотрудники = z.Сотрудники,
                            Поставщики = z.Поставщики,

                        };
            dataGrid.ItemsSource = query.ToList();
        }

        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void button1_Click14(object sender, RoutedEventArgs e)
        {

            string sqlQuery2023 = "SELECT SUM(Стоимость) AS TotalCost2023 FROM Операции WHERE YEAR(Дата_операции) = 2023";


            string sqlQuery2024 = "SELECT SUM(Стоимость) AS TotalCost2024 FROM Операции WHERE YEAR(Дата_операции) = 2024";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();


                using (SqlCommand command2023 = new SqlCommand(sqlQuery2023, connection))
                {
                    object result2023 = command2023.ExecuteScalar();
                    textbox11.Text = $"Потраченные средства за 2023: {result2023 ?? "0"}";
                }


                using (SqlCommand command2024 = new SqlCommand(sqlQuery2024, connection))
                {
                    object result2024 = command2024.ExecuteScalar();
                    textbox22.Text = $"Потраченные средства за 2024: {result2024 ?? "0"}";
                }
            }
        }
        private void UpdateData()
        {
            string operationNameFilter = textboxsearch.Text;
            int selectedYear;
            var isYearSelected = int.TryParse(comboboxsearch.SelectedItem?.ToString(), out selectedYear);

            var query = from z in entities.Операции
                        where (string.IsNullOrEmpty(operationNameFilter) || z.Тип_операции.Contains(operationNameFilter))
                                && (!isYearSelected || z.Дата_операции.Year == selectedYear)
                        select new
                        {
                            Тип_операции = z.Тип_операции,
                            Описание = z.Описание,
                            Дата_операции = z.Дата_операции,
                            Оборудование = z.Оборудование,
                            Сотрудники = z.Сотрудники,
                            Поставщики = z.Поставщики,
                        };

            dataGrid.ItemsSource = query.ToList();
        }

        private void PopulateComboBoxWithYears()
        {
            List<int> years = new List<int> { 2023, 2024 };
            comboboxsearch.ItemsSource = years;
        }

        private void SaveDataToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dataGrid.Columns[i].Header;
                }

                for (int row = 0; row < dataGrid.Items.Count; row++)
                {
                    for (int col = 0; col < dataGrid.Columns.Count; col++)
                    {
                        var cellValue = ((TextBlock)dataGrid.Columns[col].GetCellContent(dataGrid.Items[row])).Text;
                        worksheet.Cells[row + 2, col + 1].Value = cellValue;
                    }
                }

                FileInfo file = new FileInfo(savePath);
                excelPackage.SaveAs(file);

                Process.Start(file.FullName);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveDataToExcel();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            textbox11.Text = "";
            textbox22.Text = "";
            textboxsearch.Text = "";
            comboboxsearch.SelectedItem = null;
            LoadData();
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            UpdateRecordCount();
        }

        private void dataGrid_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            UpdateRecordCount();
        }

        private void UpdateRecordCount()
        {
            int count = dataGrid.Items.Count;
            textboxRecordCount.Text = $"Количество записей: {count}";
        }
    }
}