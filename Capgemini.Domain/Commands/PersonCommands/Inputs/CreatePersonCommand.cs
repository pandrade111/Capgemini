using Capgemini.Shared.Commands;
using FluentValidator;
using FluentValidator.Validation;

namespace Capgemini.Domain.Commands.PersonCommands.Inputs
{
    public class CreatePersonCommand : Notifiable, ICommand
    {
        //User
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        //Person
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        
        //Address
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        bool ICommand.Valid()
        {
            AddNotifications(new ValidationContract()
                .HasMinLen(FirstName, 3, "FirstName", "O nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 40, "FirstName", "O nome deve conter no máximo 40 caracteres")
                .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter pelo menos 3 caracteres")
                .HasMaxLen(LastName, 40, "LastName", "O sobrenome deve conter no máximo 40 caracteres")
                .IsEmail(Email, "Email", "O E-mail é inválido")
                .HasLen(CPF, 11, "Document", "CPF inválido"));

            return Valid;
        }
    }
}