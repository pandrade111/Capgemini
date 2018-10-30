using Capgemini.Domain.Entities;
using Capgemini.Domain.Queries;
using Capgemini.Domain.Repositories;
using Capgemini.Infra.DataContetx;
using Dapper;
using System;
using System.Linq;

namespace Capgemini.Infra.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly DataContext _context;
        public PersonRepository(DataContext context)
        {
            _context = context;
        }
        public bool CheckCPF(string cpf)
        {
            return
                _context
                .Connection
                .Query<bool>(
                    "SELECT CPF FROM Person WHERE CPF = @cpf",
                    new { cpf })
                .FirstOrDefault();
        }

        public bool CheckEmail(string email)
        {
            return
                _context
                .Connection
                .Query<bool>(
                    "SELECT Email FROM Person WHERE Email = @email",
                    new { email })
                .FirstOrDefault();
        }

        public GetPersonCommandResult Get(string cpf)
        {
            return
                _context
                .Connection
                .QueryFirstOrDefault<GetPersonCommandResult>(
                    @"SELECT Id, Name, CPF, Email 
                        FROM Person 
                       WHERE CPF = @cpf",
                    new { cpf });
        }

        public void Save(Person person)
        {
            _context
               .Connection
               .Execute(
                   "INSERT INTO User (Id,Username,Password) Values(@id,@username,@password)",
                   new { person.User.Id, person.User.Username, person.User.Password });

            _context
               .Connection
               .Execute(
                   @"INSERT INTO Person (Id,UserId,FirstName,LastName,CPF,Email,Phone) 
                        Values(@id,@userId,@firstName,@lastName,@cpf,@email,@phone)",
                   new
                   {
                       person.Id,
                       UserId = person.User.Id,
                       person.Name.FirstName,
                       person.Name.LastName,
                       CPF = person.CPF.Number,
                       Email = person.Email.Address,
                       person.Phone
                   });

            foreach (var address in person.Addresses)
            {
                _context
                    .Connection
                    .Execute(
                    @"INSERT INTO Address(Id, PersonId, Number, Complement, District, City, State, Country, ZipCode) 
                        Values(@id, @personId, @number, @complement, @district, @city, @state, @country, @zipcode)",
                new
                {
                    address.Id,
                    CustomerId = person.Id,
                    address.Number,
                    address.Complement,
                    address.District,
                    address.City,
                    address.State,
                    address.Country,
                    address.ZipCode,
                });
            }
        }

        public void Update(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
