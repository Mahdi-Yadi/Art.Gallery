using Microsoft.Extensions.Options;
using Quartz;
namespace Art.Gallery.Core.Services.Jobs.UserLogin;
public class RemoveLoginSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(RemoveLoginJob));

        options.AddJob<RemoveLoginJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger =>
                trigger.ForJob(jobKey)
                    .WithSimpleSchedule(schedule => schedule
                        .WithIntervalInHours(1)
                        .RepeatForever()));
    }
}