using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // private readonly IPostService _postService;
        // private readonly IMapper _mapper;
        // private readonly IUriService _uriService;
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository )
        {
            _postRepository = postRepository;
            //_postService = postService;
            // _mapper = mapper;
            // _uriService = uriService;
        }

        public IPostRepository PostRepository { get; }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var PostDto = posts.Select(x => new PostDto
            {
                PostId = x.PostId,
                Date=x.Date,
                Description=x.Description,
                Image=x.Image,
                UserId=x.UserId
            });
            return Ok(PostDto);
        }

       [HttpGet("id")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            var PostDto =  new PostDto
            {
                PostId = post.PostId,
                Date = post.Date,
                Description = post.Description,
                Image = post.Image,
                UserId = post.UserId
            };
            return Ok(PostDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = new Post 
            {
                Date = (System.DateTime)postDto.Date,
                Description = postDto.Description,
                Image = postDto.Image,
                UserId = postDto.UserId
            };
           
            await _postRepository.InsertPost(post);
            return Ok(post);
        }

    }
}
