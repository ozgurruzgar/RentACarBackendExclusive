using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IEmailSender
    {
        Task Sender(string mail, string message);
    }
}
