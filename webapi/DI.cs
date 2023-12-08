using MapsterMapper;
using SignalRChat.Core.Service.Impl;
using SignalRChat.Core.Service.Interfaces;
using SignalRChat.Data.Repositories.Impl;
using SignalRChat.Data.Repositories.Interfaces;

namespace webapi
{
    public static class DI
    {
        public static IServiceCollection AddRepositoriesDI(this IServiceCollection services)
        {
            return services
                .AddScoped<IPersonRepository, PersonRepository>()
                .AddScoped<IPersonalMessageRepository, PersonalMessageRepository>()
                .AddScoped<IGroupMessageRepository, GroupMessageRepository>()
                .AddScoped<IGroupRepository, GroupRepository>()
                .AddScoped<IGroupMemberRepository, GroupMemberRepository>();
        }

        public static IServiceCollection AddServicesDI(this IServiceCollection services)
        {
            return services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IPersonalMessageService, PersonalMessageService>()
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<IGroupService, GroupService>()
                .AddScoped<IGroupMessageService, GroupMessageService>();
        }
    }
}
