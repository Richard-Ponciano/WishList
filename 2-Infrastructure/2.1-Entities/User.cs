using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WishList.Helpers;

namespace WishList.Entities
{
    public class User
    {
        #region Construtores

        #endregion

        #region Propriedades
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id é obrigatório")]
        [Display(Name = "Id")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Favor informar um nome entre 10 e 250 caracteres")]
        //[RegularExpression(@"[^\#\\\|\,\<\>\.\;\:\]\[\}\{\~\^\´\`\=\+\-\_\)\(\*\&\¨\%\$\#\@\!\'\""\ç\Ç\ª\º\¹\²\³\£\¢\¬]+]", ErrorMessage = "Favor informar o nome sem caracteres especiais")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [Display(Name = "email")]
        //[RegularExpression(@"^[a-z0-9(\.\_\-)?]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        public ICollection<WishLst> Wishes { get; set; }
        #endregion

        #region Métodos

        #endregion
    }
}