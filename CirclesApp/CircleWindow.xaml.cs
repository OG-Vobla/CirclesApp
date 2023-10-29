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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CirclesApp
{
	/// <summary>
	/// Логика взаимодействия для CircleWindow.xaml
	/// </summary>
	public partial class CircleWindow : Window
	{
		string circleName;
		public CircleWindow(string CircleName)
		{
			circleName= CircleName;	
			InitializeComponent();
            CircleNameLabel.Content = circleName;
            ShowCircles();
        }


        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid circleGrid = FindParentGrid(sender as DependencyObject);

            if (circleGrid != null)
            {
                Label label = FindChildLabel(circleGrid);
                DbConectionClass.CirclesDBEntities.Student_coterie.Remove
                    (DbConectionClass.CirclesDBEntities.Student_coterie.Where(x=> 
                    (x.Student.Name + " " + x.Student.MiddleName + " " + x.Student.Surname) == label.Content && x.Coterie.Name == circleName).FirstOrDefault());
                DbConectionClass.CirclesDBEntities.SaveChanges();

                while (CirclesStack.Children.Count != 1)
                {
                    CirclesStack.Children.RemoveAt(1);
                }
                ShowCircles();
            }

        }
        private Grid FindParentGrid(DependencyObject child)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);

            while (parent != null && !(parent is Grid))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as Grid;
        }
        private Label FindChildLabel(DependencyObject parent)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is Label)
                {
                    return child as Label;
                }

                Label label = FindChildLabel(child);

                if (label != null)
                {
                    return label;
                }
            }

            return null;
        }
        private void ShowCircles()
        {
            var employesCircles = DbConectionClass.CirclesDBEntities.Student_coterie.Where(x => x.Coterie.Name == circleName).ToList();
            if (employesCircles.Count() != 0)
            {
                foreach (var i in employesCircles)
                {
                    Grid clonedCanvas = new Grid();
                    clonedCanvas.Margin = CircleGrid.Margin;
                    clonedCanvas.Width = CircleGrid.Width;
                    clonedCanvas.Height = CircleGrid.Height;
                    foreach (UIElement element in CircleGrid.Children)
                    {
                        UIElement clonedElement = CloneElement(element, i.Student.Name + " " + i.Student.MiddleName + " "+ i.Student.Surname);
                        clonedCanvas.Children.Add(clonedElement);
                    }
                    CirclesStack.Children.Add(clonedCanvas);
                }
            }
        }
        private UIElement CloneElement(UIElement element, string CircleName)
        {
            if (element is Button)
            {
                Button originalButton = (Button)element;
                Button clonedButton = new Button();

                clonedButton.Content = originalButton.Content;
                clonedButton.Width = originalButton.Width;
                clonedButton.Margin = originalButton.Margin;
                clonedButton.Height = originalButton.Height;
                return clonedButton;
            }
            else if (element is TextBlock)
            {
                TextBlock originalTextBlock = (TextBlock)element;
                TextBlock clonedTextBlock = new TextBlock();

                clonedTextBlock.Text = originalTextBlock.Text;
                clonedTextBlock.Width = originalTextBlock.Width;
                clonedTextBlock.Margin = originalTextBlock.Margin;
                clonedTextBlock.Height = originalTextBlock.Height;
                return clonedTextBlock;
            }
            else if (element is Image)
            {
                Image originalTextBlock = (Image)element;
                Image clonedTextBlock = new Image();
                clonedTextBlock.Source = originalTextBlock.Source;
                clonedTextBlock.Width = originalTextBlock.Width;
                clonedTextBlock.Height = originalTextBlock.Height;
                clonedTextBlock.Margin = originalTextBlock.Margin;
                clonedTextBlock.Stretch = originalTextBlock.Stretch;
                var str = originalTextBlock.Source.ToString();
                
                if (originalTextBlock.Source.ToString() == "pack://application:,,,/fdghfghf.png")
                {
                    clonedTextBlock.MouseDown += Image_MouseDown;
                }

                return clonedTextBlock;
            }
            else if (element is Label)
            {
                Label originalTextBlock = (Label)element;
                Label clonedTextBlock = new Label();

                clonedTextBlock.Content = CircleName;
                clonedTextBlock.Width = originalTextBlock.Width;
                clonedTextBlock.Height = originalTextBlock.Height;
                clonedTextBlock.Margin = originalTextBlock.Margin;
                clonedTextBlock.FontWeight = originalTextBlock.FontWeight;
                clonedTextBlock.FontSize = originalTextBlock.FontSize;
                clonedTextBlock.Foreground = originalTextBlock.Foreground;
                return clonedTextBlock;
            }
            return null;
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
			StudentsWindow studentsWindow = new StudentsWindow(circleName);
			studentsWindow.ShowDialog();
            while (CirclesStack.Children.Count != 1)
            {
                CirclesStack.Children.RemoveAt(1);
            }
            ShowCircles();
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
