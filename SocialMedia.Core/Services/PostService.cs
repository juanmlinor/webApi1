using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        /*/  private readonly IUnitOfWork _unitOfWork;
          private readonly PaginationOptions _paginationOptions;

          public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
          {
              _unitOfWork = unitOfWork;
              _paginationOptions = options.Value;
          }

          public async Task<Post> GetPost(int id)
          {
              return await _unitOfWork.PostRepository.GetById(id);
          }

          public PagedList<Post> GetPosts(PostQueryFilter filters)
          {
              filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
              filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

              var posts = _unitOfWork.PostRepository.GetAll();

              if (filters.UserId != null)
              {
                  posts = posts.Where(x => x.UserId == filters.UserId);
              }

              if (filters.Date != null)
              {
                  posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
              }

              if (filters.Description != null)
              {
                  posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
              }

              var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);
              return pagedPosts;
          }

          public async Task InsertPost(Post post)
          {
              var user = await _unitOfWork.UserRepository.GetById(post.UserId);
              if (user == null)
              {
                  throw new BusinessException("User doesn't exist");
              }

              var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
              if (userPost.Count() < 10)
              {
                  var lastPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();
                  if ((DateTime.Now - lastPost.Date).TotalDays < 7)
                  {
                      throw new BusinessException("You are not able to publish the post");
                  }
              }

              if (post.Description.Contains("Sexo"))
              {
                  throw new BusinessException("Content not allowed");
              }

              await _unitOfWork.PostRepository.Add(post);
              await _unitOfWork.SaveChangesAsync();
          }

          public async Task<bool> UpdatePost(Post post)
          {
              var existingPost = await _unitOfWork.PostRepository.GetById(post.Id);
              existingPost.Image = post.Image;
              existingPost.Description = post.Description;

              _unitOfWork.PostRepository.Update(existingPost);
              await _unitOfWork.SaveChangesAsync();
              return true;
          }

          public async Task<bool> DeletePost(int id)
          {
              await _unitOfWork.PostRepository.Delete(id);
              await _unitOfWork.SaveChangesAsync();
              return true;
          }
      }/*/
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> DeletePost(int id)
        {
            return await _postRepository.DeletePost(id);
        }

        public async Task<Post> GetPost(int id)
        {
            return await _postRepository.GetPost(id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        /*/public async Task InsertPost(Post post)
        {
            await _postRepository.InsertPost(post);

        } /*/
        public async Task InsertPost(Post post)
        {
            var user = await _userRepository.GetUser(post.UserId);
            if (user == null)
            {
                throw new Exception("User dont exist");
            }
            if (post.Description.Contains("Sexo"))
            {
                throw new Exception("Content not allowed");
            }
            await _postRepository.InsertPost(post);
        }
        public async Task<bool> UpdatePost(Post post)
        {
          return  await _postRepository.UpdatePost(post);
        }
    }
}
