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
        Employee tischerName;
        DateTime dayDate;
        public DirectorMainPage()
        {
            tischerName = null;
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
            tischerName = DbConectionClass.selectedEmployee;
            CultureInfo russianCulture = new CultureInfo("ru-RU");
            weekName = russianCulture.TextInfo.ToTitleCase(russianCulture.DateTimeFormat.GetDayName(dayDate.DayOfWeek));
            WeekDay.Content = weekName;
            DayDate.Content = dayDate.ToString("dd.MM.yyyy");
            if (tischerName != null)
            {
                TischerName.Content= tischerName.Name + " " + tischerName.MiddleName + " " + tischerName.Surname;
                var ClassList = DbConectionClass.CirclesDBEntities.Class.Where(x => x.Employee.Id_employee == tischerName.Id_employee).ToList();
                if (ClassList.Count() != 0)
                {
                    var ClassInDay = ClassList.Where(x => x.Date == dayDate).ToList();
                    if (ClassInDay.Count() != 0)
                    {
                        var FirstCircele = ClassInDay.Where(x => x.Time_start == new DateTime(10, 10, 10, 15, 0, 0).TimeOfDay).FirstOrDefault();
                        if (FirstCircele != null)
                        {
                            FirstCircleCab.Text = FirstCircele.Number_cabinet.ToString();
                            FirstCircleName.Content = FirstCircele.Coterie.Name;
                        }
                        else
                        {
                            FirstCircleCab.Text = "Нет";
                            FirstCircleName.Content = "Нет";
                        }
                        var SecondCircele = ClassInDay.Where(x => x.Time_start == new DateTime(10, 10, 10, 16, 10, 0).TimeOfDay).FirstOrDefault();
                        if (SecondCircele != null)
                        {
                            SecondCircleCab.Text = SecondCircele.Number_cabinet.ToString();
                            SecondCircleName.Content = SecondCircele.Coterie.Name;
                        }
                        else
                        {
                            SecondCircleCab.Text = "Нет";
                            SecondCircleName.Content = "Нет";
                        }
                        var ThirdCircele = ClassInDay.Where(x => x.Time_start == new DateTime(10, 10, 10, 17, 20, 0).TimeOfDay).FirstOrDefault();
                        if (ThirdCircele != null)
                        {
                            ThirdCircleCab.Text = ThirdCircele.Number_cabinet.ToString();
                            ThirdCircleName.Content = ThirdCircele.Coterie.Name;
                        }
                        else
                        {
                            ThirdCircleCab.Text = "Нет";
                            ThirdCircleName.Content = "Нет";
                        }
                    }
                    else
                    {
                        FirstCircleCab.Text = "Нет";
                        FirstCircleName.Content = "Нет";
                        SecondCircleCab.Text = "Нет";
                        SecondCircleName.Content = "Нет";
                        ThirdCircleCab.Text = "Нет";
                        ThirdCircleName.Content = "Нет";
                    }
                }
            }
            else
            {
                FirstCircleCab.Text = "Нет";
                FirstCircleName.Content = "Нет";
                SecondCircleCab.Text = "Нет";
                SecondCircleName.Content = "Нет";
                ThirdCircleCab.Text = "Нет";
                ThirdCircleName.Content = "Нет";
            }
            }

        private void TischerName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectTicherWindow selectTicherWindow = new SelectTicherWindow();
            selectTicherWindow.ShowDialog();
            RefreshDate();
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            SelectTicherWindow selectTicherWindow = new SelectTicherWindow();
            selectTicherWindow.ShowDialog();
            RefreshDate();
        }

        private void Image_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            if (DbConectionClass.selectedEmployee != null)
            {

                DbConectionClass.selectedCircle = null;
                SelectWindow selectTicherWindow = new SelectWindow();
                selectTicherWindow.ShowDialog();
                if (DbConectionClass.selectedCircle != null)
                {

                FirstCircleName.Content = DbConectionClass.selectedCircle.Name;
                }
                else
                {
                    FirstCircleName.Content = "Нет";
                }
            }
        }

        private void Image_MouseDown_4(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            if (DbConectionClass.selectedEmployee != null)
            {

                DbConectionClass.selectedCircle = null;
                SelectWindow selectTicherWindow = new SelectWindow();
                selectTicherWindow.ShowDialog(); if (DbConectionClass.selectedCircle != null)
                {

                    SecondCircleName.Content = DbConectionClass.selectedCircle.Name;
                }
                else
                {
                    SecondCircleName.Content = "Нет";
                }
            }
        }

        private void Image_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            if (DbConectionClass.selectedEmployee != null)
            {

                DbConectionClass.selectedCircle = null;
                SelectWindow selectTicherWindow = new SelectWindow();
                selectTicherWindow.ShowDialog(); if (DbConectionClass.selectedCircle != null)
                {

                    ThirdCircleName.Content = DbConectionClass.selectedCircle.Name;
                }
                else
                {
                    ThirdCircleName.Content = "Нет";
                }
            }
        }

        private void Image_MouseDown_7(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            if (FirstCircleName.Content != "Нет" && Int32.TryParse(FirstCircleCab.Text,out i) )
            {
                DbConectionClass.CirclesDBEntities.Class.Add(new Class() {Id_coterie = DbConectionClass.CirclesDBEntities.Coterie.Where(x=> 
                x.Name == FirstCircleName.Content).FirstOrDefault().Id_Coterie, 
                    Id_employee = tischerName.Id_employee, 
                    Date = dayDate, Number_cabinet = i, 
                    Time_start = new TimeSpan(15,0,0), 
                    Time_end = new TimeSpan(16, 0, 0) });
                DbConectionClass.CirclesDBEntities.SaveChanges();
            }

        }

        private void Image_MouseDown_8(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            if (SecondCircleName.Content != "Нет" && Int32.TryParse(SecondCircleCab.Text, out i))
            {
                DbConectionClass.CirclesDBEntities.Class.Add(new Class() { Id_coterie = DbConectionClass.CirclesDBEntities.Coterie.Where(x => x.Name == SecondCircleName.Content).FirstOrDefault().Id_Coterie, Id_employee = tischerName.Id_employee, Date = dayDate, Number_cabinet = i, Time_start = new TimeSpan(16, 10, 0), Time_end = new TimeSpan(17, 10, 0) });
                DbConectionClass.CirclesDBEntities.SaveChanges();

            }
        }

        private void Image_MouseDown_9(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            if (ThirdCircleName.Content != "Нет" && Int32.TryParse(ThirdCircleCab.Text, out i))
            {
                DbConectionClass.CirclesDBEntities.Class.Add(new Class() { Id_coterie = DbConectionClass.CirclesDBEntities.Coterie.Where(x => x.Name == ThirdCircleName.Content).FirstOrDefault().Id_Coterie, Id_employee = tischerName.Id_employee, Date = dayDate, Number_cabinet = i, Time_start = new TimeSpan(17, 20, 0), Time_end = new TimeSpan(18, 20, 0) });
                DbConectionClass.CirclesDBEntities.SaveChanges();
            }
        }

        private void Image_MouseDown_10(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            if (FirstCircleName.Content != "Нет" && Int32.TryParse(FirstCircleCab.Text, out i) && DbConectionClass.selectedEmployee != null)
            {
                var s = DbConectionClass.CirclesDBEntities.Class.Where(x =>
                x.Id_coterie == DbConectionClass.CirclesDBEntities.Coterie.Where(a => a.Name == FirstCircleName.Content).FirstOrDefault().Id_Coterie
                && x.Date == dayDate
                && x.Id_employee == DbConectionClass.selectedEmployee.Id_employee
                && x.Number_cabinet == i
                && x.Time_start == new TimeSpan(15, 0, 0)
                && x.Time_end == new TimeSpan(16, 0, 0)).FirstOrDefault();
                var list = DbConectionClass.CirclesDBEntities.Student_Check.Where(z => z.Id_class == s.Id_class).ToList();
                foreach (var k in list)
                {
                    DbConectionClass.CirclesDBEntities.Student_Check.Remove(k);
                }
                DbConectionClass.CirclesDBEntities.Class.Remove(s);
                DbConectionClass.CirclesDBEntities.SaveChanges();
                RefreshDate();
            }
        }

        private void Image_MouseDown_11(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            if (SecondCircleName.Content != "Нет" && Int32.TryParse(SecondCircleCab.Text, out i) && DbConectionClass.selectedEmployee != null)
            {

                var s = DbConectionClass.CirclesDBEntities.Class.Remove(DbConectionClass.CirclesDBEntities.Class.Where(x =>
                x.Id_coterie == DbConectionClass.CirclesDBEntities.Coterie.Where(a => a.Name == SecondCircleName.Content).FirstOrDefault().Id_Coterie
                && x.Date == dayDate
                && x.Id_employee == DbConectionClass.selectedEmployee.Id_employee
                && x.Number_cabinet == i
                && x.Time_start == new TimeSpan(16, 10, 0)
                && x.Time_end == new TimeSpan(17, 10, 0)).FirstOrDefault());
                foreach (var k in DbConectionClass.CirclesDBEntities.Student_Check.Where(z => z.Id_class == s.Id_class))
                {
                    DbConectionClass.CirclesDBEntities.Student_Check.Remove(k);
                }
                DbConectionClass.CirclesDBEntities.Class.Remove(s);
                DbConectionClass.CirclesDBEntities.SaveChanges();
                RefreshDate();
            }
        }

        private void Image_MouseDown_12(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            if (ThirdCircleName.Content != "Нет" && Int32.TryParse(ThirdCircleCab.Text, out i) && DbConectionClass.selectedEmployee != null)
            {

                var s = DbConectionClass.CirclesDBEntities.Class.Remove(DbConectionClass.CirclesDBEntities.Class.Where(x =>
               x.Id_coterie == DbConectionClass.CirclesDBEntities.Coterie.Where(a => a.Name == ThirdCircleName.Content).FirstOrDefault().Id_Coterie
                && x.Date == dayDate
                && x.Id_employee == DbConectionClass.selectedEmployee.Id_employee
                && x.Number_cabinet == i
                && x.Time_start == new TimeSpan(16, 10, 0)
                && x.Time_end == new TimeSpan(17, 10, 0)).FirstOrDefault());
                foreach (var k in DbConectionClass.CirclesDBEntities.Student_Check.Where(z => z.Id_class == s.Id_class))
                {
                    DbConectionClass.CirclesDBEntities.Student_Check.Remove(k);
                }
                DbConectionClass.CirclesDBEntities.Class.Remove(s);
                DbConectionClass.CirclesDBEntities.SaveChanges();

                RefreshDate();
            }
        }
    }
}

