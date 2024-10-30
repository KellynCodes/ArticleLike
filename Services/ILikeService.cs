using ArticleLike.Entities;

namespace ArticleLike.Services
{
    public interface ILikeService
    {
        Task<int> GetLikeCountAsync(int articleId);
        Task<Like?> GetLikeByArticleIdAsync(int articleId);
        Task IncrementLikeAsync(int articleId);
    }
}
