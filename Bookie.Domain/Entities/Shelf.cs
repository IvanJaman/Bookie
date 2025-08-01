﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookie.Domain.Entities
{
    public class Shelf
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<ShelfBook> ShelfBooks { get; set; } = new List<ShelfBook>();
    }
}
