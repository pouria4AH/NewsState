using System.ComponentModel.DataAnnotations;

namespace NewsState.DataLayer.Dtos
{
    public class CreatePost
    {
        public string Title { get; set; }
        [Display(Name = "زمان متن")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(0, int.MaxValue)]
        public int ReadTime { get; set; }
        [Display(Name = "فعال / غبر فعال")]
        public bool IsActive { get; set; }
        [Display(Name = "متن مقاله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PostText { get; set; }

        public long TagId { get; set; }

        [Display(Name = "نویسنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Writer { get; set; }
    }

    public enum CreatePostResult
    {
        Success,
        Error,
        ImageIsNotValid
    }
}
