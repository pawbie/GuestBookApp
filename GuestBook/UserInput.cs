using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GuestBook
{
    internal class UserInput
    {
        public static string PromptRegistrationName()
        {
            string userInput;

            Console.Write("Please provide your name: ");
            userInput = Console.ReadLine();

            // Validate user input
            bool isEmpty = string.IsNullOrEmpty(userInput);
            bool wordsAboveLimit = userInput.Split(' ').Length >= 3;
            bool unallowedCharacters = !(new Regex("^[a-zA-Z]+$")).IsMatch(String.Concat(userInput.Where(c => !Char.IsWhiteSpace(c))));

            if (isEmpty || wordsAboveLimit || unallowedCharacters)
            {
                throw new ArgumentException("Name was not provided in correct format");
            }

            return userInput;
        }
        public static int PromptRegistrationPartySize()
        {
            bool correctNameProvided;
            string userInput;
            int partySize = 0;

            Console.Write("Please provide number of people in your party: ");
            userInput = Console.ReadLine();

            // Validate user input
            bool isEmpty = string.IsNullOrEmpty(userInput);
            bool wordsAboveLimit = userInput.Split(' ').Length >= 2;
            bool invalidInt = !int.TryParse(userInput, out partySize);
            bool invalidPartySize = partySize <= 0;

            if (isEmpty || wordsAboveLimit || invalidInt || invalidPartySize)
            {
                throw new ArgumentException("Provided party size was not in supported format.");
            }

            return partySize;
        }

        public static bool PromptConfirmation(string message)
        {
            ConsoleKey? readKey = null;
            while (readKey != ConsoleKey.N && readKey != ConsoleKey.Y)
            {
                Console.Write(message);
                readKey = Console.ReadKey().Key;

                Console.WriteLine();
            }

            return readKey == ConsoleKey.Y;
        }

        public static void PromptContinue(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press ANY KEY to continue...");
            Console.ReadKey();
        }


    }
}
