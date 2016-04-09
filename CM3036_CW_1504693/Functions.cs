using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

//Functions class for various functions within the program

namespace CM3036_CW_1504693
{
    class Functions
    {
        //Form Validation
        public Boolean isEmpty(string field)
        {
            if (string.IsNullOrWhiteSpace(field) || field.Length == 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //Main Grading Functions
        public string calculateGrade(string grade1, string grade2, string grade3)
        {
            //local variables
            string overallGrade = "";
            int gradeA = 0;
            int gradeB = 0;
            int gradeC = 0;
            int gradeD = 0;
            int gradeE = 0;
            bool gradeF = false;

            //Component one - Weighting x3
            switch (grade1)
            {
                case "A":
                    gradeA += 3;
                    break;

                case "B":
                    gradeB += 3;
                    break;

                case "C":
                    gradeC += 3;
                    break;

                case "D":
                    gradeD += 3;
                    break;

                case "E":
                    gradeE += 3;
                    break;

                case "F":
                    gradeF = true;
                    break;
            }

            //Component two - Weighting x5
            switch (grade2)
            {
                case "A":
                    gradeA += 5;
                    break;

                case "B":
                    gradeB += 5;
                    break;

                case "C":
                    gradeC += 5;
                    break;

                case "D":
                    gradeD += 5;
                    break;

                case "E":
                    gradeE += 5;
                    break;

                case "F":
                    gradeF = true;
                    break;
            }

            //Component three - Weighting x2
            switch (grade3)
            {
                case "A":
                    gradeA += 2;
                    break;

                case "B":
                    gradeB += 2;
                    break;

                case "C":
                    gradeC += 2;
                    break;

                case "D":
                    gradeD += 2;
                    break;

                case "E":
                    gradeE += 2;
                    break;

                case "F":
                    gradeF = true;
                    break;
            }

            //Calculate overall grade on each grade weighting

            //check if any grade F occur first

            if (gradeF)
            {
                overallGrade = "F";
            }
            //Otherwise loop through Grade A to E
            else if (!gradeF)
            {
                //Grade A
                if (gradeA >= 5 || gradeB >= 7 || gradeC == 10)
                {
                    overallGrade = "A";
                }
                //Grade B
                else if (gradeB >= 5 || gradeC >= 7 || gradeD == 10)
                {
                    overallGrade = "B";
                }
                //Grade C
                else if (gradeC >= 5 || gradeD >= 7)
                {
                    overallGrade = "C";
                }
                // Grade D
                else if (gradeD >= 5 || gradeE >= 7)
                {
                    overallGrade = "D";
                }
                //Grade E
                else if (gradeE >= 7)
                {
                    overallGrade = "E";
                }
            }

            return overallGrade;
        }
    }
}
