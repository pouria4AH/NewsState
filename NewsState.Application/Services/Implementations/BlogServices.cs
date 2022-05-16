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
        public async Task<CreatePostResult> CreatePost(CreatePost post, IFormFile image)
        {
            try
            {
                var newPost = new Post
                {
                    IsActive = true,
                    PostText = post.PostText,
                    ReadTime = post.ReadTime,
                    TagId = post.TagId,
                    Title = post.Title,
                    Writer = post.Writer
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
        #endregion

        #region Tag
        public async Task<CreateTagResult> CreateTag(CreateTagDto tag)
        {
            try
            {
                var newTag = new Tag();
                newTag.TagName = tag.TagName;

                if (tag.Image != null && tag.Image.IsImage())
                {
                    var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(tag.Image.FileName);
                    var res = tag.Image.AddImageToServer(imageName, PathExtension.PostOriginServer, 400, 400,
                        PathExtension.PostThumbServer);
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
