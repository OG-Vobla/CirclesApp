using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace CirclesApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            Authorization();

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
            Authorization();

        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
		{
            Authorization();

        }
        private void Authorization()
        {
            if (DbConectionClass.CirclesDBEntities.Users.Where(x=> x.Login == Login.Text).FirstOrDefault() == null)
            {
                MessageBox.Show("Пользователя с таким логином нет.");
            }
            else
            {
                if (DbConectionClass.CirclesDBEntities.Users.Where(x => x.Login == Login.Text && x.Password == Password.Text).FirstOrDefault() == null)
                {
                    MessageBox.Show("Не правильный пароль.");
                }
                else
                {
                    DbConectionClass.user = DbConectionClass.CirclesDBEntities.Users.Where(x => x.Login == Login.Text && x.Password == Password.Text).FirstOrDefault();
                    if (DbConectionClass.user.Employee.Role.Name == "Учитель   ")
                    {
                        TeacherWindow teacherWindow = new TeacherWindow();
                        teacherWindow.Show();
                        this.Close();
                    }
                    else if (DbConectionClass.user.Employee.Role.Name == "Директор  ")
                    {
                        DirectorWindow teacherWindow = new DirectorWindow();
                        teacherWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("У вас нет прав пользоваться приложением.");
                    }
                }
            }

        }
	}
}
