using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryCloner
{
    /// <summary>
    /// A Repository Cloner Helper class.
    /// </summary>
    public class Helper : IHelper
    {
        #region Private Vriables

        private readonly ILogger<GitAccessProvider> _logger;
        #endregion

        #region Constructor


        /// <summary>
        /// Initializes a new instance of the <see cref="Helper"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="loggerFactory">The context.</param>
        public Helper(ILoggerFactory loggerFactory) 
        {
            this._logger = loggerFactory.CreateLogger<GitAccessProvider>();
        }
        #endregion

        /// <summary>
        /// A Directory deletion method.
        /// </summary>
        /// <param name="target_dir">The traget directory.</param>
        public void DeleteDirectory(string target_dir)
        {
            try
            {
                string[] files = Directory.GetFiles(target_dir);
                string[] dirs = Directory.GetDirectories(target_dir);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(target_dir, false);

            }
            catch(Exception ex) 
            {
                this._logger.LogError($"Error {ex.Message}", ex);
            }
        }

        /// <summary>
        /// A copy method for a complete Directory.
        /// </summary>
        /// <param name="directory">The traget directory.</param>
        /// /// <param name="destinationDir">The traget directory.</param>
        public void DeepCopy(DirectoryInfo directory, string destinationDir)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(directory.FullName, "*", SearchOption.AllDirectories))
                {
                    string dirToCreate = dir.Replace(directory.FullName, destinationDir);
                    Directory.CreateDirectory(dirToCreate);
                }

                foreach (string newPath in Directory.GetFiles(directory.FullName, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(directory.FullName, destinationDir), true);
                }
            }
            catch (Exception ex) 
            {
                this._logger.LogError($"Error {ex.Message}", ex);
            }
        }
    }
}
