using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;
using HiQ.Leap.Samples.Repository.Contracts;
using HiQ.Leap.Samples.Services.Contracts;

namespace HiQ.Leap.Samples.Services;

public class PersonService : IPersonService
{
    private readonly IRepository _repository;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IRepository repository, ILogger<PersonService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public void EditPerson(int id, PersonEditRequest request)
    {
        _repository.UpdatePerson(id, request);
    }

    public Person GetPerson(int id)
    {
        _logger.LogInformation("Attempting to find person with id '{id}'", id);
        var person = _repository.GetPerson(id);
        return person;
    }

    public List<Person> GetPersons()
    {
        var persons = _repository.GetPersons();
        return persons;
    }

    public Person SavePerson(PersonCreateRequest personRequest)
    {
        var createdPerson = _repository.Add(personRequest);
        return createdPerson;
    }

    public void DeletePerson(int id)
    {
        _repository.DeletePerson(id);
    }
}