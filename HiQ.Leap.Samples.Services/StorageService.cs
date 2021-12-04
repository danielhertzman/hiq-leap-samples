using HiQ.Leap.Samples.Common.Models;
using HiQ.Leap.Samples.Services.Contracts;

namespace HiQ.Leap.Samples.Services
{
    public class StorageService : IStorageService
    {
        private readonly List<Person> _personList;

        public StorageService()
        {
            _personList = new List<Person>();
        }

        public void EditPerson(Person person)
        {
            var index = _personList.FindIndex(x => x.Name == person.Name);
            _personList[index] = person;
        }

        public Person GetPerson(string name)
        {
            var person = _personList.SingleOrDefault(x => x.Name == name);

            if (person == null)
            {
                throw new Exception("Person not found");
            }

            return person;
        }

        public List<Person> GetPersons()
        {
            return _personList;
        }

        public Person SavePerson(Person person)
        {
            _personList.Add(person);
            return person;
        }
    }
}