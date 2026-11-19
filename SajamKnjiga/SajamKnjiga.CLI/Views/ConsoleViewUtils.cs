using System.Globalization;
using SajamKnjiga.Core.Model;

namespace SajamKnjiga.CLI.Views
{
    //Po trećim vežbama SafeInput* trazi unos dok nije ispravan, a Input* ispise poruku pa pozove SafeInput*.
// Znak '|' nije dozvoljen u tekstu jer je separator u CSV fajlovima.
static class ConsoleViewUtils
    {
        public static int SafeInputInt()
        {
            int input;
            string rawInput = System.Console.ReadLine() ?? string.Empty;
            while (!int.TryParse(rawInput, out input))
            {
                System.Console.WriteLine("Invalid input. Please enter a valid integer.");
                rawInput = System.Console.ReadLine() ?? string.Empty;
            }
            return input;
        }

        public static decimal SafeInputDecimal()
        {
            decimal input;
            string rawInput = System.Console.ReadLine() ?? string.Empty;
            while (!decimal.TryParse(rawInput, out input))
            {
                System.Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                rawInput = System.Console.ReadLine() ?? string.Empty;
            }
            return input;
        }
// u CSV se upise kao yyyy-MM-dd, a korisnik unosi dd.MM.yyyy.
        public static DateTime SafeInputDate()
        {
            DateTime input;
            string rawInput = System.Console.ReadLine() ?? string.Empty;
            while (!DateTime.TryParseExact(rawInput, "dd.MM.yyyy.", CultureInfo.InvariantCulture, DateTimeStyles.None, out input))
            {
                System.Console.WriteLine("Invalid input. Please enter a valid date in the format 'dd.MM.yyyy.'.");
                rawInput = System.Console.ReadLine() ?? string.Empty;
            }
            return input;
        }

        public static string SafeInputString()
        {
            string input = System.Console.ReadLine() ?? string.Empty;
            while (input.Contains('|'))
            {
                System.Console.WriteLine("Invalid input. Please enter a string without the '|' character.");
                input = System.Console.ReadLine() ?? string.Empty;
            }
            return input;
        }
        public static string InputString(string poruka)
        {
            System.Console.WriteLine(poruka);
            return SafeInputString();
        }

        public static int InputInt(string poruka)
        {
            System.Console.WriteLine(poruka);
            return SafeInputInt();
        }

        public static decimal InputDecimal(string poruka)
        {
            System.Console.WriteLine(poruka);
            return SafeInputDecimal();
        }

        public static DateTime InputDate(string poruka)
        {
            System.Console.WriteLine(poruka + " (format: dd.MM.yyyy.)");
            return SafeInputDate();
        }

        public static Adresa InputAdresa()
        {
            string ulica = InputString("Unesite ulicu:");
            string broj = InputString("Unesite broj:");
            string grad = InputString("Unesite grad:");
            string drzava = InputString("Unesite drzavu:");


            return new Adresa(ulica, broj, grad, drzava);
        }
    }
}