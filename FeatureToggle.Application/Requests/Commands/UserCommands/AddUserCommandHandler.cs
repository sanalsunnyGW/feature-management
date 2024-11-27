using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FeatureToggle.Application.Requests.Commands.UserCommands
{
    public class AddUserCommandHandler(UserManager<User> userManager, IValidator<AddUserCommand> userValidator) : IRequestHandler<AddUserCommand, AddUserResponseDTO>
    {
        public async Task<AddUserResponseDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            User newUser = new User(request.Email, request.Name);  //use '!' ??

            var validationResult = await userValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new AddUserResponseDTO
                {
                    Success = false,
                    Message = "Failed to create user , one or more validations failed",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }


            var result = await userManager.CreateAsync(newUser, request.Password);


            return result.Succeeded ? new AddUserResponseDTO
                {
                    Success = true,
                    Message = "User created successfully"
                }
                : new AddUserResponseDTO
                {
                    Success = false,
                    Message = "Failed to create user",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
        }
    }

    
}
