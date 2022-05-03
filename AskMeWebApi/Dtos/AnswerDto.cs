using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi.Dtos
{
    public class AnswerDto
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public bool IsCorrect { get; set; } = false;
    }
}
