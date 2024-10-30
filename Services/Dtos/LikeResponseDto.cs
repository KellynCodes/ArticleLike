namespace ArticleLike.Services.Dtos
{
    public class LikeResponseDto
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int LikeCount { get; set; } = 0;
    }
}
