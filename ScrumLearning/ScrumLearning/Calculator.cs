using System;
using System.Collections.Generic;

namespace ScrumLearning
{
    public class Calculator
    {
        public static void CalculatorMethod()
        {
            Console.WriteLine("Veuillez entrer votre calcul");
            string text = string.Empty;
            bool isOk = false;
            decimal result = decimal.Zero;
            do
            {
                try
                {
                    text = Console.ReadLine();
                    result = CalculateRPN(CalculatorStringParser.Parse(text).ToString());
                    isOk = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Cette opération n'est pas valide.");
                }
            } while (!isOk);
            Console.WriteLine("------------------------------------");
            Console.WriteLine("{0} = {1}", text, result);
            string q = string.Empty;
            do
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Entrez Q ou q pour quitter le module.");
                q = Console.ReadLine();
            } while (q.ToUpper() != "Q");
        }

        private static decimal CalculateRPN(string rpn)
        {
            string[] rpnTokens = rpn.Split(' ');
            Stack<decimal> stack = new Stack<decimal>();
            foreach (string token in rpnTokens)
            {
                if (decimal.TryParse(token, out decimal number))
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
    }
}