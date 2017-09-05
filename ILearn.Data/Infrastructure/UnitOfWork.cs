namespace ILearn.Data.Infrastructure
{
    using System.Data.Entity.Validation;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private ILearnContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public ILearnContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            try
            {
                DbContext.Commit();
            }
            catch (DbEntityValidationException e)
            {
                throw new FormattedDbEntityValidationException(e);
            }
        }
    }
}