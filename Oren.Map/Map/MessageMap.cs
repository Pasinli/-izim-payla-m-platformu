using Oren.Core.Core.Map;
using Oren.Model.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Map.Map
{
   public class MessageMap:CoreMap<Message>
    {
        public MessageMap()
        {
            ToTable("dbo.Messages");
            Property(x => x.Text).IsOptional();
            Property(x => x.SenderId).IsOptional();
            Property(x => x.ReceivederId).IsOptional();
            Property(x => x.LastCheck).HasColumnType("datetime2");
            Property(x => x.LastCheck).IsOptional();
            HasRequired(x => x.Sender).WithMany(x => x.SendedMessages).HasForeignKey(x => x.SenderId).WillCascadeOnDelete(false);
            HasRequired(x => x.ReceivedBy).WithMany(x => x.ReceivedMessages).HasForeignKey(x => x.ReceivederId).WillCascadeOnDelete(false);

        }

    }
}
