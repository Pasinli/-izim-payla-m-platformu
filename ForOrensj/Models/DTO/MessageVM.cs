using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForOrensj.Models.DTO
{
    public class MessageVM
    {
        public int Id { get; set; }
        public int SenderId { get; set; }

        public int ReceivedById { get; set; }

        public string Sender { get; set; }

        public string Text { get; set; }

    }
}