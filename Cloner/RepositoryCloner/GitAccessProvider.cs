using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryCloner
{
    /// <summary>
    /// A IGitAccessProvider .
    /// </summary>
    /// <seealso cref="IGitAccessProvider"/>
    public class GitAccessProvider:IGitAccessProvider
    {
        #region Private Vriables
        private readonly IConfigurationRoot _configurationRoot;
        private readonly BitBucketConfiguration _configuration;
        private readonly CredentialsHandler _credHandler;
        private readonly ILogger<GitAccessProvider> _logger;
        private readonly IHelper _helper;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GitAccessProvider"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="loggerFactory">The context.</param>
        public GitAccessProvider(ILoggerFactory loggerFactory, IHelper helper)
        {
            this._logger = loggerFactory.CreateLogger<GitAccessProvider>();
            this._helper = helper ?? throw new ArgumentNullException(nameof(helper));
            this. _configurationRoot = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();
            var bitBucketConfigurationInfo = _configurationRoot.GetRequiredSection(nameof(BitBucketConfiguration));
            this._configuration = bitBucketConfigurationInfo.Get<BitBucketConfiguration>();
            var creds = new UsernamePasswordCredentials()
            {
                Username = this._configuration?.UserUsername,
                Password = this._configuration?.UserPassword

            };

            this._credHandler = (_urls, _users, _cred) => creds;
            _helper = helper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the cloning process from one Repository to another Repository.
        /// </summary>
        public Task StartProcess()
        {
            try
            {
                bool dirExists = Directory.Exists(this._configuration.LocalPathForSourceRepository);
                if (dirExists)
                {
                    _helper.DeleteDirectory(this._configuration.LocalPathForSourceRepository);
                }
                Directory.CreateDirectory(this._configuration.LocalPathForSourceRepository);
                var cloneOptions = new CloneOptions { BranchName =this. _configuration.SourceRepositoryBranch, Checkout = true, CredentialsProvider =this. _credHandler };
                var cloneResult = Repository.Clone(this._configuration.SourceRepositoryUrl, this._configuration.LocalPathForSourceRepository, cloneOptions);
                _helper.DeleteDirectory(cloneResult);
                _helper.DeleteDirectory(this._configuration?.LocalPathForSourceRepository + "/.github");
                bool newdirExists = Directory.Exists(this._configuration?.LocalPathForDestinationRepository);
                if (newdirExists)
                {
                    _helper.DeleteDirectory(this._configuration.LocalPathForDestinationRepository);
                }
                Directory.CreateDirectory(this._configuration.LocalPathForDestinationRepository);
                // initialize a test repository.
                var testRepositoryPath = Repository.Init(this._configuration.LocalPathForDestinationRepository);
                // add sample content to the test repository and push it to origin.
                using (var repo = new Repository(testRepositoryPath))
                {
                   _helper.DeepCopy(new DirectoryInfo(this._configuration.LocalPathForSourceRepository), this._configuration.LocalPathForDestinationRepository);

                    //Directory.Move(_localPathForRepository, _newlocalPathForRepository);
                    // stage all the working directory changes.
                    Commands.Stage(repo, "*");

                    // commit the staged changes.
                    var author = new Signature(this._configuration.UserEmail, this._configuration.UserEmail, DateTimeOffset.Now);
                    var committer = author;
                    var commit = repo.Commit(this._configuration.CommitMeaasage, author, committer);

                    // add the origin remote that points to the test remote repository.
                    var remote = repo.Network.Remotes.Add("origin", this._configuration.DestinationRepositoryUrl);

                    // push the master branch to the origin remote repository.
                    var pushOptions = new PushOptions
                    {
                        CredentialsProvider = _credHandler
                    };
                    var branches = repo.Branches;
                    repo.CreateBranch(_configuration?.DestinationRepositoryBranch);
                    repo.Network.Push(remote, @"refs/heads/" + _configuration?.DestinationRepositoryBranch, pushOptions);
                }

               this. _logger.LogDebug("Repository cloning from Repository "+this._configuration?.SourceRepositoryUrl+" to Repository "+this._configuration?.DestinationRepositoryUrl+ "has been successfully completed.");
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error {ex.Message}", ex);
            }
            finally 
            {
                _helper.DeleteDirectory(this._configuration.LocalPathForSourceRepository);
                _helper.DeleteDirectory(this._configuration.LocalPathForDestinationRepository);

            }

            return Task.CompletedTask;
        }
        #endregion
    }
}
