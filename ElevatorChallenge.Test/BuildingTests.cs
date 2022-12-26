using System.Collections.Generic;
using ElevatorChallenge.Exceptions;
using ElevatorChallenge.Models;
using NUnit.Framework;

namespace ElevatorChallenge.Test;

public class BuildingTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NewBuildingTest()
    {
        var newBuilding = new Building(3,0);
        Assert.Pass();
    }

    [Test]
    public void NewBuildingTest1()
    {
        Assert.Throws<DomainException>((() =>
        {
            var building = new Building(1, 1);
        }));
    }

    [Test]
    public void NewBuildingTest2()
    {
        Assert.Throws<DomainException>((() =>
        {
            var building = new Building(0, 3);
        }));
    }

    [Test]
    public void BuildingAddElevatorTest()
    {
        var newBuilding = new Building(3,0);
        var elevators = new List<Elevator>()
        {
            new("A",5),
            new("B",7)
        };

        newBuilding.AddElevators(elevators);
        Assert.Pass();
    }

    [Test]
    public void BuildingAddElevatorTest1()
    {

        var newBuilding = new Building(3,0);
        var elevators = new List<Elevator>()
        {
            new("A",5),
            new("A",7)
        };

        Assert.Throws<DomainException>((() =>
        {
            newBuilding.AddElevators(elevators);
        }));
    }


    [Test]
    public void BuildingSwitchOnElevator()
    {

        var newBuilding = new Building(3,0);
        var elevators = new List<Elevator>()
        {
            new("A",7),
            new("B",7)
        };

        newBuilding.AddElevators(elevators);
        newBuilding.SwitchOnElevators();
        Assert.Pass();
    }

    [Test]
    public void BuildingSwitchOnElevator1()
    {

        var newBuilding = new Building(3,0);

        Assert.Throws<DomainException>((() =>
        {
            newBuilding.SwitchOnElevators();
        }));
    }
}