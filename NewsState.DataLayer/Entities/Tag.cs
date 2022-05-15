using System.ComponentModel.DataAnnotations;

namespace NewsState.DataLayer.Entities
{
    public class Tag : BaseEntity
    {
        #region props

        [Display(Name = "نام تگ")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TagName { get; set; }
        [Display(Name = "نام عکس")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }

        #endregion

        #region relation
        public ICollection<Post> Posts { get; set; }
        #endregion
    }
}
