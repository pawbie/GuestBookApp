using GuestBook;
using System.Text.RegularExpressions;

#region Variables
// Used to track for manual exit from registration phase
bool moreGuestsComing;
bool continueRegistration;

// Basic data for new registration
string registeringUserName;
int registeringPartySize;

// Guest registry capacity
int registryRemainingCapacity;
#endregion

#region Main
do
{
    // Reset continuation / exist variables
    moreGuestsComing = true;
    continueRegistration = true;

    // Reset registering info variables
    registeringUserName = String.Empty;
    registeringPartySize = 0;

    // Get capacity data
    registryRemainingCapacity = GuestRegistery.GetCapacity();

    // Block registration if no capacity is left
    if (registryRemainingCapacity <= 0)
    {
        UserInput.PromptContinue("Unfortunately, there are no seats left for registration...");  
    }
    else
    {
        // Get name from the user
        if (continueRegistration == true)
        {
            (continueRegistration, registeringUserName) = GuestBookLogic.ConfirmRegisteringUserName();
        }

        // Get party size from the user
        if (continueRegistration == true)
        {
            (continueRegistration, registeringPartySize) = GuestBookLogic.ConfirmRegistrationPartySize();
        }

        // Check if registration capacity allows declared party size
        if (registryRemainingCapacity < registeringPartySize && continueRegistration == true)
        {
            (continueRegistration, registeringPartySize) = GuestBookLogic.ConfirmNotEnoughSeats(registryRemainingCapacity);
        }

        // Register if new / update if existing
        if (continueRegistration == true)
        {
            GuestBookLogic.FinalizeRegistration(registeringUserName, registeringPartySize);
        }
    }

    // Final check after each registration to validate if should be launched again
    moreGuestsComing = UserInput.PromptConfirmation("Are there more guests coming after you? [y/n]: ");

} while (moreGuestsComing);

GuestBookLogic.PrintTableSummary("No.", "Name", "Party Size");
#endregion