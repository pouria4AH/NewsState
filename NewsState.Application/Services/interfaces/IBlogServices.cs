using Microsoft.AspNetCore.Http;
using NewsState.DataLayer.Dtos;

namespace NewsState.Application.Services.interfaces
{
    public interface IBlogServices : IAsyncDisposable
    {
        Task<CreatePostResult> CreatePost(CreatePost post, IFormFile file);
    }
}
