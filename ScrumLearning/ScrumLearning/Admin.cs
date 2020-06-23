using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ScrumLearning
{
    public class Admin
    {
        public static bool isConnected = false;

        public static void CreateAdmin()
        {
        Start:
            Console.Clear();
            Console.WriteLine("-----------------------");
            Console.WriteLine("Veuillez entrer un mot de passe, composé d'au minimum 16 caractères, ayant au moins une lettre minuscule, \n une lettre majuscule, un chiffre et un caractère spécial.");
            string pwd = string.Empty, err = string.Empty;
            do
            {
                Console.WriteLine("-------------------");
                if (err != string.Empty) Console.WriteLine(err);
                pwd = Console.ReadLine();
            } while (!ValidatePassword(pwd, out err));

            Console.Clear();

            Console.WriteLine("Confirm your the previous password.");
            string confirm = Console.ReadLine();
            if (confirm != pwd)
            {
                Console.WriteLine("La confirmation est incorrecte. Veuillez recommencer depuis le début. Appuyez sur n'importe quelle touche pour continuer.");
                Console.ReadKey();
                goto Start;
            }
            Console.WriteLine("----------------------------");
            Console.WriteLine("Le mot de passe a bien été confirmé. Cryptage des données...");

            string hashed = ComputeSha256Hash(pwd);

            if (File.Exists("admin.txt")) // If file exists, delete it
            {
                File.Delete("admin.txt");
            }
            StreamWriter sw = File.CreateText("admin.txt");
            sw.Close();
            File.WriteAllText("admin.txt", hashed);
            Console.WriteLine("Le mot de passe a été crypté et enregistré. Vous êtes à présent connecté.");
            isConnected = true;
            Console.ReadKey();
        }

        public static string MenuAdmin()
        {
            string adminMenu = string.Empty;
            if (Admin.isConnected)
            {
                adminMenu = Constants.LOG_OUT_ADMIN_ITEM;
            }
            else if (File.Exists("admin.txt"))
            {
                adminMenu = Constants.SIGN_IN_ADMIN_ITEM;
            }
            else
            {
                adminMenu = Constants.SIGN_UP_ADMIN_ITEM;
            }
            return adminMenu;
        }

        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static void LogOut()
        {
            isConnected = false;
        }

        public static void LogIn()
        {
            if (File.Exists("admin.txt"))
            {
                Console.Clear();
                string hashed = File.ReadAllText("admin.txt");
                Console.WriteLine("Veuillez entrer votre mot de passe :");
                int count = 0;
                string pwd = "";
                bool isOk = false;
                while (hashed != ComputeSha256Hash(pwd))
                {
                    pwd = Console.ReadLine();

                    if (hashed != ComputeSha256Hash(pwd))
                    {
                        count++;
                    }
                    else
                    {
                        isOk = true;
                    }
                    if (count == 3)
                    {
                        isOk = false;
                        break;
                    }
                }

                if (!isOk)
                {
                    Console.WriteLine("Vous avez effectué trop d'erreur. Retour au menu principal...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Vous êtes à présent connecté. Retour au menu principal...");
                    isConnected = true;
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Le mot de passe n'a pas été enregistré. Retour au menu principal.");
                Console.ReadKey();
            }
        }

        public static bool ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Le mot de passe n'est pas valide.");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{16,}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit avoir au moins une lettre minuscule.";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit avoir au moins une lettre majuscule.";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins 16 caractères.";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins un chiffre.";
                return false;
            }
            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "Le mot de passe doit contenir au moins un caractère spécial.";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}