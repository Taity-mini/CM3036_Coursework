using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

//Functions class for various functions within the program

namespace CM3036_CW_1504693
{
    class Functions
    {
        //Form Validation 

        //Checks for if a string is empty or contains only whitespace
        public Boolean isEmpty(string field)
        {
            //Is field empty then true
            if (string.IsNullOrWhiteSpace(field) || field.Length == 0)
            {
                return true;
            }
            //Not empty? return false
            else
            {
                return false;
            }
        }

        //check if a string (field) contains only letters and white space
        public Boolean onlyLetters(string field)
        {
            foreach (char c in field)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        //checks if the matriculation number field is valid
        // Only Numerical values
        // Must be in 6 numbers in length
        public Boolean validMatriculation(string field)
        {
            if (field.Length != 7)
            {
                return false;
            }
            else
            {
                foreach (char c in field)
                {
                    if (!Char.IsDigit(c))
                    {
                        return false;
                    }
                }
                return true;
            }
        }


        //Check if user exists in db based on matriculation Numbers

        public Boolean userExists(DBStudent context, string matriculation)
        {
            var existingStudentCount = context.Students.Count(a => a.matricNum == matriculation);
            if (existingStudentCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        //Main Grading Functions
        public string calculateGrade(string grade1, string grade2, string grade3)
        {
            //local variables
            string overallGrade = "";

            //Grade counters
            int gradeA = 0;
            int gradeB = 0;
            int gradeC = 0;
            int gradeD = 0;
            int gradeE = 0;

            //First check for any non-submissions
            bool nonSubmissions = false;

            if (grade1 == " " || grade2 == " " || grade3 == " ")
            {
                nonSubmissions = true;
                overallGrade = "F";
            }
            else if (!nonSubmissions)
            {

                //Component one - Weighting x3
                switch (grade1)
                {
                    case "A":
                        gradeA += 3;
                        gradeB += 3;
                        gradeC += 3;
                        gradeD += 3;
                        gradeE += 3;
                        break;

                    case "B":
                        gradeB += 3;
                        gradeC += 3;
                        gradeD += 3;
                        gradeE += 3;
                        break;

                    case "C":
                        gradeC += 3;
                        gradeD += 3;
                        gradeE += 3;
                        break;

                    case "D":
                        gradeD += 3;
                        gradeE += 3;
                        break;

                    case "E":
                        gradeE += 3;
                        break;

                }

                //Component two - Weighting x5
                switch (grade2)
                {
                    case "A":
                        gradeA += 5;
                        gradeB += 5;
                        gradeC += 5;
                        gradeD += 5;
                        gradeE += 5;
                        break;

                    case "B":
                        gradeB += 5;
                        gradeC += 5;
                        gradeD += 5;
                        gradeE += 5;
                        break;

                    case "C":
                        gradeC += 5;
                        gradeD += 5;
                        gradeE += 5;
                        break;

                    case "D":
                        gradeD += 5;
                        gradeE += 5;
                        break;

                    case "E":
                        gradeE += 5;
                        break;
                }

                //Component three - Weighting x2
                switch (grade3)
                {
                    case "A":
                        gradeA += 2;
                        gradeB += 2;
                        gradeC += 2;
                        gradeD += 2;
                        gradeE += 2;
                        break;

                    case "B":
                        gradeB += 2;
                        gradeC += 2;
                        gradeD += 2;
                        gradeE += 2;
                        break;

                    case "C":
                        gradeC += 2;
                        gradeD += 2;
                        gradeE += 2;
                        break;

                    case "D":
                        gradeD += 2;
                        gradeE += 2;
                        break;

                    case "E":
                        gradeE += 2;
                        break;

                }


                //Calculate overall grade on each grade weighting
                //Loop through Grade A to F



                //Grade A
                if (gradeA >= 5 && gradeB >= 7 && gradeC == 10)
                {
                    overallGrade = "A";
                }
                //Grade B
                else if (gradeB >= 5 && gradeC >= 7 && gradeD == 10)
                {
                    overallGrade = "B";
                }
                //Grade C
                else if (gradeC >= 5 && gradeD >= 7)
                {
                    overallGrade = "C";
                }
                // Grade D
                else if (gradeD >= 5 && gradeE >= 7)
                {
                    overallGrade = "D";
                }
                //Grade E
                else if (gradeE >= 7)
                {
                    overallGrade = "E";
                }
                else
                {
                    overallGrade = "F";
                }
            }
            return overallGrade;
        }
    }
}
