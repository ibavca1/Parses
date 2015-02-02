using System;

namespace WebParsingFramework
{
    public static class Maybe
    {
        public static TResult With<T, TResult>(this T? self, Func<T, TResult> func) where T : struct
        {
            if (!self.HasValue)
            {
                return default(TResult);
            }
            return func(self.Value);
        }

        public static TResult With<T, TResult>(this T self, Func<T, TResult> func) where T : class
        {
            if (self != null)
            {
                return func(self);
            }
            return default(TResult);
        }
    }

}