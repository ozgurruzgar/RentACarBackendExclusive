using Core.Services;

namespace WebAPI.BackgroundJobs
{
    public class FireAndForgetJob
    {
        public static void EmailSentToUserJob(string mail,string message)
        {
            Hangfire.BackgroundJob.Enqueue<IEmailSender>(x => x.Sender(mail, message));
        }
    }
}
