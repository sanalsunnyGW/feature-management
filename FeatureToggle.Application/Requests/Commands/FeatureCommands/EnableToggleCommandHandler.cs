using FeatureToggle.Application.Requests.Commands.LogCommands;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Domain.Entity.Enum;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class EnableToggleCommandHandler(BusinessContext businessContext, FeatureManagementContext featureManagementContext, IMediator mediator) : IRequestHandler<EnableToggleCommand, int>
    {
        public async Task<int> Handle(EnableToggleCommand request, CancellationToken cancellationToken)
        {
            Feature feature = await businessContext.Feature.FirstAsync(x => x.FeatureId == request.FeatureId, cancellationToken);
            User user = await featureManagementContext.Users.FirstAsync(x => x.Id == request.UserId, cancellationToken);

            if (request.BusinessId is null) //checking if release toggle
            {
                BusinessFeatureFlag? selectBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId, cancellationToken);

                    //Enable release toggle

                if (selectBusiness is not null)
                {

                    if (!selectBusiness.IsEnabled)
                    {
                        selectBusiness.UpdateIsenabled(true);
                        businessContext.BusinessFeatureFlag.Update(selectBusiness);


                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = null,
                            BusinessName = null,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Enabled
                        };

                        await mediator.Send(addLog, cancellationToken);
                    }
                    return await businessContext.SaveChangesAsync(cancellationToken);

                }
                else
                {
                    //Feature requiredFeature = await businessContext.Feature.FirstAsync(x => x.FeatureId == request.FeatureId, cancellationToken);

                    BusinessFeatureFlag newBusinessFlag = new(feature);

                    await businessContext.AddAsync(newBusinessFlag, cancellationToken);

                    AddLogCommand addLog = new()
                    {
                        FeatureId = request.FeatureId,
                        FeatureName = feature.FeatureName,
                        BusinessId = null,
                        BusinessName = null,
                        UserId = request.UserId,
                        UserName = user.UserName,
                        action = Actions.Enabled
                    };

                    await mediator.Send(addLog, cancellationToken);

                    return await businessContext.SaveChangesAsync(cancellationToken);
                }

            }


            else // if feature toggle
            {
                //To get business Name for feature toggle
                Business business = await businessContext.Business.FirstAsync(x => x.BusinessId == request.BusinessId, cancellationToken);

                BusinessFeatureFlag? selectedBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId && x.BusinessId == request.BusinessId, cancellationToken);

                //Enable feature toggle
                if (selectedBusiness is not null)
                {
                    if (!selectedBusiness.IsEnabled)
                    {
                        selectedBusiness.UpdateIsenabled(true);
                        businessContext.BusinessFeatureFlag.Update(selectedBusiness);

                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = request.BusinessId,
                            BusinessName = business.BusinessName,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Enabled
                        };

                        await mediator.Send(addLog, cancellationToken);

                        return await businessContext.SaveChangesAsync(cancellationToken);
                    }

                    return -1;


                }
                else
                {

                    if (feature.FeatureTypeId == 2)
                    {
                        Business requiredBusiness = await businessContext.Business.FirstAsync(x => x.BusinessId == request.BusinessId, cancellationToken);

                        BusinessFeatureFlag newBusinessFlag = new(feature, requiredBusiness!);

                        await businessContext.AddAsync(newBusinessFlag, cancellationToken);

                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = request.BusinessId,
                            BusinessName = business.BusinessName,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Enabled
                        };

                        await mediator.Send(addLog, cancellationToken);

                        return await businessContext.SaveChangesAsync(cancellationToken);

                    }

                    return -1;

                }
                
                
            }

        }
    
    }
}
