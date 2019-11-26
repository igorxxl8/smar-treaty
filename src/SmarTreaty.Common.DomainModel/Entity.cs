namespace SmarTreaty.Common.DomainModel
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
    }
}