using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;
using HiQ.Leap.Samples.RandomIntegration.Contracts;
using HiQ.Leap.Samples.Repository.Contracts;
using HiQ.Leap.Samples.Services.Contracts;

namespace HiQ.Leap.Samples.Services;

public class PersonService : IPersonService
{
    private readonly IRepository _repository;
    private readonly IRandomIntegration _randomIntegration;
    private readonly ILogger<PersonService> _logger;

    public PersonService(IRepository repository, IRandomIntegration randomIntegration, ILogger<PersonService> logger)
    {
        _repository = repository;
        _randomIntegration = randomIntegration;
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

    public async Task<Person> SavePersonAsync(PersonCreateRequest personRequest)
    {
        var createdPerson = _repository.Add(personRequest);
        await _randomIntegration.InvokeRandomIntegrationAsync();
        return createdPerson;
    }

    public void DeletePerson(int id)
    {
        _repository.DeletePerson(id);
    }
}