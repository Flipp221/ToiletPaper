using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        OpenFileDialog ofdImage1 = new OpenFileDialog();
        private Product _product = new Product();
        public AddProduct(Product selected)

        {
            InitializeComponent();
            if (selected != null)
             _product = selected;


            DataContext = _product;

            cbTypeProd.ItemsSource = ToiletPaperEntities.GetContext().TypeProd.ToList();
            tbTypeMat.ItemsSource = ToiletPaperEntities.GetContext().Material.ToList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(NameTB.Text))
                errors.AppendLine("укажите название");
            if (string.IsNullOrWhiteSpace(tbCost.Text))
                errors.AppendLine("укажите цену");
            if (string.IsNullOrWhiteSpace(tbCount.Text))
                errors.AppendLine("укажите количество");
            if (_product.TypeProd.Id == null)
                errors.AppendLine("Укажите тип продукта");
            if (_product.Material == null)
                errors.AppendLine("Укажите тип материала");



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_product.Id_Prod == 0)
            {
                _product.Picture = File.ReadAllBytes(ofdImage1.FileName);
                ToiletPaperEntities.GetContext().Product.Add(_product);
            }
            try
            {
                ToiletPaperEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                MenuWindow menu = new MenuWindow();
                menu.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofdImage = new OpenFileDialog();
            ofdImage.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            ofdImage.FilterIndex = 1;
            if (ofdImage.ShowDialog() == true)
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(ofdImage.FileName);
                image.EndInit();
                ofdImage1 = ofdImage;
                img.Source = image;
            }
        }

    }
}
