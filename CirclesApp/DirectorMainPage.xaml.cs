using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для DirectorMainPage.xaml
    /// </summary>
    public partial class DirectorMainPage : Page
    {
        string weekName;
        DateTime dayDate;
        public DirectorMainPage()
        {
            dayDate = DateTime.Today.Date;
            InitializeComponent();
            RefreshDate();

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dayDate = dayDate.AddDays(-1);
            RefreshDate();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            dayDate = dayDate.AddDays(1);
            RefreshDate();
        }
        private void RefreshDate()
        {
            CultureInfo russianCulture = new CultureInfo("ru-RU");
            weekName = russianCulture.TextInfo.ToTitleCase(russianCulture.DateTimeFormat.GetDayName(dayDate.DayOfWeek));
            WeekDay.Content = weekName;
            DayDate.Content = dayDate.ToString("dd.MM.yyyy");
            var ClassList = DbConectionClass.CirclesDBEntities.Class.Where(x => x.Employee.Id_employee == DbConectionClass.user.Id_employee).ToList();
            if (ClassList.Count() != 0)
            {
                var ClassInDay = ClassList.Where(x => x.Date == dayDate).ToList();
                if (ClassInDay.Count() != 0)
                {
                    var FirstCircele = ClassInDay.Where(x => x.Time_start == new DateTime(10, 10, 10, 15, 0, 0).TimeOfDay).FirstOrDefault();
                    if (FirstCircele != null)
                    {
                        FirstCircleCab.Content = FirstCircele.Number_cabinet;
                        FirstCircleName.Content = FirstCircele.Coterie.Name;
                    }
                    else
                    {
                        FirstCircleCab.Content = "Нет";
                        FirstCircleName.Content = "Нет";
                    }
                    var SecondCircele = ClassInDay.Where(x => x.Time_start == new DateTime(10, 10, 10, 16, 10, 0).TimeOfDay).FirstOrDefault();
                    if (SecondCircele != null)
                    {
                        SecondCircleCab.Content = SecondCircele.Number_cabinet;
                        SecondCircleName.Content = SecondCircele.Coterie.Name;
                    }
                    else
                    {
                        SecondCircleCab.Content = "Нет";
                        SecondCircleName.Content = "Нет";
                    }
                    var ThirdCircele = ClassInDay.Where(x => x.Time_start == new DateTime(10, 10, 10, 17, 20, 0).TimeOfDay).FirstOrDefault();
                    if (ThirdCircele != null)
                    {
                        ThirdCircleCab.Content = ThirdCircele.Number_cabinet;
                        ThirdCircleName.Content = ThirdCircele.Coterie.Name;
                    }
                    else
                    {
                        ThirdCircleCab.Content = "Нет";
                        ThirdCircleName.Content = "Нет";
                    }
                }
                else
                {
                    FirstCircleCab.Content = "Нет";
                    FirstCircleName.Content = "Нет";
                    SecondCircleCab.Content = "Нет";
                    SecondCircleName.Content = "Нет";
                    ThirdCircleCab.Content = "Нет";
                    ThirdCircleName.Content = "Нет";
                }
            }
        }
    }
}

