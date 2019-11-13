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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        hrEntities dbContext = new hrEntities();

        public AddEmployee()
        {
            InitializeComponent();
            var positionNames = from pos in dbContext.positions
                                select pos.name;
            positionComboBox.ItemsSource = positionNames.ToList();
            positionComboBox.SelectedIndex = 1;

            var departmentNames = from dept in dbContext.departments
                                  select dept.name;
            departmentComboBox.ItemsSource = departmentNames.ToList();
            departmentComboBox.SelectedIndex = 1;

            var managerNames = from em in dbContext.employees
                               where em.position_id == 2 && em.position.name == "Manager"
                               select em.name + " - " + em.id;
            managerComboBox.ItemsSource = managerNames.ToList();

            startDatePicker.SelectedDate = DateTime.Now;
        }

        private void OnCreateClick(object sender, RoutedEventArgs e)
        {
            
            employee newEmployee = new employee() {
                name = nameTextBox.Text,
                address = addressTextBox.Text,
                email = emailTextBox.Text,
                phone = phoneTextBox.Text,
                employment_status = employmentStatusComboBox.Text,
                shift = shiftComboBox.Text,
                favorite_color = favoriteColorTextBox.Text,
            };
            
            if (startDatePicker.SelectedDate.HasValue)
            {
                newEmployee.start_date = startDatePicker.SelectedDate.Value;
            }

            newEmployee.position_id = (from pos in dbContext.positions
                                where pos.name == positionComboBox.Text
                                select pos.id).First();

            newEmployee.department_id = (from dept in dbContext.departments
                                        where dept.name == departmentComboBox.Text
                                        select dept.id).First();

            string mgrString = managerComboBox.Text;
            string[] parts = mgrString.Split('-');
            if (parts.Length > 1)
            {
                newEmployee.manager_id = Int32.Parse(parts[1].Trim());
            }
                
            dbContext.employees.Add(newEmployee);
            dbContext.SaveChanges();

            log logEntry = new log()
            {
                time = System.DateTime.Now,
                change_type = "Create",
                employee_id = newEmployee.id,
                new_value = newEmployee.name
            };
            dbContext.logs.Add(logEntry);
            dbContext.SaveChanges();
            MessageBox.Show("Employee " + newEmployee.name + " added!");

            HomePage homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }
    }
}
