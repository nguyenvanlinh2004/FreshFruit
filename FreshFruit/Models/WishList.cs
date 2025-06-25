using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace FreshFruit.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AccountID { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual Account Account { get; set; } = null!;
    }
}
