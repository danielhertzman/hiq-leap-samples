using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;

namespace HiQ.Leap.Samples.Services.Contracts;

public interface IPersonService
{
    void EditPerson(int id, PersonEditRequest person);

    Person GetPerson(int id);

    List<Person> GetPersons();

    Task<Person> SavePersonAsync(PersonCreateRequest personRequest);

    void DeletePerson(int id);
}