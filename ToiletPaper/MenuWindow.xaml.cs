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
using ToiletPaper.DataBase;

namespace ToiletPaper
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public static ToiletPaperEntities db = new ToiletPaperEntities();
        public List<Product> products = new List<Product>();
        public MenuWindow()
        {
            InitializeComponent();
            RefreshComboBox();
            UpdateTovar();
            RefreshFilter();
        }
        private void CBNumberWrite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageSize = Convert.ToInt32(CBNumberWrite.SelectedItem.ToString());
            RefreshPagination();
        }
        private void BLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void BRight_Click(object sender, RoutedEventArgs e)
        {
            if (users.Count % pageSize == 0)
            {
                if (pageNumber == (users.Count / pageSize) - 1)
                    return;
            }
            else
            {

                if (pageNumber == (users.Count / pageSize))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }

        int pageSize;
        int pageNumber;
        List<Product> users = db.Product.ToList();

        private void RefreshPagination()
        {
            KeyboardList.ItemsSource = null;
            KeyboardList.ItemsSource = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }

        private void RefreshComboBox()
        {
            CBNumberWrite.Items.Add("10");
            SortCB.Items.Add("По умолчанию");
            SortCB.Items.Add("По названию");
            SortCB.Items.Add("По типу подсветки");
        }
        private void RefreshFilter()
        {
            foreach (var item in db.TypeProd)
                FilterCB.Items.Add(item.NameType);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }
        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTovar();
        }
        private void UpdateTovar()
        {
            var currentKeyboard = ToiletPaperEntities.GetContext().Product.ToList();

            currentKeyboard = currentKeyboard.Where(p => p.Name.ToLower().Contains(Poisk.Text.ToLower())).ToList();

            KeyboardList.ItemsSource = currentKeyboard.OrderBy(p => p.Name).ToList();
        }
        private void Mouse_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToiletPaperEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(a => a.Reload());
                KeyboardList.ItemsSource = ToiletPaperEntities.GetContext().Product.ToList();
            }
        }
        private void Red_Click(object sender, RoutedEventArgs e)
        {
            //AddKeyboardPage page = new AddKeyboardPage((sender as Button).DataContext as Model.keyboard);
            //page.Show();
            //this.Close();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //KeyboardList.ItemsSource = null;
            //if (SortCB.SelectedIndex == 0)
            //{
            //    KeyboardList.ItemsSource = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            //}
            //else if (SortCB.SelectedIndex == 1)
            //{
            //    var a = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            //    for (int? j = 0; j < a.Count; j++)
            //    {
            //        for (int i = 0; i < a.Count - 1; i++)
            //        {
            //            if (a[i].CompareTo(a[i + 1]) > 0)
            //            {
            //                var temp = a[i];
            //                a[i] = a[i + 1];
            //                a[i + 1] = temp;
            //            }
            //        }
            //    }
            //    KeyboardList.ItemsSource = a;
            //}
            //else if (SortCB.SelectedIndex == 2)
            //{
            //    var a = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            //    for (int? j = 0; j < a.Count; j++)
            //    {
            //        for (int i = 0; i < a.Count - 1; i++)
            //        {
            //            if (a[i].CompareTo(a[i + 1], 3) > 0)
            //            {
            //                var temp = a[i];
            //                a[i] = a[i + 1];
            //                a[i + 1] = temp;
            //            }
            //        }
            //    }
            //    KeyboardList.ItemsSource = a;
            //}
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            string item = Convert.ToString(combobox.SelectedItem);
            if (item == "Фильтрация")
            {
                KeyboardList.ItemsSource = users;
                return;
            }
            products = db.Product.Where(z => z.TypeProd.NameType == item).ToList();
            KeyboardList.ItemsSource = products;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var MousesForRemoving = KeyboardList.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить сдедующие{MousesForRemoving.Count()} элементов?", "Внимение",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ToiletPaperEntities.GetContext().Product.RemoveRange(MousesForRemoving);
                    ToiletPaperEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    KeyboardList.ItemsSource = ToiletPaperEntities.GetContext().Product.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            //AddKeyboardPage addKeyboardPage = new AddKeyboardPage(null);
            //addKeyboardPage.Show();
            //this.Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainPage = new MainWindow();
            mainPage.Show();
            this.Close();
        }
    }
}
