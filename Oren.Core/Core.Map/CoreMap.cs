using Oren.Core.Core.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Core.Core.Map
{
   public class CoreMap<T>: EntityTypeConfiguration<T>where T:CoreEntity
    {

        public CoreMap()
        {
            Property(x => x.ID).HasColumnName("Id").HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity).HasColumnOrder(1);
            Property(x => x.AddedDate).HasColumnType("datetime2").IsOptional();
        }


    }
}
