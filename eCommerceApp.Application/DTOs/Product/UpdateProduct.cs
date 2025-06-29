﻿using System.ComponentModel.DataAnnotations;
using eCommerceApp.Application.DTOs.Category;

namespace eCommerceApp.Application.DTOs.Product {
    public class UpdateProduct : ProductBase {
        [Required]
        public Guid Id { get; set; }
    }
}
