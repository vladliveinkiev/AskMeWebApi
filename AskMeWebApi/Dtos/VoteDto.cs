using System;
using System.ComponentModel.DataAnnotations;

namespace AskMeWebApi.Dtos
{
    public class VoteDto
    {
        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        public Guid AnswerId { get; set; }
    }
}
