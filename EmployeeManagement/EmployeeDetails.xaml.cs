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

        public EmployeeDetails()
        {
            InitializeComponent();
        }

        public EmployeeDetails(int employeeId) : this()
        {
            currentEmployee = (from em in dbContext.employees
                         where em.id == employeeId
                         select em).First();

            string managerName = "";
            if (currentEmployee.manager_id != null)
            {
                managerName = (from em in dbContext.employees
                               where em.id == currentEmployee.manager_id
                               select em).First().name;
            } 
            
            idLabel.Content = currentEmployee.id;
            nameLabel.Content = currentEmployee.name;
            addressLabel.Content = currentEmployee.address;
            emailLabel.Content = currentEmployee.email;
            contactLabel.Content = currentEmployee.phone;
            positionLabel.Content = currentEmployee.position.name;
            departmentLabel.Content = currentEmployee.department.name;
            shiftLabel.Content = currentEmployee.shift;
            managerLabel.Content = managerName;
            startDateLabel.Content = currentEmployee.start_date;
            employmentStatusLabel.Content = currentEmployee.employment_status;
            favoriteColorLabel.Content = currentEmployee.favorite_color;
        }

        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            int len = dbContext.employees.Local.Count();
            int pos = len;

            for (int i = 0; i < len; i++)
            {
                if (currentEmployee.id == dbContext.employees.Local[i].id) {
                    pos = i;
                    break;
                }
            }

            currentEmployee.address = "Provo";
            dbContext.employees.Local.Insert(pos, currentEmployee);
            MessageBox.Show("New address: " + currentEmployee.address);
            dbContext.SaveChanges();
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                dbContext.employees.Remove(currentEmployee);
                dbContext.SaveChanges();
            }

            EmployeeList employeeList = new EmployeeList();
            this.NavigationService.Navigate(employeeList);
        }
    }
}
