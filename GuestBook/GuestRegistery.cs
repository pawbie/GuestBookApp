using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook
{
    internal class GuestRegistery
    {
        private static Dictionary<string, int> _registrations = new();
        private static int _capacity = 20;

        public static void AddRegistration(string name, int partySize)
        {
            _registrations.Add(name, partySize);
        }

        public static void UpdateRegistration(string name, int partySize)
        {
            _registrations[name] = partySize;
        }

        public static int GetCapacity()
        {
            return _capacity - _registrations.Sum(registration => registration.Value);
        }

        public static int GetRegisteredCapacity()
        {
            return _registrations.Sum(registration => registration.Value);
        }

        public static bool CheckUserExists(string name)
        {
            return _registrations.Where(user => user.Key == name).Count() > 0;
        }

        public static int GetUserRegistration(string name)
        {
            return _registrations[name];
        }

        public static IEnumerable<KeyValuePair<string, int>> ListAllRegistrations()
        {
            foreach (var registration in _registrations)
            {
                yield return registration;
            }
        }
    }
}
