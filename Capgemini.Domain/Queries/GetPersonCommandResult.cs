using System;

namespace Capgemini.Domain.Queries
{
    public class GetPersonCommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
    }
}
