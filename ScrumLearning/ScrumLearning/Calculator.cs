using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumLearning
{
    class Calculator
    {
        public static void CalculatorMethod()
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Write your calculation");
            string tempMath = Console.ReadLine();
            //string value = new DataTable().Compute(math, null).ToString();
            if (tempMath.Contains("."))
            {
                Console.WriteLine("Decimal numbers must use a \",\"");
            }
            else
            {
                string finalMath = tempMath.Replace(",", ".");

                if (finalMath.Contains("^"))
                {
                    string value = new DataTable().Compute(finalMath, null).ToString();
                    Console.WriteLine("{0} = {1}", tempMath, value);

                }
                else
                {
                    string value = new DataTable().Compute(finalMath, null).ToString();
                    Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("{0} = {1}", tempMath, value);
                }
            }


            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            //double pow_t = Math.Pow((10 * 2), 2); 
            //double pow_tt = Math.Pow(20,2);
            //Console.WriteLine(pow_t);
            //Console.WriteLine(pow_tt);
            //Console.WriteLine("{0} = {1}", math, value);
        }

    }
}
