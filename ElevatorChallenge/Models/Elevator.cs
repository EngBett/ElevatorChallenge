using ElevatorChallenge.Enums;

namespace ElevatorChallenge.Models;

public class Elevator
{
    public Elevator(string name, int maximumCapacity)
    {
        Name = name;
        MaximumCapacity = maximumCapacity;
    }
    public Elevator(){}
    public string Name { get; private set; }
    public ElevatorMotionStatuses ElevatorMotionStatus { get; set; } = ElevatorMotionStatuses.Stationary;
    public int CurrentFloor { get; set; }
    public int MaximumCapacity { get; }
    public int CurrentCapacity { get; set; }
}