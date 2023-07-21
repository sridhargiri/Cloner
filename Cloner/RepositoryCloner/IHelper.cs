using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryCloner
{
    public interface IHelper
    {
        public void DeleteDirectory(string target_dir);
        public void DeepCopy(DirectoryInfo directory, string destinationDir);
    }
}
