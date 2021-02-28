using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Orso.Arpa.FakeSmtp
{
    /// <summary>
    /// Starts fakeSMTP-2.0.jar
    /// </summary>
    internal class Program
    {
        private static readonly string s_mailDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "mails");
        private static Process s_process = new Process();

        private static async Task Main(string[] args)
        {
            var done = new ManualResetEventSlim(false);

            using var shutdownCts = new CancellationTokenSource();
            try
            {
                AttachCtrlcSigtermShutdown(shutdownCts, done);
                StartFakeSmtpServer();
                Console.WriteLine("Application is running. Press Ctrl+C to shut down.");
                await Task.Delay(Timeout.Infinite, shutdownCts.Token);
                Console.WriteLine("Application is shutting down...");
                OnProcessExit();
            }
            catch (TaskCanceledException) { }
            finally
            {
                done.Set();
            }
        }

        private static void StartFakeSmtpServer()
        {
            var jarCommand = $"-jar {Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "fakeSMTP-2.0.jar")} -s -p 2500 -a 127.0.0.1 -o {s_mailDirectory}";

            s_process.StartInfo.FileName = "java";
            s_process.StartInfo.Arguments = jarCommand;
            s_process.Start();
        }

        private static void AttachCtrlcSigtermShutdown(CancellationTokenSource cts, ManualResetEventSlim resetEvent)
        {
            void Shutdown()
            {
                try
                {
                    OnProcessExit();
                    cts.Cancel();
                }
                catch (ObjectDisposedException)
                {
                }
                resetEvent.Wait();
            };

            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => Shutdown();
            s_process.Exited += (sender, eventArgs) => Shutdown();
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                Shutdown();
                // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
                eventArgs.Cancel = true;
            };
        }

        private static void OnProcessExit()
        {
            var di = new DirectoryInfo(s_mailDirectory);

            if (di.Exists)
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            if (s_process != null)
            {
                s_process.Close();
                s_process.Dispose();
                s_process = null;
            }
        }
    }
}
