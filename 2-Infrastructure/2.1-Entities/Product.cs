using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WishList.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id é obrigatório")]
        [Display(Name = "Id")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Favor informar um nome entre 10 e 250 caracteres")]
        public string Name { get; set; }

        public ICollection<WishLst> Wishes { get; set; }
    }
}