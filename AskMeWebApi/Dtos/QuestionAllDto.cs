using AskMeWebApi.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi.Dtos
{
    public class QuestionAllDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public List<Answer> Answers { get; set; }

        [Required]
        public QuestionType QuestionType { get; set; }
    }
}
