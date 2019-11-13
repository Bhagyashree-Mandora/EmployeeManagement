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
    /// Interaction logic for ActivityLog.xaml
    /// </summary>
    public partial class ActivityLog : Page
    {
        hrEntities dbContext;
        public ActivityLog()
        {
            InitializeComponent();
        }

        public ActivityLog(employee e, hrEntities dbContext) : this()
        {
            this.dbContext = dbContext;
            var query =
            from l in dbContext.logs
            where l.employee_id == e.id
            select new { l.time, l.change_type, l.field_name, l.old_value, l.new_value };

            EmployeeActivityDataGrid.ItemsSource = query.ToList();
        }

    }
}
