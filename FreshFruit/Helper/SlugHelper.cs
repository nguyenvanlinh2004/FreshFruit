using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace FreshFruit.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string input)
        {
            input = input.ToLowerInvariant().Trim();

            // Bỏ dấu tiếng Việt
            string normalized = input.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            string slug = sb.ToString()
                .Replace("đ", "d")
                .Replace("Đ", "d");

            // Loại bỏ ký tự không hợp lệ
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Thay thế khoảng trắng bằng dấu gạch ngang
            slug = Regex.Replace(slug, @"\s+", "-").Trim('-');

            return slug;
        }
    }
}
