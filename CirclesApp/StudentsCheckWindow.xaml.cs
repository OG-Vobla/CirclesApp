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
    /// Логика взаимодействия для StudentsCheckWindow.xaml
    /// </summary>
    public partial class StudentsCheckWindow : Window
    {
        string circleName;
        Class selectedDate;
        public StudentsCheckWindow(string CircleName, Class SelectedDate)
        {
            selectedDate = SelectedDate;
            circleName = CircleName;
            InitializeComponent();
            ShowCircles();
            Circlename.Content = circleName;
            CircleDate.Content = selectedDate.Date.ToString("dd.MM.yyyy");

        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Grid circleGrid = FindParentGrid(sender as DependencyObject);
            if (circleGrid != null)
            {
                Label label = FindChildLabel(circleGrid);
                if ((sender as Image).Source.ToString() == "pack://application:,,,/sdgrgdrgr.png")
                {
                    DbConectionClass.CirclesDBEntities.Student_Check.Remove(DbConectionClass.CirclesDBEntities.Student_Check.Where(x => x.Class.Coterie.Name == circleName && x.Class.Date == selectedDate.Date && x.Student_coterie.Student.Name + " " + x.Student_coterie.Student.MiddleName + " " + x.Student_coterie.Student.Surname == label.Content).FirstOrDefault());
                    DbConectionClass.CirclesDBEntities.Student_Check.Add(new Student_Check()
                    {
                        Id_student_coterie = DbConectionClass.CirclesDBEntities.Student_coterie.Where(x => x.Student.Name + " " + x.Student.MiddleName + " " + x.Student.Surname == label.Content && x.Coterie.Name == circleName).FirstOrDefault().Id_student_coterie,
                        Id_class = selectedDate.Id_class,
                        IsAttended = 1
                    });
                }
                else 
                {
                    DbConectionClass.CirclesDBEntities.Student_Check.Remove(DbConectionClass.CirclesDBEntities.Student_Check.Where(x => x.Class.Coterie.Name == circleName && x.Class.Date == selectedDate.Date && x.Student_coterie.Student.Name + " " + x.Student_coterie.Student.MiddleName + " " + x.Student_coterie.Student.Surname == label.Content).FirstOrDefault());
                    DbConectionClass.CirclesDBEntities.Student_Check.Add(new Student_Check()
                    {
                        Id_student_coterie = DbConectionClass.CirclesDBEntities.Student_coterie.Where(x => x.Student.Name + " " + x.Student.MiddleName + " " + x.Student.Surname == label.Content && x.Coterie.Name == circleName).FirstOrDefault().Id_student_coterie,
                        Id_class = selectedDate.Id_class,
                        IsAttended = 0
                    });
                }
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

            var students = DbConectionClass.CirclesDBEntities.Student.Where(x => DbConectionClass.CirclesDBEntities.Student_coterie.Where(i => x.Id_student == i.Id_student && i.Coterie.Name == circleName).FirstOrDefault() != null).ToList();
            if (students.Count() != 0)
            {
                foreach (var i in students)
                {

                    Grid clonedCanvas = new Grid();
                    clonedCanvas.Margin = CircleGrid.Margin;
                    clonedCanvas.Width = CircleGrid.Width;
                    clonedCanvas.Height = CircleGrid.Height;
                    foreach (UIElement element in CircleGrid.Children)
                    {
                        UIElement clonedElement = CloneElement(element, i.Name + " " + i.MiddleName + " " + i.Surname);
                        if (clonedElement != null)
                        {

                        clonedCanvas.Children.Add(clonedElement);
                        }
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
                var list = DbConectionClass.CirclesDBEntities.Student_Check.Where(x => x.Class.Coterie.Name == circleName && x.Class.Date == selectedDate.Date);
                if (list.Where(x => x.Student_coterie.Student.Name + " " + x.Student_coterie.Student.MiddleName + " " + x.Student_coterie.Student.Surname == CircleName).FirstOrDefault() == null)
                {
                    var stud = DbConectionClass.CirclesDBEntities.Student_coterie.Where(x => x.Student.Name + " " + x.Student.MiddleName + " " + x.Student.Surname == CircleName && x.Coterie.Name == circleName).FirstOrDefault();
                    if (stud != null)
                    {
                        DbConectionClass.CirclesDBEntities.Student_Check.Add(new Student_Check()
                        {
                            Id_student_coterie = stud.Id_student_coterie,
                            Id_class = selectedDate.Id_class,
                            IsAttended = 0
                        });
                        DbConectionClass.CirclesDBEntities.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
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
                


                var list = DbConectionClass.CirclesDBEntities.Student_Check.Where(x => x.Class.Coterie.Name == circleName && x.Class.Date == selectedDate.Date);
                if (list.Where(x => x.Student_coterie.Student.Name + " " + x.Student_coterie.Student.MiddleName + " " + x.Student_coterie.Student.Surname == CircleName).FirstOrDefault() == null)
                {
                    var stud = DbConectionClass.CirclesDBEntities.Student_coterie.Where(x => x.Student.Name + " " + x.Student.MiddleName + " " + x.Student.Surname == CircleName && x.Coterie.Name == circleName).FirstOrDefault();
                    if (stud != null)
                    {
                        DbConectionClass.CirclesDBEntities.Student_Check.Add(new Student_Check() { Id_student_coterie = stud.Id_student_coterie,
                        Id_class = selectedDate.Id_class, IsAttended = 0});
                        DbConectionClass.CirclesDBEntities.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
                list = DbConectionClass.CirclesDBEntities.Student_Check.Where(x => x.Class.Coterie.Name == circleName && x.Class.Date == selectedDate.Date);
                if (list.Where(x => x.Student_coterie.Student.Name + " " + x.Student_coterie.Student.MiddleName + " " + x.Student_coterie.Student.Surname == CircleName).FirstOrDefault()?.IsAttended == 0)
                {
                    if (originalTextBlock.Source.ToString() == "pack://application:,,,/sdgrgdrgr.png")
                    {
                        clonedTextBlock.Visibility = Visibility.Visible;
                    }
                    else if (originalTextBlock.Source.ToString() == "pack://application:,,,/bjsvfjsef.png")
                    {
                        clonedTextBlock.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    if (originalTextBlock.Source.ToString() == "pack://application:,,,/sdgrgdrgr.png")
                    {
                        clonedTextBlock.Visibility = Visibility.Hidden;
                    }
                    else if (originalTextBlock.Source.ToString() == "pack://application:,,,/bjsvfjsef.png")
                    {
                        clonedTextBlock.Visibility = Visibility.Visible;
                    }
                }
                
                clonedTextBlock.Width = originalTextBlock.Width;
                clonedTextBlock.Height = originalTextBlock.Height;
                clonedTextBlock.Margin = originalTextBlock.Margin;
                clonedTextBlock.Stretch = originalTextBlock.Stretch;
                if (originalTextBlock.Source.ToString() == "pack://application:,,,/bjsvfjsef.png" || originalTextBlock.Source.ToString() == "pack://application:,,,/sdgrgdrgr.png")
                {
                    clonedTextBlock.MouseDown += Image_MouseDown;
                }
                // Копирование других свойств, если необходимо

                return clonedTextBlock;
            }
            else if (element is Label)
            {
                var list = DbConectionClass.CirclesDBEntities.Student_Check.Where(x => x.Class.Coterie.Name == circleName && x.Class.Date == selectedDate.Date);
                if (list.Where(x => x.Student_coterie.Student.Name + " " + x.Student_coterie.Student.MiddleName + " " + x.Student_coterie.Student.Surname == CircleName).FirstOrDefault() == null)
                {
                    var stud = DbConectionClass.CirclesDBEntities.Student_coterie.Where(x => x.Student.Name + " " + x.Student.MiddleName + " " + x.Student.Surname == CircleName && x.Coterie.Name == circleName).FirstOrDefault();
                    if (stud != null)
                    {
                        DbConectionClass.CirclesDBEntities.Student_Check.Add(new Student_Check()
                        {
                            Id_student_coterie = stud.Id_student_coterie,
                            Id_class = selectedDate.Id_class,
                            IsAttended = 0
                        });
                        DbConectionClass.CirclesDBEntities.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                }
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
    }
}

