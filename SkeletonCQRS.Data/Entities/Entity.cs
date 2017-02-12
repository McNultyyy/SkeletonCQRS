using System;

namespace SkeletonCQRS.Data.Entities
{
    public class BaseEntity { }

    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }

    public class Entity : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}