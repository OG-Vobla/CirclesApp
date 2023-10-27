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
    /// Логика взаимодействия для SeclectCircleWindow.xaml
    /// </summary>
    public partial class SeclectCircleWindow : Window
    {
        public SeclectCircleWindow()
        {
            InitializeComponent();
            ShowCircles();

        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

            Grid circleGrid = FindParentGrid(sender as DependencyObject);

            if (circleGrid != null)
            {
                Label label = FindChildLabel(circleGrid);
                if (DbConectionClass.CirclesDBEntities.Coterie_employee.Where(x=> x.Id_coterie == DbConectionClass.CirclesDBEntities.Coterie.Where(i => i.Name == label.Content).FirstOrDefault().Id_Coterie &&  x.Id_employee == DbConectionClass.selectedEmployee.Id_employee ) != null)
                {
                    DbConectionClass.CirclesDBEntities.Coterie_employee.Add(new Coterie_employee() { Id_coterie = DbConectionClass.CirclesDBEntities.Coterie.Where(i => i.Name== label.Content).FirstOrDefault().Id_Coterie, Id_employee = DbConectionClass.selectedEmployee.Id_employee});
                    DbConectionClass.CirclesDBEntities.SaveChanges();
                    this.Close();
                }
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
            if (DbConectionClass.selectedEmployee != null)
            {

                var students = DbConectionClass.CirclesDBEntities.Coterie.Where(x => DbConectionClass.CirclesDBEntities.Coterie_employee.Where(i => x.Id_Coterie == i.Id_coterie && i.Id_employee == DbConectionClass.selectedEmployee.Id_employee).FirstOrDefault() == null).ToList();
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
                            UIElement clonedElement = CloneElement(element, i.Name);
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
    }
}
