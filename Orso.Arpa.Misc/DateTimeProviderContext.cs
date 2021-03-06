using System;
using System.Collections.Generic;
using System.Threading;

namespace Orso.Arpa.Misc
{
    /// <summary>
    /// https://codopia.wordpress.com/2017/04/24/how-to-mock-up-datetime-now-in-unit-tests-using-ambient-context-pattern/
    /// </summary>
    public class DateTimeProviderContext : IDisposable
    {
        private static readonly ThreadLocal<Stack<DateTimeProviderContext>> ThreadScopeStack
            = new ThreadLocal<Stack<DateTimeProviderContext>>(() => new Stack<DateTimeProviderContext>());
        public DateTime ContextDateTimeUtcNow;

        public DateTimeProviderContext(DateTime contextDateTimeUtcNow)
        {
            ContextDateTimeUtcNow = contextDateTimeUtcNow;
            ThreadScopeStack.Value.Push(this);
        }
        public static DateTimeProviderContext Current
        {
            get
            {
                if (ThreadScopeStack.Value.Count == 0)
                {
                    return null;
                }
                return ThreadScopeStack.Value.Peek();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if(ThreadScopeStack.Value.Count > 0)
            {
                ThreadScopeStack.Value.Pop();
            }
        }

        #endregion
    }
}
