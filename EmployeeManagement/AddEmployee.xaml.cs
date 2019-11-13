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
        //employee currEmployee;

        public AddEmployee()
        {
            InitializeComponent();
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
        }

        //public AddEmployee(employee currEmployee) : this()
        //{
        //    this.currEmployee = currEmployee;
        //    nameTextBox.Text = currEmployee.name;
        //    addressTextBox.Text = currEmployee.address;
        //    emailTextBox.Text = currEmployee.email;
        //    phoneTextBox.Text = currEmployee.phone;
        //    employmentStatusComboBox.Text = currEmployee.employment_status;
        //    positionComboBox.Text = currEmployee.position.name;
        //    departmentComboBox.Text = currEmployee.department.name;
        //    shiftComboBox.Text = currEmployee.shift;
        //    startDatePicker.SelectedDate = currEmployee.start_date;
        //    favoriteColorTextBox.Text = currEmployee.favorite_color;
        //    if (currEmployee.manager_id.HasValue)
        //    {
        //        managerComboBox.Text = (from em in dbContext.employees
        //                                where em.id == currEmployee.manager_id
        //                                select em.name + " - " + em.id).First();
        //    }
        //}

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
            
            //if (currEmployee != null)
            //{
            //    newEmployee.id = currEmployee.id;
            //}

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
                
            //Page destination;
            //if (currEmployee == null)
            //{
            //    dbContext.employees.Add(newEmployee);
            //    MessageBox.Show("Employee " + newEmployee.name + " added!");
            //    destination = new HomePage();
            //}
            //else
            //{
            //    int len = dbContext.employees.Local.Count();
            //    int pos = len;

            //    for (int i = 0; i < len; i++)
            //    {
            //        if (currEmployee.id == dbContext.employees.Local[i].id)
            //        {
            //            pos = i;
            //            break;
            //        }
            //    }

            //    dbContext.employees.Local.Insert(pos, currEmployee);
            //    destination = new EmployeeDetails(currEmployee.id);
            //}

            dbContext.employees.Add(newEmployee);
            dbContext.SaveChanges();

            log logEntry = new log()
            {
                time = System.DateTime.Now,
                change_type = "Create",
                employee_id = newEmployee.id,
                field_name = "Employee name",
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
            //Page destination;
            //if (currEmployee == null)
            //{
            //    destination = new HomePage();
            //} else
            //{
            //    destination = new EmployeeDetails(currEmployee.id);
            //}
            //this.NavigationService.Navigate(destination);

            HomePage homePage = new HomePage();
            this.NavigationService.Navigate(homePage);
        }
    }
}
