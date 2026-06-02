using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Domain.Entities
{
    public class CategoryEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }


        private readonly List<ProductEntity> _products = new();
        public virtual IReadOnlyCollection<ProductEntity> Products => _products.AsReadOnly();


        private CategoryEntity() { }


        public CategoryEntity(string name, string? description)
        {
            UpdateName(name);
            Description = description;
        }


        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Tên danh mục không được để trống.", nameof(newName));

            Name = newName;
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }
    }
}
