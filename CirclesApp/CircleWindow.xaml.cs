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

namespace CirclesApp
{
	/// <summary>
	/// Логика взаимодействия для CircleWindow.xaml
	/// </summary>
	public partial class CircleWindow : Window
	{
		public CircleWindow()
		{
			InitializeComponent();
		}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{

        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
			StudentsWindow studentsWindow = new StudentsWindow();
			studentsWindow.Show();
        }
    }
}
