using ArticleLike.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticleLike.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class LikesController : ControllerBase
        {
            private readonly ILikeService _service;

            public LikesController(ILikeService service)
            {
                _service = service;
            }

            [HttpGet("{articleId}")]
            public async Task<IActionResult> GetLikeCount(int articleId)
            {
                int likeCount = await _service.GetLikeCountAsync(articleId);
                return Ok(new { ArticleId = articleId, LikeCount = likeCount });
            }

            [HttpPost("{articleId}")]
            public async Task<IActionResult> IncrementLike(int articleId)
            {
                await _service.IncrementLikeAsync(articleId);
                return Ok(new { Message = "Like incremented successfully." });
            }
        }
}
