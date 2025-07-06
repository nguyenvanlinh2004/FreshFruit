using System.ComponentModel.DataAnnotations;

namespace FreshFruit.Models.ViewModel
{
    public class ProductDetailViewModel
    {
        // Thông tin sản phẩm chính
        public Product? Product { get; set; }

		public string? slug { get; set; }
        // Hình ảnh liên quan đến sản phẩm
        public List<ProductImage>? ProductImages { get; set; }

        // Danh sách đánh giá (Rating) kèm comment
        public List<RatingWithCommentVM>? RatingsWithComments { get; set; }

        // Danh sách sản phẩm cùng loại
        public List<Product>? RelatedProducts { get; set; }
        public double AverageRating { get; set; }

        // Dùng cho form người dùng nhập đánh giá
        [Range(1, 5, ErrorMessage = "Vui lòng chọn số sao từ 1 đến 5")]
        public int RatingInput { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung bình luận")]
        [StringLength(500, ErrorMessage = "Bình luận không vượt quá 500 ký tự")]
        public string? CommentInput { get; set; }

        // Gửi Id sản phẩm trong form POST
        public int ProductId { get; set; }
    }

    // Sub-ViewModel chứa cả Rating và Comment
    public class RatingWithCommentVM
    {
        public int RatingId { get; set; }
        public int? MemberId { get; set; }
        public int ProductId { get; set; }
        public int RatingValue { get; set; } 
        public DateTime CreatedAt { get; set; }
        public int? Status { get; set; }

        public string? CommentText { get; set; }
        public DateTime? CommentCreatedAt { get; set; }
        public int? CommentStatus { get; set; }
		public string? FullName { get; set; }
    }
}
