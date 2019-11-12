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
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            employee newEmployee = new employee() {
                name = "Sample",
                address = "test",
                email = "sample@test.com",
                phone = "1234567890",
                position_id = 1,
                department_id = 1,
                start_date = DateTime.Parse("4/15/2019"),
                end_date = null,
                employment_status = "Employed",
                shift = "Evening",
                manager_id = null,
                favorite_color = "Black",
                photo = null
            };

            dbContext.employees.Add(newEmployee);
            MessageBox.Show("New employee record added!");
            dbContext.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
