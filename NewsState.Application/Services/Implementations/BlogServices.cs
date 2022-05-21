using System.Security.Cryptography.X509Certificates;
using MarketPlace.Application.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewsState.Application.Extensions;
using NewsState.Application.Services.interfaces;
using NewsState.DataLayer.Dtos;
using NewsState.DataLayer.Entities;
using NewsState.DataLayer.Repository;

namespace NewsState.Application.Services.Implementations
{
    public class BlogServices : IBlogServices
    {
        #region ctor
        private readonly IGenericRepository<Post> _postRepository;
        private readonly IGenericRepository<Tag> _tagRepository;

        public BlogServices(IGenericRepository<Post> postRepository, IGenericRepository<Tag> tagRepository)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
        }
        #endregion

        #region post
        public async Task<CreatePostResult> CreatePost(CreatePostDto postDto, IFormFile image)
        {
            try
            {
                var newPost = new Post
                {
                    IsActive = true,
                    PostText = postDto.PostText,
                    ReadTime = postDto.ReadTime,
                    TagId = postDto.TagId,
                    Title = postDto.Title,
                    Writer = postDto.Writer
                };

                if (image != null && image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                    var res = image.AddImageToServer(imageName, PathExtension.PostOriginServer, 400, 400,
                        PathExtension.PostThumbServer);
                    if (res)
                    {
                        newPost.ImageName = imageName;
                        await _postRepository.AddEntity(newPost);
                        await _postRepository.SaveChanges();
                        return CreatePostResult.Success;
                    }

                }
                return CreatePostResult.ImageIsNotValid;
            }
            catch (Exception e)
            {
                return CreatePostResult.Error;
            }
        }

        public async Task<List<ShowShortPostDto>> GetLastPost()
        {
            return await _postRepository.GetQuery().AsQueryable()
                .Include(x => x.Tag)
                .Where(x => !x.IsDelete && x.IsActive)
                .OrderByDescending(x => x.CreateDate)
                .Take(3).Select(x => new ShowShortPostDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    ReadTime = x.ReadTime,
                    ImageName = x.ImageName,
                    Date = x.CreateDate.ToStringShamsiDate(),
                    TagName = x.Tag.TagName
                }).ToListAsync();
        }

        public async Task<ShowShortPostDto> GetShortPost()
        {
            var minTime = await _postRepository.GetQuery().AsQueryable()
                .MinAsync(x => x.ReadTime);
            var post = await _postRepository.GetQuery()
                .Include(x => x.Tag)
                .AsQueryable()
                .Where(x => !x.IsDelete && x.IsActive)
                .OrderByDescending(x => x.CreateDate)
                .FirstOrDefaultAsync(x=>x.ReadTime == minTime);
            if (post == null) return null;
            return new ShowShortPostDto
            {
                Id = post.Id,
                ReadTime = post.ReadTime,
                TagName = post.Tag.TagName,
                Title = post.Title,
                Date = post.CreateDate.ToStringShamsiDate(),
                ImageName = post.ImageName,
            };

        }

        public async Task<List<ReadPostDto>> GetOlderPosts()
        {
            return await _postRepository.GetQuery().AsQueryable()
                .Include(x => x.Tag)
                .Where(x => !x.IsDelete && x.IsActive)
                .OrderBy(x => x.CreateDate)
                .Take(4)
                .Select(x => new ReadPostDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    ReadTime = x.ReadTime,
                    ImageName = x.ImageName,
                    Date = x.CreateDate.ToStringShamsiDate(),
                    PostText = x.PostText
                }).ToListAsync();
        }

        public async Task<ReadPostDto> GetPost(long postId)
        {
            var post = await _postRepository.GetEntityById(postId);
            if(post == null || !post.IsActive) return null;
            return new ReadPostDto
            {
                Id = post.Id,
                ReadTime = post.ReadTime,
                ImageName = post.ImageName,
                PostText = post.PostText,
                Title = post.Title,
                Writer = post.Writer,
                Date = post.CreateDate.ToStringShamsiDate(),
            };
        }

        #endregion

        #region Tag
        public async Task<CreateTagResult> CreateTag(CreateTagDto tag, IFormFile image)
        {
            try
            {
                var newTag = new Tag();
                newTag.TagName = tag.TagName;

                if (image != null && image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                    var res = image.AddImageToServer(imageName, PathExtension.TagOriginServer, 400, 400,
                        PathExtension.TagOriginServer);
                    if (res)
                    {
                        newTag.ImageName = imageName;
                        await _tagRepository.AddEntity(newTag);
                        await _tagRepository.SaveChanges();
                        return CreateTagResult.Success;
                    }
                }
                return CreateTagResult.ImageIsNotValid;
            }
            catch (Exception e)
            {
                return CreateTagResult.Error;
            }
        }

        public async Task<List<Tag>> ListTags()
        {
            return await _tagRepository.GetQuery().AsQueryable().Where(x => !x.IsDelete).ToListAsync();
        }


        #endregion

        #region dispose
        public async ValueTask DisposeAsync()
        {
            await _postRepository.DisposeAsync();
            await _tagRepository.DisposeAsync();
        }

        #endregion
    }
}
