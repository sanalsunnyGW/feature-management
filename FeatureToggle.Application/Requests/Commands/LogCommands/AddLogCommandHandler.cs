using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;

namespace FeatureToggle.Application.Requests.Commands.LogCommands
{
    public class AddLogCommandHandler(FeatureManagementContext featureContext) : IRequestHandler<AddLogCommand, int>
    {
        public async Task<int> Handle(AddLogCommand request, CancellationToken cancellationToken)
        {
            Log log = new Log(request.UserId,request.UserName,request.FeatureId,request.FeatureName,request.BusinessId,request.BusinessName,request.action);

            await featureContext.Logs.AddAsync(log,cancellationToken);

            return await featureContext.SaveChangesAsync(cancellationToken);
        }
    }
}
