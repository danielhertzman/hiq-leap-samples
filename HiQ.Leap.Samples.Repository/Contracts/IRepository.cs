using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;

namespace HiQ.Leap.Samples.Repository.Contracts;

public interface IRepository
{
    Person Add(PersonCreateRequest person);

    List<Person> GetPersons();

    Person GetPerson(int id);

    void UpdatePerson(int id, PersonEditRequest request);

    void DeletePerson(int id);
}