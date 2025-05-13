using Art.Gallery.Core.Services.Jobs.UserLogin;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
namespace Art.Gallery.Core.Services.Jobs;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddQuartz();

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureOptions<RemoveLoginSetup>();
    }
}