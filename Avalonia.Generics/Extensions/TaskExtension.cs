using Avalonia.Threading;

namespace Avalonia.Generics.Extensions
{
    public static class TaskExtension
    {
        /// <summary>
        /// Synchronously awaits a <see cref="Task"/> function on the UI thread
        /// </summary>
        /// <param name="task"></param>
        public static void Await(this Task task)
        {
            using var source = new CancellationTokenSource();
            task.ContinueWith(t => source.Cancel(), TaskScheduler.FromCurrentSynchronizationContext());
            Dispatcher.UIThread.MainLoop(source.Token);
        }
    }
}
