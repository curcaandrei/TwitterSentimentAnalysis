using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    [Table("Tweets")]
    public class Tweet : BaseEntity
    {
        public string Text { get; set; }
    }
}