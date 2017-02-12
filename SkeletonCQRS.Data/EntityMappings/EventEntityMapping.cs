using System.Data.Entity.ModelConfiguration;
using SkeletonCQRS.Data.Entities;

namespace SkeletonCQRS.Data.EntityMappings
{
    public class EventEntityMapping : EntityTypeConfiguration<EventEntity>
    {
        public EventEntityMapping()
        {
            ToTable("Event");

            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("EventId");

            Property(x => x.TimeStamp);

            Property(x => x.AggregateId);

            Property(x => x.EventName);

            Property(x => x.EventData).HasColumnType("xml");
        }
    }
}