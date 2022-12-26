using ElevatorChallenge.Enums;
using ElevatorChallenge.Exceptions;

namespace ElevatorChallenge.Models;

/// <summary>
/// Elevator Model
/// </summary>
public class Elevator
{
    public Elevator(string name, int maximumCapacity, int bottomFloor, int topFloor)
    {
        Name = name;
        MaximumCapacity = maximumCapacity;
        BottomFloor = bottomFloor;
        TopFloor = topFloor;
    }

    public Elevator(string name, int maximumCapacity)
    {
        if (string.IsNullOrEmpty(name)) throw new DomainException("Elevator name must not be empty");
        if (maximumCapacity <= 0) throw new DomainException("Maximum capacity must be greater than 0");
        Name = name;
        MaximumCapacity = maximumCapacity;
    }


    public string Name { get; private set; }
    public ElevatorMotionStatuses ElevatorMotionStatus { get; set; } = ElevatorMotionStatuses.Stationary;
    public int CurrentFloor { get; set; }
    public int TopFloor { get; set; }
    public int BottomFloor { get; set; }
    public int MaximumCapacity { get; }

    private List<Person> _personsInElevator = new List<Person>();
    public IReadOnlyList<Person> PersonsInElevator => _personsInElevator;
    public List<Person> PersonsQueue = new();

    public void AddPersonToElevator(Person person)
    {
        if (_personsInElevator.Count == MaximumCapacity) throw new DomainException("Elevator maximum capacity reached");
        _personsInElevator.Add(person);
    }

    public void AddPersonToElevator(IEnumerable<Person> persons)
    {
        if (_personsInElevator.Count + persons.Count() > MaximumCapacity)
            throw new DomainException("Elevator maximum capacity reached");
        _personsInElevator.AddRange(persons);
    }

    public void Run()
    {
        var t = new Thread(ElevatorMotion);
        t.Start();
    }

    private void ElevatorMotion()
    {
        while (true)
        {
            Thread.Sleep(1000);

            if (ElevatorMotionStatus == ElevatorMotionStatuses.Stationary && PersonsQueue.Any())
            {
                var defaultPerson = PersonsQueue.FirstOrDefault();
                if (defaultPerson?.StartingFloor < CurrentFloor)
                {
                    ElevatorMotionStatus = ElevatorMotionStatuses.MovingDown;
                }

                if (defaultPerson?.StartingFloor > CurrentFloor)
                {
                    ElevatorMotionStatus = ElevatorMotionStatuses.MovingUp;
                }

                if (defaultPerson?.StartingFloor == CurrentFloor && defaultPerson.DestinationFloor > CurrentFloor)
                    ElevatorMotionStatus = ElevatorMotionStatuses.MovingUp;
                if (defaultPerson?.StartingFloor == CurrentFloor && defaultPerson.DestinationFloor < CurrentFloor)
                    ElevatorMotionStatus = ElevatorMotionStatuses.MovingDown;
            }


            //Console.WriteLine($"Elevator '{Name}' {ElevatorMotionStatus}");
            if (!PersonsQueue.Any() && ElevatorMotionStatus == ElevatorMotionStatuses.Stationary) continue;

            if (PersonsQueue.Any(x => x.StartingFloor == CurrentFloor))
            {
                AddPersonToElevator(PersonsQueue.Where(x => x.StartingFloor == CurrentFloor));
                PersonsQueue.RemoveAll(x => x.StartingFloor == CurrentFloor);
                _personsInElevator.RemoveAll(x => x.DestinationFloor == CurrentFloor);
            }

            if (PersonsInElevator.Any(x => x.DestinationFloor == CurrentFloor))
                _personsInElevator.Remove(PersonsInElevator.FirstOrDefault(x => x.DestinationFloor == CurrentFloor));

            if (!PersonsInElevator.Any())
            {
                ElevatorMotionStatus = ElevatorMotionStatuses.Stationary;
                continue;
            }

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
}