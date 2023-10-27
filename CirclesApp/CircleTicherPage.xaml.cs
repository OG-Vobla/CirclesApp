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
    /// Логика взаимодействия для CircleTicherPage.xaml
    /// </summary>
    public partial class CircleTicherPage : Page
    {
        Employee selectedEmployee;
        public CircleTicherPage()
        {
            selectedEmployee = DbConectionClass.selectedEmployee;
            InitializeComponent();
        ShowCircles();
        }


    private void Image_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Grid circleGrid = FindParentGrid(sender as DependencyObject);
        if (circleGrid != null)
        {
            Label label = FindChildLabel(circleGrid);
            ConfirmationWindow circlesPage = new ConfirmationWindow();
            circlesPage.ShowDialog();
                if (circlesPage.DialogResult)
                {
                    var list = DbConectionClass.CirclesDBEntities.Class.Where(x=> x.Id_employee == selectedEmployee.Id_employee && x.Id_coterie == DbConectionClass.CirclesDBEntities.Coterie.Where(i=> i.Name == label.Content).FirstOrDefault().Id_Coterie);
                    foreach (var item in list)
                    {
                        DbConectionClass.CirclesDBEntities.Class.Remove(item);
                    }
                    DbConectionClass.CirclesDBEntities.Coterie_employee.Remove(DbConectionClass.CirclesDBEntities.Coterie_employee.Where(x => x.Id_employee == selectedEmployee.Id_employee && x.Id_coterie == DbConectionClass.CirclesDBEntities.Coterie.Where(i => i.Name == label.Content).FirstOrDefault().Id_Coterie).FirstOrDefault());
                    DbConectionClass.CirclesDBEntities.SaveChanges();
                    ShowCircles();
                }
                else
                {

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

            while (CirclesStack.Children.Count != 1)
            {
                CirclesStack.Children.RemoveAt(1);
            }
            if (selectedEmployee != null)
            {
                TischerName.Content = selectedEmployee.Name + " " + selectedEmployee.MiddleName + " " + selectedEmployee.Surname;
                var employesCircles = DbConectionClass.CirclesDBEntities.Coterie_employee.Where(x => x.Id_employee == selectedEmployee.Id_employee).ToList();
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
                        UIElement clonedElement = CloneElement(element, i.Coterie.Name);
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
                if (originalTextBlock.Source.ToString() == "pack://application:,,,/dsdghfghfhd.png")
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
            // Копирование других свойств, если необходимо

            return clonedTextBlock;
        }
        // Добавьте другие типы элементов, которые вы хотите поддерживать

        // Если тип элемента не поддерживается, верните null или бросьте исключение
        return null;
    }

        private void TischerName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectTicherWindow selectTicherWindow = new SelectTicherWindow();
            selectTicherWindow.ShowDialog();
            selectedEmployee = DbConectionClass.selectedEmployee;
            ShowCircles();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            SeclectCircleWindow selectTicherWindow = new SeclectCircleWindow();
            selectTicherWindow.ShowDialog();
            
            ShowCircles();
        }
    }

    }