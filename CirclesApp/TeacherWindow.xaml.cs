using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CirclesApp
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        public TeacherWindow()
        {
            InitializeComponent();
            TeacherWindowFrame.NavigationService.Navigate(new MainPage());
        }

		private void TeacherWindowFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{

		}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			TeacherWindowFrame.NavigationService.Navigate(new MainPage());

		}

		private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
		{
			TeacherWindowFrame.NavigationService.Navigate(new CirclesPage());

		}
	}
}
