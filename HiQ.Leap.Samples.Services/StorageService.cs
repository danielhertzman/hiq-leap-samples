using HiQ.Leap.Samples.Domain.Exceptions;
using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;
using HiQ.Leap.Samples.Services.Contracts;

namespace HiQ.Leap.Samples.Services;

public class StorageService : IStorageService
{
    private readonly ILogger<StorageService> _logger;
    private readonly Dictionary<int, Person> _persons;

    private int _lastKeyInserted;

    public StorageService(ILogger<StorageService> logger)
    {
        _logger = logger;
        _persons = new Dictionary<int, Person>();
    }

    public void EditPerson(int id, PersonEditRequest request)
    {
        if (!_persons.TryGetValue(id, out var person))
        {
            throw new PersonNotFoundException(id);
        }

        person.GivenName = request.GivenName;
        person.SurName = request.SurName;
        _persons[id] = person;
    }

    public Person GetPerson(int id)
    {
        _logger.LogInformation("Attempting to find person with id '{id}'", id);

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

    public Person SavePerson(PersonCreateRequest personRequest)
    {
        var id = ++_lastKeyInserted;

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
}