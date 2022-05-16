using Microsoft.AspNetCore.Http;
using NewsState.DataLayer.Dtos;
using NewsState.DataLayer.Entities;

namespace NewsState.Application.Services.interfaces
{
    public interface IBlogServices : IAsyncDisposable
    {
        Task<CreatePostResult> CreatePost(CreatePostDto postDto, IFormFile image);
        Task<CreateTagResult> CreateTag(CreateTagDto tag, IFormFile image);
        Task<List<Tag>> ListTags();
    }
}
