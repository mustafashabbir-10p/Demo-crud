using Demo.models;

namespace Demo.Service
{
    public interface IAuth
    {
        Task<Person> register(AddPerson add);
        Task<string> login(Login add);

    }
}
