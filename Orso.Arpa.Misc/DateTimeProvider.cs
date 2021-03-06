using System;

namespace Orso.Arpa.Misc
{
    public class DateTimeProvider
    {
        #region Singleton
        private static readonly Lazy<DateTimeProvider> _lazyInstance = new Lazy<DateTimeProvider>(() => new DateTimeProvider());
        private DateTimeProvider()
        {
        }
        public static DateTimeProvider Instance
        {
            get
            {
                return _lazyInstance.Value;
            }
        }
        #endregion

        private readonly Func<DateTime> _defaultCurrentFunction = () => DateTime.UtcNow;

        public DateTime GetUtcNow()
        {
            if (DateTimeProviderContext.Current == null)
            {
                return _defaultCurrentFunction.Invoke();
            }
            else
            {
                return DateTimeProviderContext.Current.ContextDateTimeUtcNow;
            }
        }
    }
}
