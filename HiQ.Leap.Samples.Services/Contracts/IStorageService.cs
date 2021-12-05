using HiQ.Leap.Samples.Common.Models;

namespace HiQ.Leap.Samples.Services.Contracts;

public interface IStorageService
{
    Person SavePerson(Person person);

    Person GetPerson(string name);

    List<Person> GetPersons();

    void EditPerson(Person person);
}