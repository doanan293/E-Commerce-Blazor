﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApp.Domain.Entities {
    public class Product {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Category? Category { get; set; }
        public Guid CategoryId { get; set; }
    }
}
