using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
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

        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            return Ok(posts);
        }
    }
}
