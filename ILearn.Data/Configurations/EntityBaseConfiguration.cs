namespace ILearn.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using ILearn.Entities;

    public class EntityBaseConfiguration<T> : EntityTypeConfiguration<T> where T : class, IBaseEntity
    {
        public EntityBaseConfiguration()
        {
            HasKey(e => e.ID);
        }
    }
}
