﻿using System;
namespace ShoppingService.Models
{
	public class Cart
	{
		public Cart()
		{
		}

        public int Id { get; set; }
        public List<Product> Items { get; set; }
    }
}

