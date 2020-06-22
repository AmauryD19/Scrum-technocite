using System;
using System.Data;

namespace ScrumLearning
{
    internal class Calculator
    {
        public static void CalculatorMethod()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Write your calculation");
            string tempMath = Console.ReadLine();
            if (tempMath.Contains("."))
            {
                Console.WriteLine("Decimal numbers must use a \",\"");
            }
            else
            {
                string finalMath = tempMath.Replace(",", ".");

                if (finalMath.Contains("^"))
                {
                    //string baseNumber = finalMath.Substring(0, finalMath.IndexOf('^'));
                    //double baseNum = double.Parse(baseNumber);
                    //double power = double.Parse(finalMath.Substring(finalMath.IndexOf('^'), finalMath.Length - 1));

                    //double pow = Math.Pow(baseNumber, power);
                    //Console.WriteLine(pow);
                    Console.WriteLine(finalMath.IndexOf('^'));
                }
                else if (finalMath.Contains("Cos"))
                {
                    Console.WriteLine("Cos");
                }
                else if (finalMath.Contains("Sin"))
                {
                    Console.WriteLine("Sin");
                }
                else
                {
                    string value = new DataTable().Compute(finalMath, null).ToString();
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0} = {1}", tempMath, value);
                }
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
        }
    }
}