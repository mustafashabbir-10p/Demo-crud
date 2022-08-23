using Demo.models;

namespace Demo.Service
{
    public interface IAuth
    {
        Task<Person> register(Register add);
        Task<string> login(Login add);

    }
}
