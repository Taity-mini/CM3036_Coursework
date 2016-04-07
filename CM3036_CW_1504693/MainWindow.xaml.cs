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
using System.Diagnostics;
using System.Data.Entity;

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
            // Load data by setting the CollectionViewSource.Source property:
            // dBStudentViewSource.Source = [generic data source]
            context = new DBStudent();

            StudentsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("studentViewSource")));
            OnRefresh(sender, e);
            // Load data by setting the CollectionViewSource.Source property:
            // studentViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource dBStudentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("dBStudentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // dBStudentViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource studentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("studentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // studentViewSource.Source = [generic data source]
        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            var query = from student in context.Students select student;
            // convert set of Appointment objects to list of Appointment objects
            Students = query.ToList();
            // connect list of Appointment entities to collection view
            StudentsViewSource.Source = Students;
        }

        //Adding a student's grades to the database
        private void onAddStudent(object sender, RoutedEventArgs e)
        {
            //Field variables
            string studentFirstName = firstName.Text;
            string studentLastName = lastName.Text;
            string studentMatriculation = matriculationNo.Text;
            Trace.WriteLine(grade1.SelectedValue.ToString());
            string studentGrade1 = grade1.SelectionBoxItem.ToString();
            string studentGrade2 = grade2.SelectionBoxItem.ToString();
            string studentGrade3 = grade3.SelectionBoxItem.ToString();
            Trace.WriteLine(studentGrade1);
            Trace.WriteLine(studentGrade2);
            Trace.WriteLine(studentGrade3);
           
            string studentOverallGrade = "A"; //temp  for now..
            Student students = new Student();
            //Input validation will go here..
           

            //Calculate Overall Grade



            //Add fields to database
            students.firstName = studentFirstName;
            students.lastName = studentLastName;
            students.matricNum = studentMatriculation;
            students.gradeOne = "A";
            students.gradeTwo = "B";
            students.gradeThree = "C";
            students.gradeOverall = studentOverallGrade;
           

            context.Students.Add(students);

           

            //Refresh the displayed list
            OnRefresh(sender, e);
            context.SaveChanges();

            MessageBox.Show("Student Successfully Added!");
        }

        //Editing Student Grades
        private void OnEditStudent(object sender, RoutedEventArgs e)
        {

        }
        //Deleting Student details
        private void OnDeleteStudent(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Student student = (Student)button.DataContext;

            var result = MessageBox.Show("Delete Student: " + student.matricNum + "?",  "Confirmation",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                context.Students.Remove(student);
                context.SaveChanges();
                OnRefresh(sender, e);
            }
        }




    }
}
