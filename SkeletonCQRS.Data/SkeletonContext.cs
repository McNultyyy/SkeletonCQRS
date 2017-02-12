using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonCQRS.Data
{
    public class SkeletonContext : DbContext
    {
        public SkeletonContext()
        {
            Database.SetInitializer<SkeletonContext>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            var typesToRegister = GetType().Assembly.GetTypes()
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null &&
                    type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)
            );

            foreach (var type in typesToRegister)
            {
                dynamic instance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(instance);
            }
        }
    }
}
