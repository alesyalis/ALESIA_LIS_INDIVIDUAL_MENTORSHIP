using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppConfiguration.Extentions
{
    public static class CanselExtention
    {
        public static async Task CancelOnError(this Task task, CancellationTokenSource cts)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch
            {
                cts.Cancel();

                throw;
            }
        }

        public static async Task<TTaskResult> CancelOnError<TTaskResult>(this Task<TTaskResult> task, CancellationTokenSource cts)
        {
            await ((Task)task).CancelOnError(cts);

            return task.Result;
        }
    }
}
