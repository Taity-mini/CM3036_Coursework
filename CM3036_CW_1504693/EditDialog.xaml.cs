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
using System.Diagnostics;
using System.Runtime.Remoting.Contexts;


namespace CM3036_CW_1504693
{
    /// <summary>
    /// Interaction logic for EditDialog.xaml
    /// Editing Individual Students Window Form
    /// Coursework for CM3036 | Programming in C#
    /// Author: Andrew Tait (1504693)
    /// </summary>
    /// 
    public partial class EditDialog : Window
    {
        private DBStudent context;
        private string initalMatriculation;
     
        public EditDialog(DBStudent context,string FirstName, string LastName, string Matriculation, string grade1, string grade2, string grade3)
        {
            InitializeComponent();
            //Set editing fields as current student details
            firstName.Text = FirstName;
            lastName.Text = LastName;
            matriculation.Text = Matriculation;
            this.initalMatriculation = Matriculation;
            
            //Set combobox to correct grade
            checkGrade(grade1, Grade1);
            checkGrade(grade2, Grade2);
            checkGrade(grade3, Grade3);
        }
        
        //Check grade string and set combobox selected index accordingly
        public void checkGrade(string grade, ComboBox box)
        {
            switch (grade)
            {
                //Non submission
                case "":
                    box.SelectedIndex = 0;
                    break;

                case "A":
                    box.SelectedIndex = 1;
                    break;

                case "B":
                    box.SelectedIndex = 2;
                    break;

                case "C":
                    box.SelectedIndex = 3;
                    break;

                case "D":
                    box.SelectedIndex = 4;
                    break;

                case "E":
                    box.SelectedIndex = 5;
                    break;

                case "F":
                    box.SelectedIndex = 6;
                    break;
            }
        }

        private void onSubmit(object sender, RoutedEventArgs e)
        {
            //Local Variables
            Functions validation = new Functions();
            context = new DBStudent();
          
            string studentFirstName = firstName.Text;
            string studentLastName = lastName.Text;
            string studentMatriculation = matriculation.Text;

            //Input validation
            bool incomplete = false;
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
            if (!validation.validMatriculation(studentMatriculation))
            {
                MessageBox.Show("Student Matriculation can only be 7 numbers in length");
                incomplete = true;
            }
            else if (incomplete == false)
            {
                bool MatriculationNoChange = studentMatriculation.Equals(this.initalMatriculation);
                //check if the Matriculation has changed or not

                bool userExists = validation.userExists(context, studentMatriculation);
                if (userExists && !MatriculationNoChange)
                {
                    MessageBox.Show("User Already Exists, try a different Matriculation Number ");
                }
                else if (!userExists || MatriculationNoChange)
                {
                    DialogResult = true;
                    Close();
                }
            }
        }
    }
}