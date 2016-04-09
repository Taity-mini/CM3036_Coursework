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
            //Trace.WriteLine(grade1.SelectedValue.ToString());
            string studentGrade1 = grade1.SelectionBoxItem.ToString();
            string studentGrade2 = grade2.SelectionBoxItem.ToString();
            string studentGrade3 = grade3.SelectionBoxItem.ToString();
            Trace.WriteLine(studentGrade1);
            Trace.WriteLine(studentGrade2);
            Trace.WriteLine(studentGrade3);

            Functions validation = new Functions();

            string studentOverallGrade = "A"; //temp  for now..
            Student students = new Student();
            bool incomplete = false;
            //Input validation will go here..
            if (validation.isEmpty(studentFirstName) || validation.isEmpty(studentLastName) || validation.isEmpty(studentMatriculation))
            {

                MessageBox.Show("Fields incomplete, try again!");
                incomplete = true;

            }
            else if (incomplete == false)
            {
                //Calculate Overall Grade

                //Add fields to database
                students.firstName = studentFirstName;
                students.lastName = studentLastName;
                students.matricNum = studentMatriculation;
                students.gradeOne = studentGrade1;
                students.gradeTwo = studentGrade2;
                students.gradeThree = studentGrade3;
                students.gradeOverall = studentOverallGrade;


                context.Students.Add(students);

                //Refresh the displayed list
                OnRefresh(sender, e);
                context.SaveChanges();

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

        //Editing Student Grades
        private void OnEditStudent(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            Student student = (Student)button.DataContext;

            EditDialog dialog = new EditDialog(student.firstName, student.lastName, student.matricNum, student.gradeOne, student.gradeTwo, student.gradeThree);

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

                //set updated fields into student record
                student.firstName = firstName;
                student.lastName = lastName;
                student.matricNum = matricNum;
                student.gradeOne = grade1;
                student.gradeTwo = grade2;
                student.gradeThree = grade3;

                //update db with new details
                context.SaveChanges();

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

        }



    }
}
