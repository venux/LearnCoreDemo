using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseKnowledgeDemo.DependencyInjectionDemo
{
    public class MyDependency : IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        public MyDependency(ILogger<MyDependency> logger)
        {
            _logger = logger;
        }

        public Task WriteMessage(string message)
        {
            _logger.LogInformation("My Dependency Injection Test。");

            return Task.FromResult(0);
        }
    }
}
