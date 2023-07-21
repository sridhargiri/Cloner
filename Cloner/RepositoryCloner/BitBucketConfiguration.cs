using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryCloner
{

    /// <summary>
    /// A BitBucketConfiguration model.
    /// </summary>
    public class BitBucketConfiguration
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the SourceRepositoryUrl.
        /// </summary>
        /// <value>The SourceRepositoryUrl.</value>
        public string SourceRepositoryUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the DestinationRepositoryUrl.
        /// </summary>
        /// <value>The DestinationRepositoryUrl.</value>
        public string DestinationRepositoryUrl { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Bitbucket Account User Name .
        /// </summary>
        /// <value>The UserUsername.</value>
        public string UserUsername { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Bitbucket Account User Password .
        /// </summary>
        /// <value>The UserPassword.</value>
        public string UserPassword { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Bitbucket Account public User .
        /// </summary>
        /// <value>The UserName.</value>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Bitbucket Account user email .
        /// </summary>
        /// <value>The User Email.</value>
        public string UserEmail { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Local Path For SourceRepository.
        /// </summary>
        /// <value>The LocalPathForSourceRepository.</value>
        public string LocalPathForSourceRepository { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Local Path For DestinationRepository.
        /// </summary>
        /// <value>The LocalPathForDestinationRepository.</value>
        public string LocalPathForDestinationRepository { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Source Repository Branch.
        /// </summary>
        /// <value>The SourceRepositoryBranch.</value>
        public string SourceRepositoryBranch { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Destination Repository Branch.
        /// </summary>
        /// <value>The DestinationRepositoryBranch.</value>
        public string DestinationRepositoryBranch { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Commit Meaasage.
        /// </summary>
        /// <value>The CommitMeaasage.</value>
        public string CommitMeaasage { get; set; } = null!;

        #endregion

    }
}
