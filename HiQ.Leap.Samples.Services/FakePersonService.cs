using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;
using HiQ.Leap.Samples.Services.Contracts;

namespace HiQ.Leap.Samples.Services;

public class FakePersonService : IPersonService
{
    public void EditPerson(int id, PersonEditRequest person)
    {
        throw new NotImplementedException();
    }

    public Person GetPerson(int id)
    {
        throw new NotImplementedException();
    }

    public List<Person> GetPersons()
    {
        throw new NotImplementedException();
    }

    public Task<Person> SavePersonAsync(PersonCreateRequest personRequest)
    {
        throw new NotImplementedException();
    }

    public void DeletePerson(int id)
    {
        throw new NotImplementedException();
    }
}