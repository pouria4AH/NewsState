using Microsoft.AspNetCore.Http;
using NewsState.DataLayer.Dtos;
using NewsState.DataLayer.Entities;

namespace NewsState.Application.Services.interfaces
{
    public interface IBlogServices : IAsyncDisposable
    {
        Task<CreatePostResult> CreatePost(CreatePostDto postDto, IFormFile file);
        Task<CreateTagResult> CreateTag(CreateTagDto tag);
        Task<List<Tag>> ListTags();
    }
}
