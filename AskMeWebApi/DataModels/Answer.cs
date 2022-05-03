using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace AskMeWebApi.DataModels
{
    public class Answer
    {
        [BsonId]
        [Key]
        [Required]
        public Guid AnswerId { get; set; } = Guid.NewGuid();

        [BsonRequired]
        public string Text { get; set; }

        [BsonRequired]
        public Guid QuestionId { get; set; }

        [BsonRequired]
        public bool IsCorrect { get; set; } = false;

    }
}
