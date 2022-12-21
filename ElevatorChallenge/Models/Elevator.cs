using ElevatorChallenge.Enums;

namespace ElevatorChallenge.Models;

public class Elevator
{
    public Elevator(string name, int maximumCapacity, int topFloor, int bottomFloor)
    {
        Name = name;
        MaximumCapacity = maximumCapacity;
        TopFloor = topFloor;
        BottomFloor = bottomFloor;
    }

    public Elevator()
    {
    }

    public string Name { get; private set; }
    public ElevatorMotionStatuses ElevatorMotionStatus { get; set; } = ElevatorMotionStatuses.Stationary;
    public int CurrentFloor { get; set; }
    public int TopFloor { get; set; }
    public int BottomFloor { get; set; }
    public int MaximumCapacity { get; }
    public int CurrentCapacity { get; set; }

    public void Run()
    {

        if (ElevatorMotionStatus == ElevatorMotionStatuses.MovingDown)
        {
            if (CurrentFloor > BottomFloor) CurrentFloor--;
            else ElevatorMotionStatus = ElevatorMotionStatuses.Stationary;
        }
        else
        {
            if (CurrentFloor < TopFloor) CurrentFloor++;
            else ElevatorMotionStatus = ElevatorMotionStatuses.Stationary;
        }
    }
}