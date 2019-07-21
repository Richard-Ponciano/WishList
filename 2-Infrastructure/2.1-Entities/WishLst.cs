using System.ComponentModel.DataAnnotations;

namespace WishList.Entities
{
    public class WishLst
    {
        [Key]
        [Required(ErrorMessage = "UserId é obrigatório")]
        public int UserId { get; set; }
        public User User { get; set; } = null;

        [Key]
        [Required(ErrorMessage = "ProductId é obrigatório")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null;
    }
}