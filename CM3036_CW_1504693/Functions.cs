﻿using System;
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

            //First check for any non-submissions
            //bool nonSubmissions = false;

            //if (grade1 == " " || grade2 == " " || grade3 == " ")
            //{
            //    nonSubmissions = true;
            //    overallGrade = "F";
            //}
            //else if (!nonSubmissions)
            //{
                //component values
                int com1 = 0;
                int com2 = 0;
                int com3 = 0;

                //A = 5
                //B = 4
                //C = 3
                //D = 2
                //E = 1
                //F = 0


                //Component one - Weighting x3
                switch (grade1)
                {
                    case "A":
                        com1 = 5;
                        break;

                    case "B":
                        com1 = 4;
                        break;

                    case "C":
                        com1 = 3;
                        break;

                    case "D":
                        com1 = 2;
                        break;

                    case "E":
                        com1 = 1;
                        break;

                    case "F":
                    case " ":
                        com1 = 0;
                        break;
                }

                //Component two - Weighting x5
                switch (grade2)
                {
                    case "A":
                        com2 = 5;
                        break;

                    case "B":
                        com2 = 4;
                        break;

                    case "C":
                        com2 = 3;
                        break;

                    case "D":
                        com2 = 2;
                        break;

                    case "E":
                        com2 = 1;
                        break;

                    case "F":
                    case " ":
                        com2 = 0;
                        break;
                }

                //Component three - Weighting x2
                switch (grade3)
                {
                    case "A":
                        com3 = 5;
                        break;

                    case "B":
                        com3 = 4;
                        break;

                    case "C":
                        com3 = 3;
                        break;

                    case "D":
                        com3 = 2;
                        break;

                    case "E":
                        com3 = 1;
                        break;

                    case "F":
                    case " ":
                        com3 = 0;
                        break;
                }


                //Calculate overall grade on each grade weighting
                // Loop through Grade A to E 
                //Grade A
                //A = 5
                //B = 4
                //C = 3


                //Condition 1                                 //Condition 2                                         //Condition 3                                              
                if ((com2 == 5 || (com1 == 5 && com3 == 5)) && ((com2 >= 4 && com3 >= 4) || (com2 >= 4 && com1 >= 4)) && ((com1 >= 3 && com2 >= 3) && (com3 >= 3)))
                {
                    overallGrade = "A";
                }
                //Grade B

                //B = 4
                //C = 3
                //D = 2

                        //Condition 1                               //Condition 2                                              //Condition 3      
                else if ((com2 == 4 || (com1 == 4 && com3 == 4)) && ((com2 >= 3 && com3 >= 3) || (com2 >= 3 && com1 >= 3)) && ((com1 >= 2 && com2 >= 2) && (com3 >= 2)))
                {
                    overallGrade = "B";
                }

                //Grade C
                //C = 3
                //D = 2

                        //Condition 1                                 //Condition 2                        
                else if ((com2 == 3 || (com1 == 3 && com3 == 3)) && ((com2 >= 2 && com3 >= 2) || (com2 >= 2 && com1 >= 2)))
                {
                    overallGrade = "C";
                }

                 //Grade D
                //D = 2
                //E = 1

                         //Condition 1                               //Condition 2 
                else if ((com2 == 2 || (com1 == 2 && com3 == 2)) && ((com2 >= 1 && com3 >= 1) || (com2 >= 1 && com1 >= 1)))
                {
                    overallGrade = "D";
                }

                //Grade E
                //E = 1
                //Condition 1
                else if ((com2 >= 1 && com3 >= 1) || (com2 >= 1 && com1 >= 1))
                {
                    overallGrade = "E";
                }

                //Grade F
                //Condition 1

                else
                {
                    overallGrade = "F";
                }
           // }
            return overallGrade;
        }
    }
}
