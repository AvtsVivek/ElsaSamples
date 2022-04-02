using System.Threading;
using System.Threading.Tasks;
using Elsa.Scripting.Liquid.Messages;
using Fluid;
using MediatR;
using MxWork.ElsaWf.UserRegis.BlazorServerMongo.Models;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMongo.Handlers
{
    /// <summary>
    /// Configure the Liquid template context to allow access to certain models. 
    /// </summary>
    public class LiquidConfigurationHandler : INotificationHandler<EvaluatingLiquidExpression>
    {
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            var context = notification.TemplateContext;
            context.MemberAccessStrategy.Register<User>();
            context.MemberAccessStrategy.Register<RegistrationModel>();

            return Task.CompletedTask;
        }
    }
}
