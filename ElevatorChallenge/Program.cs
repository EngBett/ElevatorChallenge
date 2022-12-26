using System.Text;
using ElevatorChallenge.Exceptions;
using ElevatorChallenge.Models;

namespace ElevatorChallenge;

internal class Program
{
    /// <summary>
    /// Application Entry
    /// </summary>
    /// <param name="args">CommandLine Arguments</param>
    static void Main(string[] args)
    {
        var building = new Building(12, 0);

        //Create  and add elevators to the building
        var elevators = new List<Elevator>()
        {
            new("A", 20),
            new("B", 20),
            new("C", 20),
            new("D", 20),
            new("E", 20),
            new("F", 20),
            new("G", 20)
        };

        building.AddElevators(elevators);
        building.SwitchOnElevators();

        //Run Program
        RunProgram(building);
        Environment.Exit(0);
    }

    /// <summary>
    /// Runs the console program for Elevator
    /// </summary>
    /// <param name="building"></param>
    private static void RunProgram(Building building)
    {
        int choice;
        while (true)
        {
            try
            {
                Menu();
                choice = GetInteger("Enter your choice", 1, 2);
                if (choice == 2) break;

                var person = PersonElevatorRequest(building.BottomFloor, building.TopFloor);
                var request = new ElevatorRequest(building, person);
                var availableElevator = request.RequestElevator();
                if (availableElevator != null)
                {
                    Console.WriteLine($"Elevator '{availableElevator.Name}' is coming to get you");
                    building.Elevators.FirstOrDefault(x=>x.Name==availableElevator.Name).PersonsQueue.Add(person);
                    continue;
                }
                Console.WriteLine($"All elevators are currently busy.");
            }
            catch (DomainException e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
        }
    }

    /// <summary>
    /// Menu for to guide running of the program
    /// </summary>
    private static void Menu()
    {
        var str = new StringBuilder("Enter the choices below:\n");
        str.Append("\t1. Elevator Request.\n");
        str.Append("\t2. Quit\n");
        Console.WriteLine();
        Console.WriteLine(str.ToString());
    }

    /// <summary>
    /// Person Elevator Request
    /// </summary>
    /// <returns>Person Object</returns>
    private static Person PersonElevatorRequest(int minFloor, int maxFloor)
    {
        var startFloor = GetInteger("Enter the starting floor", minFloor, maxFloor);
        var destinationFloor = GetInteger("Enter the destination floor", minFloor, maxFloor);
        return new Person { StartingFloor = startFloor, DestinationFloor = destinationFloor };
    }

    /// <summary>
    /// Integer input on console
    /// </summary>
    /// <param name="promptMessage">Message prompt before integer input</param>
    /// <param name="min">Minimum integer allowed</param>
    /// <param name="max">Maximum integer allowed</param>
    /// <returns>Integer</returns>
    private static int GetInteger(string promptMessage, int min, int max)
    {
        int num;
        while (true)
        {
            Console.Write($"{promptMessage}:");

            try
            {
                num = Convert.ToInt32(Console.ReadLine());
                if (num <= max && num >= min) break;

                Console.WriteLine($"ERROR: Enter integer between '{min}' and '{max}'");
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR: Enter valid Integer");
            }
        }

        return num;
    }
}