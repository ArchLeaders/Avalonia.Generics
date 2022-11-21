using Avalonia.Threading;

namespace Avalonia.Generics.Extensions
{
    public static class TaskExtension
    {
        public static void RunSync(this Task task)
        {
            using var source = new CancellationTokenSource();
            task.ContinueWith(t => source.Cancel(), TaskScheduler.FromCurrentSynchronizationContext());
            Dispatcher.UIThread.MainLoop(source.Token);
        }
    }
}
