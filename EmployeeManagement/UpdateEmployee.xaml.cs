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
    /// Interaction logic for UpdateEmployee.xaml
    /// </summary>
    public partial class UpdateEmployee : Page
    {
        hrEntities dbContext;
        employee currEmployee;

        public UpdateEmployee()
        {
            InitializeComponent();
        }

        public UpdateEmployee(employee currEmployee, hrEntities dbContext) : this()
        {
            this.currEmployee = currEmployee;
            this.dbContext = dbContext;

            var positionNames = from pos in dbContext.positions
                                select pos.name;
            positionComboBox.ItemsSource = positionNames.ToList();

            var departmentNames = from dept in dbContext.departments
                                  select dept.name;
            departmentComboBox.ItemsSource = departmentNames.ToList();

            var managerNames = from em in dbContext.employees
                               where em.position_id == 2 && em.position.name == "Manager"
                               select em.name + " - " + em.id;
            managerComboBox.ItemsSource = managerNames.ToList();

            nameTextBox.Text = currEmployee.name;
            addressTextBox.Text = currEmployee.address;
            emailTextBox.Text = currEmployee.email;
            phoneTextBox.Text = currEmployee.phone;
            employmentStatusComboBox.Text = currEmployee.employment_status;
            positionComboBox.Text = currEmployee.position.name;
            departmentComboBox.Text = currEmployee.department.name;
            shiftComboBox.Text = currEmployee.shift;
            startDatePicker.SelectedDate = currEmployee.start_date;
            favoriteColorTextBox.Text = currEmployee.favorite_color;
            if (currEmployee.manager_id.HasValue)
            {
                managerComboBox.Text = (from em in dbContext.employees
                                        where em.id == currEmployee.manager_id
                                        select em.name + " - " + em.id).First();
            }
        }

        public void OnUpdateChangesClick(object sender, RoutedEventArgs e) 
        {
            currEmployee.name = nameTextBox.Text;
            currEmployee.address = addressTextBox.Text;
            currEmployee.email = emailTextBox.Text;
            currEmployee.phone = phoneTextBox.Text;
            currEmployee.employment_status = employmentStatusComboBox.Text;
            currEmployee.shift = shiftComboBox.Text;
            currEmployee.favorite_color = favoriteColorTextBox.Text;

            if (startDatePicker.SelectedDate.HasValue)
            {
                currEmployee.start_date = startDatePicker.SelectedDate.Value;
            }

            currEmployee.position_id = (from posi in dbContext.positions
                                       where posi.name == positionComboBox.Text
                                       select posi.id).First();

            currEmployee.department_id = (from dept in dbContext.departments
                                         where dept.name == departmentComboBox.Text
                                         select dept.id).First();

            string mgrString = managerComboBox.Text;
            string[] parts = mgrString.Split('-');
            if (parts.Length > 1)
            {
                currEmployee.manager_id = Int32.Parse(parts[1].Trim());
            }


            int len = dbContext.employees.Local.Count();
            int pos = len;

            for (int i = 0; i < len; i++)
            {
                if (currEmployee.id == dbContext.employees.Local[i].id)
                {
                    pos = i;
                    break;
                }
            }

            dbContext.employees.Local.Insert(pos, currEmployee);
            dbContext.SaveChanges();

            EmployeeDetails employeeDetails = new EmployeeDetails(currEmployee.id);
            this.NavigationService.Navigate(employeeDetails);
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails(currEmployee.id);
            this.NavigationService.Navigate(employeeDetails);
        }
    }
}
