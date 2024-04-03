using Microsoft.EntityFrameworkCore;
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
using static Практика.MainWindow;

namespace Практика
{
    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = Login.Text;
            var password = Password.Password;
            if (password.Length < 6) { MessageBox.Show("Слишком маленький пароль"); return; }
            var context = new AppDbContext();

            var user_exist = context.Users.FirstOrDefault(x => x.Login == login);
            if (user_exist != null)
            {
                MessageBox.Show("Такой пользователь уже есть");
                return;
            }
            var user = new User { Login = login, Password = password };
            context.Users.Add(user);
            context.SaveChanges();
            MessageBox.Show("Welcome to the club, buddy");
           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow Form = new MainWindow();
            Form.Show();
            this.Hide();
        }
    }
}
