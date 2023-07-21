using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryCloner
{
    /// <summary>
    /// A IGitAccessProvider interface.
    /// </summary>
    public interface IGitAccessProvider
    {
        //inherited
      public Task StartProcess();
    }
}
