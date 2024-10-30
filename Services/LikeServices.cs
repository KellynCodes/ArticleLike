using ArticleLike.Context;
using ArticleLike.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ArticleLike.Services
{
    public class LikeService : ILikeService
    {
        private readonly AppDbContext _context;
        private readonly IDistributedCache _cache;
        public LikeService(AppDbContext context, IDistributedCache cache)
        {
            _cache = cache;
            _context = context;
        }


        public async Task<int> GetLikeCountAsync(int articleId)
        {
            string cacheKey = $"like_count_{articleId}";
            string cachedCount = await _cache.GetStringAsync(cacheKey);

            if (cachedCount != null)
            {
                return int.Parse(cachedCount);
            }

            Like? like = await GetLikeByArticleIdAsync(articleId);
            int likeCount = like?.LikeCount ?? 0;

            await _cache.SetStringAsync(cacheKey, likeCount.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return likeCount;
        }
        public async Task<Like?> GetLikeByArticleIdAsync(int articleId)
        {
            return await _context.Likes.FirstOrDefaultAsync(l => l.ArticleId == articleId);
        }

        public async Task IncrementLikeAsync(int articleId)
        {
            Like? like = await GetLikeByArticleIdAsync(articleId);
            if (like == null)
            {
                like = new Like { ArticleId = articleId, LikeCount = 1 };
                _context.Likes.Add(like);
            }
            else
            {
                like.LikeCount++;
            }

            await _context.SaveChangesAsync();
            await _cache.RemoveAsync($"like_count_{articleId}");
        }

    }
}
