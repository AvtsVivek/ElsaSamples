using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Activities;
using Microsoft.Extensions.DependencyInjection;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Extensions
{
    public static class UserServiceCollectionExtensions
    {
        public static IServiceCollection AddUserActivities(this IServiceCollection services)
        {
            return services
                .AddActivity<CreateUser>()
                .AddActivity<ActivateUser>()
                .AddActivity<DeleteUser>();
        }
    }
}
