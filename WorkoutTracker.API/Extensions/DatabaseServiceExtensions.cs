using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Repository;
using WorkoutTracker.Domain.Extensions;
using WorkoutTracker.Domain.Repositories.Interfaces;
using WorkoutTracker.Domain.Services;
using WorkoutTracker.Domain.Services.Interfaces;

namespace WorkoutTracker.API.Extensions
{
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection ManageDatabaseServices(this IServiceCollection services, IConfiguration config)
        {
            var configDatabaseType = config.GetValue(typeof(string), "DatabaseType").ToString();
            if (configDatabaseType == "Memory")
            {
                services.AddTransient<IInMemoryWorkoutRepository, InMemoryWorkoutRepository>();
            }
            else if (configDatabaseType == "SQL")
            {
                services.AddDbContext<WorkoutDbContext>(
                    o => o.UseSqlServer(config.GetConnectionString("SqlServer")));
                services.AddTransient<IWorkoutRepository, WorkoutRepository>();
                services.AddTransient<IEmailService, EmailService>();
                services.AddTransient<IWorkoutService, WorkoutService>();
                services.AddTransient<IAccountService, AccountService>();
                services.AddIdentityServices(config);
            }
            return services;
        }
    }
}
