using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzDemo
{
    public class ScheduledJob : IJob
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<ScheduledJob> logger;

        public ScheduledJob(IConfiguration configuration, ILogger<ScheduledJob> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            logger.LogInformation($"定时任务，当前时间：{DateTime.Now.ToLongTimeString()}");

            await Task.CompletedTask;
        }
    }
}
