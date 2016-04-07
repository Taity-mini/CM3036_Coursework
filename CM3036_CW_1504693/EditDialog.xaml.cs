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
            //Grades 1-3
            //Grade1.SelectedValue = grade1;
            //Grade2.SelectedValue = grade2;
            //Grade1.SelectedItem = grade1;
            //Grade1.SelectedItem = grade1;
            //Grade2.SelectedIndex = Grade2.Items.IndexOf(grade2);
            //Grade3.SelectedIndex = Grade3.Items.IndexOf(grade3);
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
            DialogResult = true;
            Close();
        }
    }
}
