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

            Console.Write("Enter cout of team members, value shoud be greater than '0': ");
            var membersCount = Convert.ToInt32(Console.ReadLine()) - 1;

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
                    Console.WriteLine("User clicked A");
                    Member.AddMember(members, 1, true);
                    Console.WriteLine("Team members have been updated:");
                    Member.ShowMembers(members);
                }
                else if (enteredKey.Key == ConsoleKey.P)
                {
                    Console.WriteLine("User clicked P");
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
                if (membersCount <= 0)
                {
                    Console.WriteLine("Please enter a valid value greater than 0.");
                    return;
                }
                

                var startIteration = members.Count; //0
                var maxIteration = membersCount; //2

                if (isRecall)
                {
                    startIteration = members.Count + 1; // 2 + 1 = 3
                    maxIteration = members.Count + membersCount; // 2+1 = 3
                }

                for (int i = startIteration; i <= maxIteration; i++)
                {
                    Console.Write($"Enter the name of team member number {i + 1} : ");
                    var name = Console.ReadLine();
                    Console.Write($"Enter the age of team member number {i + 1} : ");
                    var age = Convert.ToInt32(Console.ReadLine());
                    Console.Write($"Enter programing language name which using team member number {i + 1}: ");
                    var programingLanguage = Console.ReadLine();
                    Console.Write($"Enter 'YES' if team member number {i + 1} is on full time contract and 'NO' if not: ");
                    var contractType = Console.ReadLine().ToLower();
                    var isFulltime = contractType == "yes" ? true : false;

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
    }
}
