using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreshFruit.Models.ViewModel
{
    public class ProductEditVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Danh mục không được để trống")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn hoặc bằng 0")]
        public double? Price { get; set; }

        public string? Description { get; set; }

        public string? Slug { get; set; }

        public string? Image { get; set; } // ảnh cũ

        public IFormFile? ImageFile { get; set; } // ảnh chính mới (nếu có thay đổi)

        public List<string>? OldGalleryImages { get; set; } // ảnh phụ cũ (nếu cần show lại)

        public List<IFormFile>? ImageFiles { get; set; } // ảnh phụ mới (nếu cần thêm mới)
    }
}
