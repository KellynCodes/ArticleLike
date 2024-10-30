using System.ComponentModel.DataAnnotations;

namespace ArticleLike.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int LikeCount { get; set; } = 0;
        public Article Article { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }

}
