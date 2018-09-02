using System;

namespace BetaViews.Core.DataBase.ORM
{
    public class ServiceBase : IDisposable
    {
        internal readonly DataBaseContext DataContext;

        public ServiceBase()
        {
            DataContext = new DataBaseContext();
        }

        public void Dispose()
        {
            if (DataContext != null) DataContext.Dispose();
        }
    }
}
