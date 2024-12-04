using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FeatureToggle.Application.Requests.Queries.Login
{
    public class GetAuthTokenQueryValidator : AbstractValidator<GetAuthTokenQuery>
    {
        public GetAuthTokenQueryValidator() { 
            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress()
                                 .Must(x => x.EndsWith("@geekywolf.com"))
                                 .WithMessage("Provided email is not part of GeekyWolf.");
        }
    }
}
