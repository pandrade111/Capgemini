using Capgemini.Domain.Entities;
using Capgemini.Domain.Queries;

namespace Capgemini.Domain.Repositories
{
    public interface IPersonRepository
    {
        GetPersonCommandResult Get(string cpf);
        void Save(Person person);
        void Update(Person person);
        bool CheckCPF(string cpf);
        bool CheckEmail(string email);
    }
}
