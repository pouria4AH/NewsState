using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NewsState.DataLayer.Dtos
{
    public class CreateTagDto
    {

        [Display(Name = "نام تگ")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TagName { get; set; }
        //[Display(Name = " عکس")]
        //[MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        //[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //public IFormFile Image { get; set; }
    }

    public enum CreateTagResult
    {
        Success,
        Error,
        ImageIsNotValid
    }
}
