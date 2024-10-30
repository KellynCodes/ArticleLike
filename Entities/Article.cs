using System.ComponentModel.DataAnnotations;

namespace ArticleLike.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
