using ElevatorChallenge.Enums;
using ElevatorChallenge.Exceptions;
using ElevatorChallenge.Models;

namespace ElevatorChallenge;

/// <summary>
/// Creates a request for an elevator in a building
/// </summary>
public class ElevatorRequest
{
    private readonly Building _building;
    private readonly Person _person;

    public ElevatorRequest(Building building, Person person)
    {
        _building = building;
        _person = person;
    }

    /// <summary>
    ///  Request an elevator for person
    /// </summary>
    /// <returns></returns>
    /// <exception cref="DomainException"></exception>
    public Elevator? RequestElevator()
    {
        var elevators = _building.Elevators;
        ElevatorMotionStatuses? motionStatus = null;

        if (_person.DestinationFloor == _person.StartingFloor)
            throw new DomainException("Cannot request and elevator to the floor you already are in.");

        if(_person.DestinationFloor>_building.TopFloor || _person.DestinationFloor<_building.BottomFloor || _person.StartingFloor<_building.BottomFloor || _person.StartingFloor>_building.TopFloor)
            throw new DomainException("Floors supplied are out of range");

        if (_person.DestinationFloor > _person.StartingFloor) motionStatus = ElevatorMotionStatuses.MovingUp;
        if (_person.DestinationFloor < _person.StartingFloor) motionStatus = ElevatorMotionStatuses.MovingDown;

        IEnumerable<Elevator> availableElevators=new List<Elevator>();
        if (motionStatus.Equals(ElevatorMotionStatuses.MovingUp))
        {
            availableElevators = elevators.Where(x =>
            (x.CurrentFloor < _person.StartingFloor && (x.MaximumCapacity>x.PersonsInElevator.Count || x.PersonsInElevator.Any(y=>y.DestinationFloor==_person.StartingFloor)) && x.ElevatorMotionStatus==ElevatorMotionStatuses.MovingUp) ||
            (x.ElevatorMotionStatus == ElevatorMotionStatuses.Stationary)
            ).OrderBy(x=>x.CurrentFloor-_person.StartingFloor);
        }
        else
        {
            availableElevators = elevators.Where(x =>
                (x.CurrentFloor > _person.StartingFloor && (x.MaximumCapacity>x.PersonsInElevator.Count || x.PersonsInElevator.Any(y=>y.DestinationFloor==_person.StartingFloor)) && x.ElevatorMotionStatus==ElevatorMotionStatuses.MovingDown) ||
                (x.ElevatorMotionStatus == ElevatorMotionStatuses.Stationary)
            ).OrderBy(x=>_person.StartingFloor-x.CurrentFloor);
        }

        return availableElevators.FirstOrDefault();
    }



}