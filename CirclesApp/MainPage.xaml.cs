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

namespace CirclesApp
{
	/// <summary>
	/// Логика взаимодействия для MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		string weekName;
		public MainPage()
		{
			weekName = "Понедельник";
			InitializeComponent();
		}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			weekName = "Понедельник";
			WeekDay.Content = weekName;
		}

		private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			weekName = "Вторник";
			WeekDay.Content = weekName;
		}

		private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
		{
			weekName = "Среда";
			WeekDay.Content = weekName;
		}

		private void Image_MouseDown_3(object sender, MouseButtonEventArgs e)
		{
			weekName = "Четверг";
			WeekDay.Content = weekName;
		}

		private void Image_MouseDown_4(object sender, MouseButtonEventArgs e)
		{
			weekName = "Пятница";
			WeekDay.Content = weekName;
		}

		private void Image_MouseDown_5(object sender, MouseButtonEventArgs e)
		{
			weekName = "Суббота";
			WeekDay.Content = weekName;
		}
	}
}
