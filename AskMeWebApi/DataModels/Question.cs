using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace AskMeWebApi.DataModels
{
    public class Question
    {
        [BsonId]
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRequired]
        public string QuestionText { get; set; }

        [BsonRequired]
        public QuestionType QuestionType { get; set; }
    }

    public enum QuestionType
    {
        Poll, //0
        Trivia //1
    }

}
