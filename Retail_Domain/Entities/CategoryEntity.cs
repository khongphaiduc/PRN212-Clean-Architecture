using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Domain.Entities
{
    // Domain Entity không nên chứa navigation của EF.
    public class CategoryEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public CategoryEntity() { }

        public CategoryEntity(int id, string name, string? description)
        {
            Id = id;
            Name = name;
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
