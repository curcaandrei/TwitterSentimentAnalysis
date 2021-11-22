using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    [Table("Tweets")]
    public class Tweet : BaseEntity
    {
        public string Date { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public List<Feelings> feels { get; set; }
    }
}