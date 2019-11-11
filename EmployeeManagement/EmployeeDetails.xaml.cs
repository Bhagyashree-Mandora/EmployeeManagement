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

using System.Data.Entity;

namespace EmployeeManagement
{
    /// <summary>
    /// Interaction logic for EmployeeDetails.xaml
    /// </summary>
    public partial class EmployeeDetails : Page
    {
        employee currentEmployee;

        hrEntities dbContext = new hrEntities();
        //HrDbContext dbContext = new HrDbContext();

        public EmployeeDetails()
        {
            InitializeComponent();
        }

        public EmployeeDetails(string name) : this()
        {
            currentEmployee = (from em in dbContext.employees
                         where em.name == name
                         select em).First();
            
            nameLabel.Content = currentEmployee.name;
            addressLabel.Content = currentEmployee.address;
        }

        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            int len = dbContext.employees.Local.Count();
            int pos = len;

            for (int i = 0; i < len; i++)
            {
                if (String.Compare(currentEmployee.name, dbContext.employees.Local[i].name) == 0) {
                    pos = i;
                    break;
                }
            }

            currentEmployee.address = "xxx";
            dbContext.employees.Local.Insert(pos, currentEmployee);
            MessageBox.Show("New address: " + currentEmployee.address);
            dbContext.SaveChanges();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            // dbContext.employee.Remove();
        }
    }
}
