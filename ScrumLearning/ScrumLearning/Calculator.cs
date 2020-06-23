using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumLearning
{
    public class Calculator
    {
        public static void CalculatorMethod()
        {
            Console.WriteLine("Veuillez entrer votre calcul");
            string text = Console.ReadLine();
            decimal result = CalculateRPN(CalculatorStringParser.Parse(text).ToString());
            Console.WriteLine("------------------------------------");
            Console.WriteLine("{0} = {1}", text, result);
            string q = string.Empty;
            do 
            {
                q = Console.ReadLine();
            } while (q.ToUpper() != "Q");
        }

        static decimal CalculateRPN(string rpn)
        {
            string[] rpnTokens = rpn.Split(' ');
            Stack<decimal> stack = new Stack<decimal>();
            decimal number = decimal.Zero;
            
            foreach (string token in rpnTokens)
            {
                if (decimal.TryParse(token, out number))
                {
                    stack.Push(number);
                }
                else
                {
                    switch (token)
                    {
                        case "^":
                        case "pow":
                            {
                                number = stack.Pop();
                                stack.Push((decimal)Math.Pow((double)stack.Pop(), (double)number));
                                break;
                            }
                        case "ln":
                            {
                                stack.Push((decimal)Math.Log((double)stack.Pop(), Math.E));
                                break;
                            }
                        case "sqrt":
                            {
                                stack.Push((decimal)Math.Sqrt((double)stack.Pop()));
                                break;
                            }
                        case "*":
                            {
                                stack.Push(stack.Pop() * stack.Pop());
                                break;
                            }
                        case "/":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() / number);
                                break;
                            }
                        case "+":
                            {
                                stack.Push(stack.Pop() + stack.Pop());
                                break;
                            }
                        case "-":
                            {
                                number = stack.Pop();
                                stack.Push(stack.Pop() - number);
                                break;
                            }
                        default:
                            Console.WriteLine("Erreur dans la méthode CalculateRPN(string).");
                            break;
                    }
                }
            }
            return stack.Pop();
        }

        static void PrintState(Stack<decimal> stack)
        {
            decimal[] arr = stack.ToArray();

            for (int i = arr.Length - 1; i >= 0; i--)
            {
                Console.Write("{0,-8:F3}", arr[i]);
            }

            Console.WriteLine();
        }
    }

}