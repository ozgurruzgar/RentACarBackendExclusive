using Hangfire;
using System.Diagnostics;

namespace WebAPI.BackgroundJobs
{
    public class RecurringJob
    {
        public static void ReportingJob()
        {
            Hangfire.RecurringJob.AddOrUpdate("reportjob1", () => EmailReport(), Cron.Monthly);
        } 

        public static void EmailReport()
        {
            Debug.WriteLine("Rapor", "Ay sonu raporu maile iletilmiştir.");
        }
    }
}
