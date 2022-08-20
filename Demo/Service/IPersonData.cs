using Demo.models;

namespace Demo.Service
{
    public interface IPersonData
    {
        Task<List<Person>> getPersons();

        Task<Person> getPerson(Guid id);

        Task<Person> addPerson(AddPerson add);

        Task<Person> updatePerson(AddPerson upt, Guid id);
        Task<Person> deletePerson(Guid id);
    }
}
