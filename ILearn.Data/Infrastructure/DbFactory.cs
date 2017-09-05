namespace ILearn.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ILearnContext dbContext;

        public ILearnContext Init()
        {
            return dbContext ?? (dbContext = new ILearnContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
