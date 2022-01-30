using HiQ.Leap.Samples.Domain.Exceptions;
using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;
using HiQ.Leap.Samples.Repository.Contracts;

namespace HiQ.Leap.Samples.Repository;

public class Repository : IRepository
{
    private readonly Dictionary<int, Person> _persons;
    private static int CurrentKey;

    public Repository()
    {
        _persons = new Dictionary<int, Person>();
    }

    public Person Add(PersonCreateRequest personRequest)
    {
        var id = ++CurrentKey;

        var person = new Person
        {
            Id = id,
            GivenName = personRequest.GivenName,
            SurName = personRequest.SurName,
        };

        _persons.Add(id, person);
        return person;
    }

    public void DeletePerson(int id)
    {
        if (!_persons.Remove(id))
        {
            throw new PersonNotFoundException(id);
        }
    }

    public Person GetPerson(int id)
    {
        if (!_persons.TryGetValue(id, out var person))
        {
            throw new PersonNotFoundException(id);
        }

        return person;
    }

    public List<Person> GetPersons()
    {
        return _persons.Values.ToList();
    }

    public void UpdatePerson(int id, PersonEditRequest request)
    {
        if (!_persons.TryGetValue(id, out var person))
        {
            throw new PersonNotFoundException(id);
        }

        person.GivenName = request.GivenName;
        person.SurName = request.SurName;
        _persons[id] = person;
    }
}

