using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook
{
    internal class GuestBookLogic
    {
        public static (bool continueRegistration, string registeringUserName) ConfirmRegisteringUserName()
        {
            // Initial variables for name and registration continuation
            var correctNameProvided = false;
            var continueRegistration = true;
            var registeringUserName = "";

            // Loop as long name is not provided correctly and user agrees to try again
            while (correctNameProvided == false && continueRegistration == true)
            {
                ResetAppScreen();
                try
                {
                    registeringUserName = UserInput.PromptRegistrationName();
                    correctNameProvided = true;
                }
                catch (ArgumentException)
                {

                    ResetAppScreen();
                    Console.WriteLine("Name was not provided in correct format");
                    continueRegistration = UserInput.PromptConfirmation("Do you want to try again? [y/n]: ");
                }
                catch
                {
                    ResetAppScreen();
                    Console.WriteLine("Something went wrong...");
                }
            }

            return (continueRegistration, registeringUserName);
        }

        public static (bool continueRegistration, int registrationPartySize) ConfirmRegistrationPartySize()
        {
            // Initial variables for name and registration continuation
            var correctPartySizeProvided = false;
            var continueRegistration = true;
            var registrationPartySize = 0;

            while (correctPartySizeProvided == false && continueRegistration == true)
            {
                ResetAppScreen();
                try
                {
                    registrationPartySize = UserInput.PromptRegistrationPartySize();
                    correctPartySizeProvided = true;
                }
                catch (ArgumentException)
                {
                    ResetAppScreen();
                    Console.WriteLine("Party size was not provided in correct format");
                    continueRegistration = UserInput.PromptConfirmation("Do you want to try again? [y/n]: ");
                }
                
            }

            return (continueRegistration, registrationPartySize);
        }

        public static (bool continueRegistration, int registeringPartySize) ConfirmNotEnoughSeats(int registryRemainingCapacity)
        {
            var continueRegistration = true;
            var registeringPartySize = 0;

            ResetAppScreen();
            Console.WriteLine($"Unfortunately, there are not enough seats left.");
            continueRegistration = UserInput.PromptConfirmation($"Do you want to register for remaining {registryRemainingCapacity} seats? [y/n]: ");

            // If confirmed, lower party size to available amount
            if (continueRegistration == true)
            {
                registeringPartySize = registryRemainingCapacity;
            }

            return (continueRegistration, registeringPartySize);
        }

        public static void FinalizeRegistration(string registeringUserName, int registeringPartySize)
        {
            var continueRegistration = true;

            try
            {
                GuestRegistery.AddRegistration(registeringUserName, registeringPartySize);
            }
            catch (ArgumentException ex) when (ex.Message.Contains("An item with the same key has already been added."))
            {
                ResetAppScreen();
                Console.WriteLine("It looks that you are already registered to the party...");
                continueRegistration = UserInput.PromptConfirmation("Do you want to update your reservation size? [y/n]: ");

                if (continueRegistration == true)
                {
                    try
                    {
                        GuestRegistery.UpdateRegistration(registeringUserName, registeringPartySize);
                    }
                    catch
                    {
                        ResetAppScreen();
                        UserInput.PromptContinue("Something went wrong during the registration!");
                    }
                }
            }
            catch
            {
                ResetAppScreen();
                UserInput.PromptContinue("Something went wrong during the registration!");
            }
        }

        public static void PrintTableSummary(params string[] tableHeaders)
        {
            // List and format all reservations
            var finalRegistrations = new List<string>(); 

            var i = 1;
            foreach (var registration in GuestRegistery.ListAllRegistrations())
            {
                finalRegistrations.Add($"{i},{registration.Key},{registration.Value}");
                i++;
            }

            // Print table data
            GuestBookLogic.ResetAppScreen();
            OutputTable.PrintTable(tableHeaders, finalRegistrations.ToArray());
            Console.WriteLine($"Total: {GuestRegistery.GetRegisteredCapacity()}");
            Console.ReadKey();
        }

        public static bool BlockRegistration(string message)
        {
            var continueRegistration = false;

            ResetAppScreen();
            Console.WriteLine("Unfortunately, there are no seats left for registration...");

            return continueRegistration;
        }

        public static void ResetAppScreen()
        {
            Console.Clear();
            Console.WriteLine("***********************************");
            Console.WriteLine("*                                 *");
            Console.WriteLine("*                                 *");
            Console.WriteLine("*           Guest Book            *");
            Console.WriteLine("*                                 *");
            Console.WriteLine("*                                 *");
            Console.WriteLine("***********************************");

            Console.WriteLine();
        }
    }
}
