using Capgemini.Domain.Commands;
using Capgemini.Domain.Commands.PersonCommands.Inputs;
using Capgemini.Domain.Handlers;
using Capgemini.Domain.Queries;
using Capgemini.Domain.Repositories;
using Capgemini.Shared.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Capgemini.Api.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;
        private readonly PersonHandler _personHandler;
        public PersonController(IPersonRepository personRepository,
            PersonHandler personHandler)
        {
            _personRepository = personRepository;
            _personHandler = personHandler;
        }
        [HttpPost]
        [Route("v1/persons")]
        public ICommandResult Post([FromBody]CreatePersonCommand command)
        {
            var result = (CommandResult)_personHandler.Handle(command);
            return result;
        }
        [HttpGet]
        [Route("v1/persons/{cpf}")]
        public GetPersonCommandResult GetById(string cpf)
        {
            return _personRepository.Get(cpf);
        }
    }
}
