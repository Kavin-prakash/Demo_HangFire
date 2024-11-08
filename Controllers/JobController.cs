using Demo_Practice_Hangfire.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Practice_Hangfire.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    public JobController()
    {

    }
    [HttpPost]
    [Route("CreateBackGroundJob")]
    public IActionResult CreateBackGroundJob()
    {
        // BackgroundJob.Enqueue(() => System.Console.WriteLine("Back Ground Job is Triggered"));
        BackgroundJob.Enqueue<TestJobs>(x => x.WriteLog("BackGround Job is Triggred"));
        return Ok();
    }

    [HttpPost]
    [Route("CreateScheduledJob")]
    public IActionResult CreateScheduledJob()
    {
        var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);

        var datetimeoffset = new DateTimeOffset(scheduleDateTime);

        // BackgroundJob.Schedule(() => System.Console.WriteLine("Scheduled Job is Triggred"), datetimeoffset);
        BackgroundJob.Schedule<TestJobs>((x) => x.WriteLog("Scheduled Job is Triggred"), datetimeoffset);

        return Ok();
    }

    [HttpPost]
    [Route("CreateContinuationJob")]
    public IActionResult CreateContinuationJob()
    {

        var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);

        var datetimeoffset = new DateTimeOffset(scheduleDateTime);

        var Job = BackgroundJob.Schedule(() => System.Console.WriteLine("Scheduled 2 Job is Triggred"), datetimeoffset);

        var Job2Id = BackgroundJob.ContinueJobWith(Job, () => System.Console.WriteLine("Continuation Job 1  is Triggred"));
        var Job3Id = BackgroundJob.ContinueJobWith(Job, () => System.Console.WriteLine("Continuation Job 2  is Triggred"));
        var Job4Id = BackgroundJob.ContinueJobWith(Job, () => System.Console.WriteLine("Continuation Job 3  is Triggred"));

        return Ok();
    }

    [HttpPost]
    [Route("CreateRecurringJobs")]
    public IActionResult CreateRecurringJob()
    {

        // RecurringJob.AddOrUpdate("RecurrringJob1", () => System.Console.WriteLine("Recurring Jobs is Scheduled"), "* * * * *");
        RecurringJob.AddOrUpdate<TestJobs>("RecurrringJob1", (x) => x.WriteLog("Recurring Jobs is Scheduled"), "* * * * *");
        return Ok();
    }
}