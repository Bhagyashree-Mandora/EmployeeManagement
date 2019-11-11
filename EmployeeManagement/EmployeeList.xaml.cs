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
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Page
    {

        hrEntities dbContext = new hrEntities();

        public EmployeeList()
        {
            InitializeComponent();
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            var query =
            from em in dbContext.employees
            select new EmployeeListItem 
            {
                id = em.id,
                name = em.name,
                address = em.address,
            };
            
            EmployeeListDataGrid.ItemsSource = query.ToList();
        }

        private void ViewDetailsHandler(object sender, MouseButtonEventArgs e)
        {
            EmployeeListItem em = (EmployeeListItem)this.EmployeeListDataGrid.SelectedItem;
            EmployeeDetails employeeDetails = new EmployeeDetails(em.name);
            this.NavigationService.Navigate(employeeDetails);
        }

        public class EmployeeListItem
        {
            public int id { get; set; }
            public string name { get; set; }
            public string address { get; set; }

            public EmployeeListItem() { }
          
        }
    }
}
