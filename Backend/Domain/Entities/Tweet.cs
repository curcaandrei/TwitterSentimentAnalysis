using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Domain.Common;
using Microsoft.ML.Data;

namespace Domain.Entities
{
    [Table("Tweets")]
    public class Tweet : BaseEntity
    {
        public string Date { get; set; } = "No text";
        public string User { get; set; } = "No text";
        
        [AllowNull]
        public string Username { get; set; }

        [LoadColumn(1)]
        public string Text { get; set; } = "No text";
        
        [AllowNull]
        public Dictionary<string, float> feels { get; set; }
    }
}