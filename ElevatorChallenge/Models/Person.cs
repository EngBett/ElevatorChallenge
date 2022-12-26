namespace ElevatorChallenge.Models;

public class Person
{
    public Person(int startingFloor, int destinationFloor, Elevator elevator)
    {
        StartingFloor = startingFloor;
        DestinationFloor = destinationFloor;
        Elevator = elevator;
    }

    public Person(int startingFloor, int destinationFloor)
    {
        StartingFloor = startingFloor;
        DestinationFloor = destinationFloor;
    }

    public Person(){}
    public int StartingFloor { get; set; }
    public int DestinationFloor { get; set; }
    public Elevator Elevator { get; set; }
}