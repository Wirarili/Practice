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
            var email = Mail.Text;
            var login = Login.Text;
            var password = Password.Password;
            var cpassword = CPassword.Password;
            var context = new AppDbContext();

            var user_exist = context.Users.FirstOrDefault(x => x.Login == login); 
            if (user_exist != null) {ErrorName.Text = "Такой пользователь уже есть"; BEN.BorderBrush = Brushes.Red; return; } // Проверка имени
            var dog = '@';
            bool serch = email.Contains(dog);
            if (email == null || serch == false) { ErrorEmail.Text = "Неправельный Email"; BEE.BorderBrush = Brushes.Red; return; }//проверка на собаку
            else { BEE.BorderBrush = Brushes.AntiqueWhite; ErrorEmail.Text = ""; }
            if (password.Length < 6) { ErrorPassword.Text = "Слишком маленький пароль"; BEP.BorderBrush = Brushes.Red; return; }//проверка на длину пароля
            else { BEP.BorderBrush = Brushes.AntiqueWhite; ErrorPassword.Text = ""; }
            char[] chars = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+' };
            int sum = 0;
            for (int i = 0; i < chars.Length; i++) { if (password.Contains(chars[i]) == true) { sum++; } }
            if (sum < 1) { ErrorPassword.Text = "Нужен специальный символ"; BEP.BorderBrush = Brushes.Red; return; }//проверка на спец символ
            else { BEP.BorderBrush = Brushes.AntiqueWhite; ErrorPassword.Text = ""; }
            
            if (password != cpassword) { ErrorCPassword.Text = "Пароли не совпадают"; BECP.BorderBrush = Brushes.Red; return; }//проверка на подтверждение пароля
            else { BECP.BorderBrush = Brushes.AntiqueWhite; ErrorCPassword.Text = ""; }
            var user = new User { Login = login, Password = password, Email = email };
            context.Users.Add(user);
            context.SaveChanges();
            MainWindow Form = new MainWindow();
            Form.Show();
            this.Close();


        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow Form = new MainWindow();
            Form.Show();
            this.Close();
        }
    }
}
