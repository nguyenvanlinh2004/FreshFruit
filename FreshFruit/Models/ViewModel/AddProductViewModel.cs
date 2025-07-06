using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreshFruit.Models.ViewModel
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string? Description { get; set; }
        public string? LongDescription { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh chính")]
        [Display(Name = "Ảnh chính")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Ảnh con (nhiều)")]
        public List<IFormFile>? ImageFiles { get; set; }
    }
}
