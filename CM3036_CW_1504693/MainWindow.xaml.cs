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

        }

        private void OnRefresh(object sender, RoutedEventArgs e)
        {
            var query = from student in context.Students select student;
            // convert set of Appointment objects to list of Appointment objects
            Students = query.ToList();
            // connect list of Appointment entities to collection view
            StudentsViewSource.Source = Students;

            //Calculate % of students that have passed
            calculatePass();

        }


        //Adding a student's grades to the database
        private void onAddStudent(object sender, RoutedEventArgs e)
        {

            //Field variables
            string studentFirstName = firstName.Text;
            string studentLastName = lastName.Text;
            string studentMatriculation = matriculationNo.Text;

            string studentGrade1 = grade1.SelectionBoxItem.ToString();
            string studentGrade2 = grade2.SelectionBoxItem.ToString();
            string studentGrade3 = grade3.SelectionBoxItem.ToString();
            Trace.WriteLine(studentGrade1);
            Trace.WriteLine(studentGrade2);
            Trace.WriteLine(studentGrade3);

            Functions validation = new Functions();

            Student students = new Student();
            bool incomplete = false;

            //Input validation will go here:

            //First check if all fields are not empty
            if (validation.isEmpty(studentFirstName) || validation.isEmpty(studentLastName) || validation.isEmpty(studentMatriculation))
            {
                MessageBox.Show("Fields incomplete, try again!");
                incomplete = true;
            }

            //Then check if student names only contain letters/whitespace
            if (!validation.onlyLetters(studentFirstName) && !validation.onlyLetters(studentLastName))
            {
                MessageBox.Show("Student Name Fields can only contain letters or whitespace");
                incomplete = true;
            }

            //Lastly validate student matriculation number only contains numbers and is 7 numbers in length
            if(!validation.validMatriculation(studentMatriculation))
            {
                MessageBox.Show("Student Matriculation can only be 7 numbers in length");
                incomplete = true;
            }

            else if (incomplete == false)
            {
             
                bool userExists = validation.userExists(context, studentMatriculation);
                if (userExists)
                {
                    MessageBox.Show("User Already Exists, try a different Matriculation Number ");
                }
                else if (userExists == false)
                {
                    //Calculate Overall Grade
                    string studentOverallGrade = validation.calculateGrade(studentGrade1, studentGrade2, studentGrade3);

                    //Add fields to database
                    students.firstName = studentFirstName;
                    students.lastName = studentLastName;
                    students.matricNum = studentMatriculation;
                    students.gradeOne = studentGrade1;
                    students.gradeTwo = studentGrade2;
                    students.gradeThree = studentGrade3;
                    students.gradeOverall = studentOverallGrade;

                    //Refresh the displayed list
                    context.Students.Add(students);
                    context.SaveChanges();
                    OnRefresh(sender, e);

                    //Clear fields to default
                    firstName.Clear();
                    lastName.Clear();
                    matriculationNo.Clear();
                    grade1.SelectedIndex = 0;
                    grade2.SelectedIndex = 0;
                    grade3.SelectedIndex = 0;
                    MessageBox.Show("Student Successfully Added!");
                }

            }
        }

        //Editing Student Grades
        private void OnEditStudent(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Student student = (Student)button.DataContext;

            EditDialog dialog = new EditDialog(context, student.firstName, student.lastName, student.matricNum, student.gradeOne, student.gradeTwo, student.gradeThree);
            Functions grades = new Functions();
            Nullable<bool> result = dialog.ShowDialog();
            if (dialog.DialogResult == true)
            {
                //Get updated fields from edit dialog
                string firstName = dialog.firstName.Text;
                string lastName = dialog.lastName.Text;
                string matricNum = dialog.matriculation.Text;
                string grade1 = dialog.Grade1.SelectionBoxItem.ToString();
                string grade2 = dialog.Grade2.SelectionBoxItem.ToString();
                string grade3 = dialog.Grade3.SelectionBoxItem.ToString();
                string studentOverallGrade = grades.calculateGrade(grade1, grade2, grade3);

                //set updated fields into student record
                student.firstName = firstName;
                student.lastName = lastName;
                student.matricNum = matricNum;
                student.gradeOne = grade1;
                student.gradeTwo = grade2;
                student.gradeThree = grade3;
                student.gradeOverall = studentOverallGrade;

                //update db with new details
                context.SaveChanges();

                //Confirm student has updated
                MessageBox.Show("Student Updated");

                //Refresh list on application
                OnRefresh(sender, e);
            }
        }

        //Deleting Student details
        private void OnDeleteStudent(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Student student = (Student)button.DataContext;

            var result = MessageBox.Show("Delete Student: " + student.matricNum + "?", "Confirmation",
                MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                context.Students.Remove(student);
                context.SaveChanges();
                OnRefresh(sender, e);
            }
        }


        //Delete all student records
        private void onDeleteAllStudents(object sender, RoutedEventArgs e)
        {
            //Check if database is not empty
            if (Students.Count > 0)
            {
                var result = MessageBox.Show("Delete all students (irreversible)?", "Confirmation", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    //Delete all Students
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Students]");
                    context.SaveChanges();
                    OnRefresh(sender, e);
                }
            }
            else //Otherwise display error message
            {
                MessageBox.Show("Can't delete from an empty database!");
            }
        }

        //Calculate students have passed and those have failed (E/F)
        private void calculatePass()
        {
            double StudentPasses = 0;
            double studentTotal = context.Students.Count(); // Total Number of Students
            double finalPassRate;

            //Loop through all students
            foreach (Student student in context.Students)
            {
                if (student.gradeOverall != "E" && student.gradeOverall != "F")
                {
                    StudentPasses++;
                }
            }
            //Check if there any students
            if (studentTotal > 0)
            {   
                //calculate passRate with total # of passes with total # of students
                finalPassRate = (StudentPasses / studentTotal) * 100;
                //set pass rate to label
                passRate.Content = String.Format("{0:0.0#}", finalPassRate) + "%";
            }
            else // No students? Then display 0.0%
            {
                passRate.Content = 0.0 + "%";
            }
        }

    }
}
