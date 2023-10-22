﻿using System;
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
	/// Логика взаимодействия для CirclesPage.xaml
	/// </summary>
	public partial class CirclesPage : Page
	{
		public CirclesPage()
		{
			InitializeComponent();
		}

		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			CircleWindow circlesPage = new CircleWindow();
			circlesPage.Show();
        }
    }
}
