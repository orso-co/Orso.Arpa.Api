using System;
using System.Collections;
using System.Threading;

namespace Orso.Arpa.Misc
{
    public class DateTimeProviderContext : IDisposable
    {
        private static readonly ThreadLocal<Stack> ThreadScopeStack = new ThreadLocal<Stack>(() => new Stack());
        public DateTime ContextDateTimeUtcNow;
        private readonly Stack _contextStack = new Stack();

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
                return ThreadScopeStack.Value.Peek() as DateTimeProviderContext;
            }
        }

        #region IDisposable Members
        public void Dispose()
        {
            ThreadScopeStack.Value.Pop();
        }
        #endregion
    }
}
