using ElevatorChallenge.Exceptions;

namespace ElevatorChallenge.Models;
public class Building
{
    public Building(int topFloor, int bottomFloor)
    {
        TopFloor = topFloor;
        BottomFloor = bottomFloor;
    }

    public int TopFloor { get; set; }
    public int BottomFloor { get; set; }
    public List<Elevator> Elevators { get; set; } = new List<Elevator>();

    public void AddElevators(IEnumerable<Elevator> elevators)
    {
        if (elevators.Any(elevator => Elevators.Any(x => string.Equals(x.Name, elevator.Name, StringComparison.CurrentCultureIgnoreCase))))
        {
            throw new DomainException("Multiple elevator names not allowed");
        }

        Elevators.AddRange(elevators.Select(x => new Elevator(x.Name, x.MaximumCapacity, BottomFloor, TopFloor)));
    }

    public void AddElevators(Elevator elevator)
    {
        if (Elevators.Any(x => string.Equals(x.Name, elevator.Name, StringComparison.CurrentCultureIgnoreCase)))
            throw new DomainException("Multiple elevator names not allowed");
        Elevators.Add(new Elevator(elevator.Name, elevator.MaximumCapacity, BottomFloor, TopFloor));
    }

    public void SwitchOnElevators()
    {
        if (!Elevators.Any()) throw new DomainException("Building has no elevators.");

        foreach (var elevator in Elevators)
        {
            elevator.Run();
        }
    }
}