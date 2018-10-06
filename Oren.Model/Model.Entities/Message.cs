using Oren.Core.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oren.Model.Model.Entities
{
   public class Message:CoreEntity
    {
        public AppUser Sender { get; set; }

        public AppUser ReceivedBy { get; set; }

        public int SenderId { get; set; }

        public int ReceivederId { get; set; }

        public string Text { get; set; }
        
        public DateTime? LastCheck { get; set; }

    }
}
