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
        employee currentEmployee;
        string oldManagerName;
        List<log> UpdateLogs = new List<log>();

        public UpdateEmployee()
        {
            InitializeComponent();
        }

        public UpdateEmployee(employee currEmployee, hrEntities dbContext) : this()
        {
            this.currentEmployee = currEmployee;
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
            endDatePicker.SelectedDate = currEmployee.end_date;
            favoriteColorTextBox.Text = currEmployee.favorite_color;
            if (currEmployee.manager_id.HasValue)
            {
                oldManagerName = (from em in dbContext.employees
                                  where em.id == currEmployee.manager_id
                                  select em.name).First();
                managerComboBox.Text = managerEntryFormat(oldManagerName, currEmployee.manager_id.Value);
            }
        }

        private string managerEntryFormat(string name, int id)
        {
            return name + " - " + id;
        }

        private log logEntry(string field, object oldVal, object newVal)
        {
            return new log()
            {
                employee_id = currentEmployee.id,
                time = System.DateTime.Now,
                change_type = "Update",
                field_name = field,
                old_value = Convert.ToString(oldVal),
                new_value = Convert.ToString(newVal),
            };
        }

        public void OnUpdateChangesClick(object sender, RoutedEventArgs e)
        {
            List<log> entries = new List<log>();
            if (!currentEmployee.name.Equals(nameTextBox.Text))
            {
                entries.Add(logEntry("Name", currentEmployee.name, nameTextBox.Text));
                currentEmployee.name = nameTextBox.Text;
            }
            if (!currentEmployee.address.Equals(addressTextBox.Text))
            {
                entries.Add(logEntry("Address", currentEmployee.address, addressTextBox.Text));
                currentEmployee.address = addressTextBox.Text;
            }
            if (!currentEmployee.email.Equals(emailTextBox.Text))
            {
                entries.Add(logEntry("Email", currentEmployee.email, emailTextBox.Text));
                currentEmployee.email = emailTextBox.Text;
            }
            if (!currentEmployee.phone.Equals(phoneTextBox.Text))
            {
                entries.Add(logEntry("Phone", currentEmployee.phone, phoneTextBox.Text));
                currentEmployee.phone = phoneTextBox.Text;
            }
            if (!currentEmployee.employment_status.Equals(employmentStatusComboBox.Text))
            {
                entries.Add(logEntry("Employment Status", currentEmployee.employment_status, employmentStatusComboBox.Text));
                currentEmployee.employment_status = employmentStatusComboBox.Text;
            }
            if (!currentEmployee.shift.Equals(shiftComboBox.Text))
            {
                entries.Add(logEntry("Shift", currentEmployee.shift, shiftComboBox.Text));
                currentEmployee.shift = shiftComboBox.Text;
            }
            if (!currentEmployee.favorite_color.Equals(favoriteColorTextBox.Text))
            {
                entries.Add(logEntry("Favorite Color", currentEmployee.email, favoriteColorTextBox.Text));
                currentEmployee.favorite_color = favoriteColorTextBox.Text;
            }
            if (startDatePicker.SelectedDate.HasValue)
            {
                if (!currentEmployee.start_date.Equals(startDatePicker.SelectedDate.Value))
                {
                    entries.Add(logEntry("Start Date", currentEmployee.start_date, startDatePicker.SelectedDate.Value));
                }
                currentEmployee.start_date = startDatePicker.SelectedDate.Value;
            }
            if (endDatePicker.SelectedDate.HasValue)
            {
                if (!currentEmployee.start_date.Equals(endDatePicker.SelectedDate.Value))
                {
                    entries.Add(logEntry("End Date", currentEmployee.end_date, endDatePicker.SelectedDate.Value));
                }
                currentEmployee.end_date = endDatePicker.SelectedDate.Value;
            }
            if (!currentEmployee.position.name.Equals(positionComboBox.Text)) 
            {
                entries.Add(logEntry("Position", currentEmployee.position.name, positionComboBox.Text));
                currentEmployee.position_id = (from posi in dbContext.positions
                                               where posi.name == positionComboBox.Text
                                               select posi.id).First();
            }
            if (!currentEmployee.department.name.Equals(departmentComboBox.Text))
            {
                entries.Add(logEntry("Department", currentEmployee.department.name, departmentComboBox.Text));
                currentEmployee.department_id = (from dept in dbContext.departments
                                                 where dept.name == departmentComboBox.Text
                                                 select dept.id).First();
            }

            string mgrString = managerComboBox.Text;
            string[] parts = mgrString.Split('-');
            if (parts.Length > 1)
            {
                int newManagerId = Int32.Parse(parts[1].Trim());
                if (currentEmployee.manager_id != newManagerId)
                {
                    entries.Add(logEntry("Manager", oldManagerName, parts[0].Trim()));
                    currentEmployee.manager_id = newManagerId;
                }
            }

            int len = dbContext.employees.Local.Count();
            int pos = len;

            for (int i = 0; i < len; i++)
            {
                if (currentEmployee.id == dbContext.employees.Local[i].id)
                {
                    pos = i;
                    break;
                }
            }

            dbContext.employees.Local.Insert(pos, currentEmployee);
            dbContext.SaveChanges();

            addChangeLog(entries);

            EmployeeDetails employeeDetails = new EmployeeDetails(currentEmployee.id);
            this.NavigationService.Navigate(employeeDetails);
        }

        private void addChangeLog(List<log> logEntries)
        {
            foreach (var entry in logEntries)
            {
                dbContext.logs.Add(entry);
            }
            dbContext.SaveChanges();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails(currentEmployee.id);
            this.NavigationService.Navigate(employeeDetails);
        }

    }
}
