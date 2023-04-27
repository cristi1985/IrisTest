// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace IrisTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            char chr;
            bool val;
            Console.WriteLine("Please press 1 to input column index or 2 to input column letter");
            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("Input column number");
                int columnNum;
                bool isNumber = int.TryParse(Console.ReadLine(),out columnNum);
                //check column number is bigger than 0 and has no more than three digits
                //Math.Floor  method is fast as long as the number of digits is under 5 and reliable as long as we don't pass negative digits
                if (isNumber && columnNum>0 && Math.Floor(Math.Log10(columnNum)+1)<=3) 
                {
                    Console.WriteLine("Column letter is {0}", ReturnColumnString(columnNum));
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Please input only positive numbers of a maximum of three digits");
                    Console.ReadKey();
                }
            }
            else if(input == "2") 
            {
                Console.WriteLine("Input column letters");
                string letters = Console.ReadLine();
                string pattern = @"^[A-Za-z]+$";
                Regex regex = new Regex(pattern);
                bool isMatch = Regex.IsMatch(letters, pattern);//check if input contain any illegal characters numbers or special chars
                bool isProperLength = letters.Length > 0 && letters.Length <= 3; //check that the input has the correct length which is a maximum of three characters
                if (!isMatch && !isProperLength)
                {
                    Console.WriteLine("Input does not contain only letters");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Column number is {0}", ReturnColumnNumber(letters.ToUpperInvariant()));
                    Console.ReadKey();
                }
            }
        }
        public static int ReturnColumnNumber(string columnName)
        {
            int[] noDigits = new int[columnName.Length];
            for (int i = 0; i <columnName.Length; i++)
            {
                noDigits[i] = Convert.ToInt32(columnName[i]) -64; //get index in alphabet from ascii code
            }
            int multiplier = 1;
            int result = 0;
            for (int position = noDigits.Length - 1; position >= 0; position--)
            {
                result += noDigits[position] * multiplier;

                multiplier *= 26; //the number of letters in the alphabet
            }
            return result;
        }

        public static string ReturnColumnString(int columnIndex)
        {
            if (columnIndex < 0)
            {
                throw new ArgumentOutOfRangeException("Column number cannot be negative");
            }

            string colString = "";
            decimal colNumber = columnIndex;

            while(colNumber > 0)
            {
                decimal currentLetterIndex = (colNumber - 1) % 26;
                char currentLetter = (char)(currentLetterIndex + 65);
                colString = currentLetter + colString;
                colNumber = (colNumber - (currentLetterIndex + 1)) / 26;
            }
            return colString;
        }


     
    }
}

