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

namespace CM3036_CW_1504693
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Coursework for CM3036 | Programming in C#
    /// Author: Andrew Tait (1504693)
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private DBStudent context;
        private List<Student> Students;
        private System.Windows.Data.CollectionViewSource StudentsViewSource;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            StudentsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dBStudentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // dBStudentViewSource.Source = [generic data source]
            context = new DBStudent();
            //OnRefresh(sender, e);
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            var query = from student in context.Students select student;
            // convert set of Appointment objects to list of Appointment objects
            Students = query.ToList();
            // connect list of Appointment entities to collection view
            StudentsViewSource.Source =  Students;
        }


    }
}
