using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseKnowledgeDemo.Models
{
    public class AppOptions : IOptions<AppOptions>
    {
        public AppOptions Value => new AppOptions();

        public string Option { get; set; }
    }
}
