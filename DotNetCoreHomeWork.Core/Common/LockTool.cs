using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DotNetCoreHomeWork.Core.Common
{
    public static class LockTool
    {
        private static ReaderWriterLockSlim dataLock = new ReaderWriterLockSlim();

        public static void WriteLockAction(Action action)
        {
            try
            {
                dataLock.EnterWriteLock();
                action();
            }
            finally
            {
                dataLock.ExitWriteLock();
            }
        }

        public static T ReadLockFunc<T>(Func<T> func) where T : class
        {
            try
            {
                dataLock.EnterReadLock();
                return func();
            }
            finally
            {
                dataLock.ExitReadLock();
            }
        }
    }
}
