using Quartz;

namespace ErrorQueue.Quartz
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services) {
            services.AddQuartz(options=> {
                options.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create(nameof(ShppingCartJob));

                options.AddJob<ShppingCartJob>(jobKey).AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(
                    schedule => schedule.WithIntervalInSeconds(10).WithRepeatCount(1)
                    )) ;
                
            });
            services.AddQuartzHostedService(options => {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}
