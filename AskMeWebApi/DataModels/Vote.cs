using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace AskMeWebApi.DataModels
{
    public class Vote
    {
        [BsonId]
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRequired]
        public Guid QuestionId { get; set; }

        [BsonRequired]
        public Guid AnswerId { get; set; }
    }
}
