using MxWork.ElsaWf.UserRegis.BlazorServerMongo.Activities;
using Microsoft.Extensions.DependencyInjection;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMongo.Extensions
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
