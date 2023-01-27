using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthValidator:AbstractValidator<User>
    {
        public AuthValidator()
        {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.FirstName.Length).LessThan(30);
            RuleFor(u => u.LastName.Length).LessThan(30);
        }
    }
}
