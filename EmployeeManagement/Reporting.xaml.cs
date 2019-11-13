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

namespace EmployeeManagement
{
    /// <summary>
    /// Interaction logic for Reporting.xaml
    /// </summary>
    /// 
    public partial class Reporting : Page
    {
        hrEntities dbContext = new hrEntities();

        public Reporting()
        {
            InitializeComponent();
        }

        private void StatPageLoaded(object sender, RoutedEventArgs e)
        {
            /*
            WeeklyStats.ItemsSource = dbContext.employees.SqlQuery("SELECT * " +
                "FROM employees " +
                "WHERE start_date BETWEEN '2019-01-01' AND '2020-01-01' " +
                "group by datepart(ww, start_date)").Count();
             */  
            labelHiredCount.Content = dbContext.employees.SqlQuery("select * from employees where employment_status = 'Terminated' AND end_date between '2019-01-01' and '2019-12-01'").Count();
        }
    }
}
