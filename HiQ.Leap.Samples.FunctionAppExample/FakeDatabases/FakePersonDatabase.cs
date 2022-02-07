using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiQ.Leap.Samples.FunctionAppExample.Models;

namespace HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;

public class FakePersonDatabase
{
    private readonly Dictionary<int, Person> _persons;
    private int CurrentKey;

    public FakePersonDatabase()
    {
        _persons = new Dictionary<int, Person>();
    }

    public async Task<Person> AddAsync(PersonCreateRequest personRequest)
    {
        var id = ++CurrentKey;

        var person = new Person
        {
            Id = id,
            GivenName = personRequest.GivenName,
            SurName = personRequest.SurName,
        };

        _persons.Add(id, person);
        return await Task.FromResult(person); // Never mind the await, just to satisfy async/await
    }

    public void DeletePerson(int id)
    {
        if (!_persons.Remove(id))
        {
        }
    }

    public async Task<Person> GetPersonAsync(int id)
    {
        if (!_persons.TryGetValue(id, out var person))
        {
            return null;
        }

        return await Task.FromResult(person);
    }

    public async Task<List<Person>> GetPersonsAsync()
    {
        await Task.CompletedTask; // Don't mind this, just to satisfy the async/await
        return _persons.Values.ToList();
    }

    public void UpdatePerson(int id, PersonEditRequest request)
    {
        if (!_persons.TryGetValue(id, out var person))
        {
            return;
        }

        person.GivenName = request.GivenName;
        person.SurName = request.SurName;
        _persons[id] = person;
    }
}