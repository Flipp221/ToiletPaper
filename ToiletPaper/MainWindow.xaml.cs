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
using ToiletPaper.DataBase;

namespace ToiletPaper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ToiletPaperEntities db = new ToiletPaperEntities();
        public static Number_User number;
        public static Roll roll;
        public static Visibility isAdmin = Visibility.Visible;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in db.Number_User)
            {
                if (item.Password == tbPassword.Password.Trim() && item.id_user == 1) 
                {
                    if (item.Login == tbLogin.Text.Trim() && item.id_user == 1)
                    {
                        MessageBox.Show($"Привет Админ - {item.User.Name}");
                        MainWindow.number = item;
                    }
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();
                    Close();
                }
                if (number == null)
                {
                    MessageBox.Show("Неправильный логин или пароль!");
                }
            }
        }
    }
}
