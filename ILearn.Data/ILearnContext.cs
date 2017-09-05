namespace ILearn.Data
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using ILearn.Entities;

    public class ILearnContext : DbContext
    {
        public ILearnContext() : base("ILearn")
        {
            Database.SetInitializer<ILearnContext>(null);
        }

        #region Entity Sets
        
        public IDbSet<QuesList> QuesListSet { get; set; }

        public IDbSet<Topic> TopicSet { get; set; }

        public IDbSet<ExternalResource> ExternalResourceSet { get; set; }

        public IDbSet<MultipleChoice> MultipleChoiceSet { get; set; }

        public IDbSet<TutorialTitle> TutorialTitleSet { get; set; }

        public IDbSet<TutorialSubtitle> TutorialSubtitleSet { get; set; }

        public IDbSet<TutorialContent> TutorialContentSet { get; set; }

        #endregion

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
