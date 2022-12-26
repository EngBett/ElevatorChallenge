using System.Collections.Generic;
using ElevatorChallenge.Exceptions;
using ElevatorChallenge.Models;
using NUnit.Framework;

namespace ElevatorChallenge.Test;

public class ElevatorRequestTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RequestElevator()
    {
        var building = new Building(12, 0);

        //Create  and add elevators to the building
        var elevators = new List<Elevator>()
        {
            new("A", 20),
            new("B", 20),
            new("C", 20)
        };

        building.AddElevators(elevators);
        var person = new Person(0, 12);
        var request = new ElevatorRequest(building, person);
        var availableElevator = request.RequestElevator();
        Assert.Pass();
    }

    [Test]
    public void RequestElevator1()
    {
        var building = new Building(12, 0);

        //Create  and add elevators to the building
        var elevators = new List<Elevator>()
        {
            new("A", 20),
            new("B", 20),
            new("C", 20)
        };

        building.AddElevators(elevators);
        var person = new Person(1, 1);
        Assert.Throws<DomainException>(() =>
        {
            var request = new ElevatorRequest(building, person);
            var _ = request.RequestElevator();
        });
    }

    [Test]
    public void RequestElevator2()
    {
        var building = new Building(12, 1);

        //Create  and add elevators to the building
        var elevators = new List<Elevator>()
        {
            new("A", 20),
            new("B", 20),
            new("C", 20)
        };

        building.AddElevators(elevators);
        var person = new Person(0, 1);
        Assert.Throws<DomainException>(() =>
        {
            var request = new ElevatorRequest(building, person);
            var _ = request.RequestElevator();
        });
    }

    [Test]
    public void RequestElevator3()
    {
        var building = new Building(12, 1);

        //Create  and add elevators to the building
        var elevators = new List<Elevator>()
        {
            new("A", 20),
            new("B", 20),
            new("C", 20)
        };

        building.AddElevators(elevators);
        var person = new Person(13, 1);
        Assert.Throws<DomainException>(() =>
        {
            var request = new ElevatorRequest(building, person);
            var _ = request.RequestElevator();
        });
    }
}