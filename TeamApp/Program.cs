using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var members = new List<Member>();
            int membersCount = GetValidatedInput("Enter cout of team members, value shoud be greater than '0': ");
                        

            Member.AddMember(members, membersCount);
            Member.ShowMembers(members);

            while (true)
            {

                Console.Write("\n\nPress 'X' to exit, 'A' to add another member, or 'P' to display the list of members again:");
                var enteredKey = Console.ReadKey();

                Console.WriteLine();
                if (enteredKey.Key == ConsoleKey.X)
                {
                    Console.WriteLine("Exiting the application...");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                else if (enteredKey.Key == ConsoleKey.A)
                {
                    Member.AddMember(members, 1, true);
                    Console.WriteLine("\nTeam members have been updated:");
                    Member.ShowMembers(members);
                }
                else if (enteredKey.Key == ConsoleKey.P)
                {
                    Member.ShowMembers(members);
                }
                else
                {
                    Console.WriteLine("Unknown key pressed");
                }
            }
        }

        class Member
        {
            public string Name;
            public int Age;
            public string ProgramingLanguage;
            public bool isFullTime;

            public Member(string name, int age, string programingLanguage, bool isFullTime)
            {
                Name = name;
                Age = age;
                ProgramingLanguage = programingLanguage;
                this.isFullTime = isFullTime;
            }

            public static void AddMember(List<Member> members, int membersCount, bool isRecall = false)
            {
                var maxIteration = membersCount;

                if (isRecall)
                {
                    maxIteration = members.Count + membersCount;
                }
                
                for (int i = members.Count; i < maxIteration; i++)
                {
                    Console.Write($"Enter the name of team member number {i + 1} : ");
                    var name = Console.ReadLine();

                    var age = GetValidatedInput($"Enter the age of team member number {i + 1}: ", 100); 
                    
                    Console.Write($"Enter programing language name which using team member number {i + 1}: ");
                    var programingLanguage = Console.ReadLine();
                    var isFulltime = false; // Питання: якщо заюати ВАР то вибиває помилку, тому що тре ініціалізувати, щоб компілятор розумів який то тип. Яке рішення краще ? var isFulltime = false; чи bool isFulltime;

                    while (true)
                    {
                        Console.Write($"Enter 'YES' if team member number {i + 1} is full-time contract and 'NO' if not: ");
                        var contractType = Console.ReadLine()?.Trim().ToLower();

                        if (contractType == "yes")
                        {
                            isFulltime = true;
                            break;
                        }
                        else if (contractType == "no")
                        {
                            isFulltime = false;
                            break;
                        }

                        Console.WriteLine("Invalid input. Please enter a valid answer ('YES' or 'NO').");
                    }

                    members.Add(new Member(name, age, programingLanguage, isFulltime));
                }
            }
            public static void ShowMembers(List<Member> members) {
                if (members == null || members.Count == 0)
                {
                    Console.WriteLine("No members to display.");
                    return;
                }

                Console.WriteLine("\nMembers List:");
                
                foreach (var member in members)
                {
                    var currentContractType = member.isFullTime ? "Full Time" : "Part Time";
                    Console.WriteLine($"Member {member.Name} is {member.Age} " +
                        $"years old, knows {member.ProgramingLanguage}" +
                        $" and works {currentContractType}");
                }
            }
        }
        static int GetValidatedInput(string prompt, int? max = null) // що це за оператор ?
        {
            int value;

            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out value) && value > 0 && (max == null || value <= max))
                {
                    return value;
                }

                string rangeMessage = max != null ? $"between 1 and {max}" : $"from 1 to infinity: ";
                Console.WriteLine($"Invalid input. Please enter a valid number ({rangeMessage}).");
            }
        }
    }
}
