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
using System.Diagnostics;


namespace CM3036_CW_1504693
{
    /// <summary>
    /// Interaction logic for EditDialog.xaml
    /// </summary>
    public partial class EditDialog : Window
    {
        public EditDialog(string FirstName, string LastName, string Matriculation, string grade1, string grade2, string grade3)
        {
            InitializeComponent();

            //Set editing fields as current student details
            firstName.Text = FirstName;
            lastName.Text = LastName;
            matriculation.Text = Matriculation;

            Trace.WriteLine(grade1);
            checkGrade(grade1, Grade1);
            checkGrade(grade2, Grade2);
            checkGrade(grade3, Grade3);
        }

        public void checkGrade(string grade, ComboBox box)
        {
            switch (grade)
            {
                case "A":
                    box.SelectedIndex = 0;
                    break;

                case "B":
                    box.SelectedIndex = 1;
                    break;

                case "C":
                    box.SelectedIndex = 2;
                    break;

                case "D":
                    box.SelectedIndex = 3;
                    break;

                case "E":
                    box.SelectedIndex = 4;
                    break;

                case "F":
                    box.SelectedIndex = 5;
                    break;
            }
        }

        private void onSubmit(object sender, RoutedEventArgs e)
        {
            //Local Variables
            Functions validation = new Functions();
            string studentFirstName = firstName.Text;
            string studentLastName = lastName.Text;
            string studentMatriculation = matriculation.Text;

            bool incomplete = false;
            //Input validation
            if (validation.isEmpty(studentFirstName) || validation.isEmpty(studentLastName) || validation.isEmpty(studentMatriculation))
            {

                MessageBox.Show("Fields incomplete, try again!");
                incomplete = true;

            }
            else if (incomplete == false)
            {
                DialogResult = true;
                Close();
            }
        }
    }
}
