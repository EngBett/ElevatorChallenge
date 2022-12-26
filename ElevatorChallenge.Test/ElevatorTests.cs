using System.Collections.Generic;
using ElevatorChallenge.Exceptions;
using ElevatorChallenge.Models;
using NUnit.Framework;

namespace ElevatorChallenge.Test;

public class ElevatorTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCreation()
    {

        Assert.Throws<DomainException>((() =>
        {
            var elevator = new Elevator("",10);
        }));
    }

    [Test]
    public void TestCreation1()
    {

        Assert.Throws<DomainException>((() =>
        {
            var elevator = new Elevator("A",0);
        }));
    }

    [Test]
    public void TestCreation2()
    {

        var elevator = new Elevator("A",10);
        Assert.Pass();
    }

    [Test]
    public void AddPersonTest()
    {

        var elevator = new Elevator("A",10);
        var person = new Person(1, 2);
        elevator.AddPersonToElevator(person);
        Assert.Pass();
    }

    [Test]
    public void AddPersonTest1()
    {

        var elevator = new Elevator("A",10);
        var persons = new List<Person>()
        {
            new Person(1, 2),
            new Person(1, 2)
        };

        elevator.AddPersonToElevator(persons);
        Assert.Pass();
    }

    [Test]
    public void AddPersonTest2()
    {

        var elevator = new Elevator("A",1);
        var persons = new List<Person>()
        {
            new Person(1, 2),
            new Person(1, 2)
        };


        Assert.Throws<DomainException>((() =>
        {
            elevator.AddPersonToElevator(persons);
        }));
    }

}