using Capgemini.Domain.Commands;
using Capgemini.Domain.Commands.PersonCommands.Inputs;
using Capgemini.Domain.Entities;
using Capgemini.Domain.Repositories;
using Capgemini.Domain.ValuesObjects;
using Capgemini.Shared.Commands;
using FluentValidator;

namespace Capgemini.Domain.Handlers
{
    public class PersonHandler :
        Notifiable,
        ICommandHandler<CreatePersonCommand>
    {
        private readonly IPersonRepository _personRepository;
        public PersonHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ICommandResult Handle(CreatePersonCommand command)
        {
            if (_personRepository.CheckCPF(command.CPF))
                AddNotification("CPF", "Este CPF já está em uso");

            if (_personRepository.CheckEmail(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            var user = new User(command.Username, command.Password, command.ConfirmPassword);
            var name = new Name(command.FirstName, command.LastName);
            var cpf = new CPF(command.CPF);
            var email = new Email(command.Email);

            var person = new Person(user, name, cpf, email, command.Phone);

            AddNotifications(name.Notifications);
            AddNotifications(cpf.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(person.Notifications);

            if (Invalid)
                return new CommandResult(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            // Persistir o cliente
            _personRepository.Save(person);

            return new CommandResult(true, "Usuário criado com sucesso.", new
            {
                Name = name.ToString(),
                Email = email.Address
            });
        }
    }
}
