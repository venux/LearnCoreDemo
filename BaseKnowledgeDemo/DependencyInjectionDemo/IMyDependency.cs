using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseKnowledgeDemo.DependencyInjectionDemo
{
    public interface IMyDependency
    {
        Task WriteMessage(string message);
    }
}
