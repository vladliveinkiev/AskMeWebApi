using AskMeWebApi.DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AskMeWebApi.Dtos
{
    public class QuestionDto
    {
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public List<AnswerDto> Answers { get; set; }
        [Required]
        public QuestionType QuestionType { get; set; }
    }
}
