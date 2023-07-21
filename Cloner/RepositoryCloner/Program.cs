using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RepositoryCloner;



ServiceProvider serviceProvider = new ServiceCollection()
    .AddLogging((loggingBuilder) => loggingBuilder
        .SetMinimumLevel(LogLevel.Trace)
        .AddConsole()
        )
    .AddSingleton<IGitAccessProvider, GitAccessProvider>()
    .AddSingleton<IHelper,Helper>()
    .BuildServiceProvider();

var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>(); 

logger.LogDebug("Starting application");

//do the actual work here
var gitProvider = serviceProvider.GetService<IGitAccessProvider>();
gitProvider.StartProcess();

logger.LogDebug("Repository cloning task successfully completed");
