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
    /// Логика взаимодействия для StudentsWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        string circleName;
        public StudentsWindow(string CircleName)
        {
            circleName= CircleName;
            InitializeComponent();
            ShowCircles();

        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
          
            Grid circleGrid = FindParentGrid(sender as DependencyObject);

            if (circleGrid != null)
            {
                Label label = FindChildLabel(circleGrid);
                DbConectionClass.CirclesDBEntities.Student_coterie.Add(new Student_coterie() {Id_student = DbConectionClass.CirclesDBEntities.Student.Where(i => (i.Name + " " + i.MiddleName + " " + i.Surname) == label.Content).FirstOrDefault().Id_student, id_coterie = DbConectionClass.CirclesDBEntities.Coterie.Where(i => i.Name == circleName).FirstOrDefault().Id_Coterie });
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
            while (CirclesStack.Children.Count != 1)
            {
                CirclesStack.Children.RemoveAt(1);
            }
            if (SearchText.Text != "" && SearchText.Text != "Поиск")
            {
                var employesCircles = DbConectionClass.CirclesDBEntities.Student.Where(x => (x.Name + " " + x.MiddleName + " " + x.Surname).Contains(SearchText.Text.ToString()) && DbConectionClass.CirclesDBEntities.Student_coterie.Where(u => u.Coterie.Name == circleName && u.Id_student == x.Id_student).FirstOrDefault() == null).ToList();

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
                            UIElement clonedElement = CloneElement(element, i.Name + " " + i.MiddleName + " " + i.Surname);
                            clonedCanvas.Children.Add(clonedElement);
                        }
                        CirclesStack.Children.Add(clonedCanvas);
                    }
                }
            }
            else
            {
                var employesCircles = DbConectionClass.CirclesDBEntities.Student.Where(x => DbConectionClass.CirclesDBEntities.Student_coterie.Where(u => u.Coterie.Name == circleName && u.Id_student == x.Id_student).FirstOrDefault() == null).ToList();
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
                            double t = 0;
                            var a = DbConectionClass.CirclesDBEntities.Student_Check.Where(x => x.Id_student_coterie == DbConectionClass.CirclesDBEntities.Student_coterie.Where(y => y.Id_student == i.Id_student).FirstOrDefault().Id_student_coterie);
                            foreach (var l in a)
                            {
                                if (l.IsAttended == 1)
                                {
                                    t++;
                                }
                            }
                            
                                UIElement clonedElement = CloneElement(element, i.Name + " " + i.MiddleName + " " + i.Surname + " ");
                            clonedCanvas.Children.Add(clonedElement);
                        }
                        CirclesStack.Children.Add(clonedCanvas);
                    }
                }
            }
        }
        private UIElement CloneElement(UIElement element, string CircleName)
        {
            if (element is Button)
            {
                Button originalButton = (Button)element;
                Button clonedButton = new Button();

                // Копирование свойств из originalButton в clonedButton
                clonedButton.Content = originalButton.Content;
                clonedButton.Width = originalButton.Width;
                clonedButton.Margin = originalButton.Margin;
                clonedButton.Height = originalButton.Height;
                // Копирование других свойств, если необходимо

                return clonedButton;
            }
            else if (element is TextBlock)
            {
                TextBlock originalTextBlock = (TextBlock)element;
                TextBlock clonedTextBlock = new TextBlock();

                // Копирование свойств из originalTextBlock в clonedTextBlock
                clonedTextBlock.Text = originalTextBlock.Text;
                clonedTextBlock.Width = originalTextBlock.Width;
                clonedTextBlock.Margin = originalTextBlock.Margin;
                clonedTextBlock.Height = originalTextBlock.Height;
                // Копирование других свойств, если необходимо

                return clonedTextBlock;
            }
            else if (element is Image)
            {
                Image originalTextBlock = (Image)element;
                Image clonedTextBlock = new Image();

                // Копирование свойств из originalTextBlock в clonedTextBlock
                clonedTextBlock.Source = originalTextBlock.Source;
                clonedTextBlock.Width = originalTextBlock.Width;
                clonedTextBlock.Height = originalTextBlock.Height;
                clonedTextBlock.Margin = originalTextBlock.Margin;
                clonedTextBlock.Stretch = originalTextBlock.Stretch;
                if (originalTextBlock.Source.ToString() == "pack://application:,,,/kabwbfkwwe.png")
                {
                    clonedTextBlock.MouseDown += Image_MouseDown;
                }
                // Копирование других свойств, если необходимо

                return clonedTextBlock;
            }
            else if (element is Label)
            {
                Label originalTextBlock = (Label)element;
                Label clonedTextBlock = new Label();

                // Копирование свойств из originalTextBlock в clonedTextBlock
                clonedTextBlock.Content = CircleName;
                clonedTextBlock.Width = originalTextBlock.Width;
                clonedTextBlock.Height = originalTextBlock.Height;
                clonedTextBlock.Margin = originalTextBlock.Margin;
                clonedTextBlock.FontWeight = originalTextBlock.FontWeight;
                clonedTextBlock.FontSize = originalTextBlock.FontSize;
                clonedTextBlock.Foreground = originalTextBlock.Foreground;
                clonedTextBlock.MouseDown += Image_MouseDown;
                // Копирование других свойств, если необходимо

                return clonedTextBlock;
            }
            // Добавьте другие типы элементов, которые вы хотите поддерживать

            // Если тип элемента не поддерживается, верните null или бросьте исключение
            return null;
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ShowCircles();

        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            ShowCircles();
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowCircles();
        }
    }
}
