using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzDemo.Extensions
{
    public static class QuartzExtensions
    {
        public static void AddQuart(this IServiceCollection services, Type jobType)
        {
            services.Add(new ServiceDescriptor(typeof(IJob), typeof(ScheduledJob), ServiceLifetime.Transient));
            services.AddSingleton<IJobFactory, ScheduledJobFactory>();
            services.AddSingleton<IJobDetail>(provider =>
            {
                return JobBuilder.Create<ScheduledJob>()
                                .WithIdentity("Sample.job", "group1")
                                .Build();
            });
            services.AddSingleton<ITrigger>(provider =>
            {
                return TriggerBuilder.Create()
                    .WithIdentity($"Sample.trigger", "group1")
                    .StartNow()
                    .WithSimpleSchedule(s =>
                    {
                        s.WithInterval(TimeSpan.FromSeconds(5))
                        .WithRepeatCount(100);
                    })
                    .Build();
            });

            services.AddSingleton<IScheduler>(provider =>
            {
                var schedulerFactory = new StdSchedulerFactory();
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();

                return scheduler;
            });
        }

        public static void UseQuatrz(this IApplicationBuilder app)
        {
            IScheduler scheduler = app.ApplicationServices.GetService<IScheduler>();
            scheduler.ScheduleJob(app.ApplicationServices.GetService<IJobDetail>(), app.ApplicationServices.GetService<ITrigger>());
        }
    }
}
