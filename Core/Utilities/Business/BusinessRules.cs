using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics)
            {
                if(!logic.Success)
                {
                    return logic;
                }
            }
            return null;
        }

        public static async Task<IResult> RunTasks(params Task<IResult>[] logics)
        {
            foreach (var logic in logics)
            {
                var tasks = await logic;
                if (!tasks.Success)
                {
                    return tasks;
                }
            }
            return null;
        }
    }
}
