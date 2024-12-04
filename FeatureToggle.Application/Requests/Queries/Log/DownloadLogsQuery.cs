using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class DownloadLogsQuery : IRequest<FileContentResult>
    {

    }
}
