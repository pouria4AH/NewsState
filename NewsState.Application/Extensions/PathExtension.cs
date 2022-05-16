namespace NewsState.Application.Extensions
{
    public class PathExtension
    {
        public static string PostOrigin = "/Content/Images/Post/origin/";
        public static string PostOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Post/origin/");

        public static string PostThumb = "/Content/Images/P" +
            "ost/Thumb/";
        public static string PostThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/P" +
            "ost/Thumb/"); 
        
        public static string TagOrigin = "/Content/Images/Tag/origin/";
        public static string TagOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Tag/origin/");

        public static string TagThumb = "/Content/Images/Tag/Thumb/";
        public static string TagThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/Tag/Thumb/");
    }
}
