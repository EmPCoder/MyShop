﻿using System;

namespace MyShop.Core.Models
{
    public abstract class BaseEntity //Abstract cannot create entity of class in its own right, classes can only implement
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
