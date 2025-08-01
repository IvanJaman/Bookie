﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookie.Domain.Entities;

namespace Bookie.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
