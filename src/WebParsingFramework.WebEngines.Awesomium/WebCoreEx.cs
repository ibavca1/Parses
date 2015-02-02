using System;
using System.Threading;
using Awesomium.Core;

namespace WebParsingFramework.WebEngines.Awesomium
{
    internal static class WebCoreEx
    {
        public static void Execute(Action action)
        {
            var evnt = new AutoResetEvent(false);
            WebCore.QueueWork(
                () =>
                    {
                        action();
                        evnt.Set();
                    });
            evnt.WaitOne();
        }

        public static T Execute<T>(Func<T> func)
        {
            T result = default(T);
            var evnt = new AutoResetEvent(false);
            WebCore.QueueWork(
                () =>
                    {
                        result = func();
                        evnt.Set();
                    });

            evnt.WaitOne();
            return result;
        }
    }
}