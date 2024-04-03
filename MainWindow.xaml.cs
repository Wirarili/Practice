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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Практика
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateTransport()
        {
            var userId = 1;
            var context = new AppDbContext();
            var user = context.Users.Find(userId);

            var transport = new Transport
            {
                Owner = user,
                Identifier = "F00BB"
            };
            context.Transports.Add(transport);
            context.SaveChanges();
        }

        public class Transport
        {
            public int Id { get; set; }
            public User Owner { get; set; }
            public int OwnerId { get; set; }
            public string Identifier { get; set; }
        }


        private void UpdateUser()
        {
            var userId = 1;
            var context = new AppDbContext();
            var user = context.Users.Find(userId);
            user.Login = "admin1";
            context.SaveChanges();
        }
        private void DeleteUser()
        {
            var userId = 1;
            var context = new AppDbContext();
            var user = context.Users.Find(userId);

            context.Users.Remove(user);
            context.SaveChanges();
        }

        

        public class User
        {
            public int ID { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public List<Transport> Transports { get; set; }
        }

        public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=UserDb1;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
            public DbSet<Transport> Transports { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = Login.Text;
            var password = Password.Password;

            var context = new AppDbContext();
            var user = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password || x.Email == login && x.Password == password); 
            if (user == null)
            {
                LoginError.Text = "Неверный логин или пароль";
                return;
            }
            Window2 window2 = new Window2();
            window2.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 newForm = new Window1();
            newForm.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
