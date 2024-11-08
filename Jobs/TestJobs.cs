namespace Demo_Practice_Hangfire.Jobs;

public class TestJobs
{

    private readonly ILogger _logger;

    public TestJobs(ILogger<TestJobs> logger)
    {

        _logger = logger;
    }
    public void WriteLog(string logmessage)
    {
        _logger.LogInformation($"{DateTime.Now} ,{logmessage}");
    }
}