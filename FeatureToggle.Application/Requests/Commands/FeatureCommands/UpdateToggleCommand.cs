using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class UpdateToggleCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public int FeatureId { get; set; }

        public int? BusinessId { get; set; }

        public bool EnableOrDisable { get; set; }
    }
}
