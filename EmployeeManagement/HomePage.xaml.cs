﻿using System;
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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void OnViewClick(object sender, RoutedEventArgs e)
        {
            EmployeeList employeeList = new EmployeeList();
            this.NavigationService.Navigate(employeeList);
        }

        private void OnAddClick_home(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            this.NavigationService.Navigate(addEmployee);
        }

        private void OnStatsClick(object sender, RoutedEventArgs e)
        {
            Reporting reportingPage = new Reporting();
            this.NavigationService.Navigate(reportingPage);
        }

    }
}
