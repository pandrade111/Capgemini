using Capgemini.Domain.ValuesObjects;
using Capgemini.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Capgemini.Domain.Entities
{
    public class Person : Entity
    {
        private readonly IList<Address> _addresses;

        public Person(
            User user,
            Name name,
            CPF cpf,
            Email email,
            string phone)
        {
            User = user;
            Name = name;
            CPF = CPF;
            Email = email;
            Phone = phone;
            _addresses = new List<Address>();
        }

        public User User { get; private set; }
        public Name Name { get; private set; }
        public CPF CPF { get; private set; }
        public Email Email { get; private set; }
        public string Phone { get; private set; }
        public IReadOnlyCollection<Address> Addresses => _addresses.ToArray();

        public void AddAddress(Address address) => _addresses.Add(address);

        public override string ToString() => Name.ToString();
    }
}