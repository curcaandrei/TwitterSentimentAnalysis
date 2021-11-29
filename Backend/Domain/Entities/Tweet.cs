using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities
{
    [Table("Tweets")]
    public class Tweet : BaseEntity
    {
        public string Date { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public Dictionary<string, float> feels { get; set; }
    }
}