﻿using Lugat.Models.Foundations.Bolims;

namespace Lugat.Models.Foundations.Categories
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Star { get; set; }
        public List<Bolim>? Bolimlar { get; set; }
    }
}
