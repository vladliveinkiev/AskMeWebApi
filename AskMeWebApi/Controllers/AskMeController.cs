using AskMeWebApi.DataModels;
using AskMeWebApi.Dtos;
using AskMeWebApi.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AskMeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IQuestionsRepository _questionsRepository;

        public AskMeController(IQuestionsRepository questionRepository,IMapper mapper)
        {
            _mapper = mapper;
            _questionsRepository = questionRepository;
        }

        [SwaggerOperation(Summary = "Get all question from database")]
        [SwaggerResponse(statusCode: 200, Description = "Return list of questions")]
        [HttpGet]
        [Route("getallquestions")]
        public async Task<IActionResult> GetAllQuestions()
        {
            var result = await _questionsRepository.GetAllAsync();
            return Ok(result);
        }

        [SwaggerOperation(Summary = "Get question by Id")]
        [SwaggerResponse(statusCode: 200, Description = "Return question")]
        [SwaggerResponse(statusCode: 404, Description = "Question not found by given ID")]
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get(Guid questionId)
        {
            var question = await _questionsRepository.GetAsync(questionId);

            if (question == null)
                return NotFound();
            else
                return Ok(question);
        }

        [SwaggerOperation(Summary = "Insert new question")]
        [SwaggerResponse(statusCode: 200, Description = "Return question Id")]
        [HttpPost]
        [Route("insert")]
        public IActionResult Insert(QuestionDto questionForInsert)
        {
            if (!questionForInsert.Answers.Any() || 
                (questionForInsert.QuestionType==QuestionType.Trivia && 
                questionForInsert.Answers.Count(cc=>cc.IsCorrect)!=1))
            {
                return BadRequest("Input data not correct - please, check question type or correctnes of iscorrect values");
            }
            var question = _mapper.Map<Question>(questionForInsert);
            _questionsRepository.InsertQuestion(question);
            var answers = _mapper.Map<List<Answer>>(questionForInsert.Answers);
            answers.ForEach(f => f.QuestionId = question.Id);
            _questionsRepository.InsertAnswers(answers);
            return Ok(question.Id);
        }

        [SwaggerOperation(Summary = "Insert vote")]
        [SwaggerResponse(statusCode: 200, Description = "Return vote quantity and, if question type is Trivia, corectness of the answer")]
        [HttpPost]
        [Route("vote")]
        public async Task<IActionResult> Vote(VoteDto voteDto)
        {
            Vote vote = _mapper.Map<Vote>(voteDto);
            var voteCounted = await _questionsRepository.VoteAsync(vote);
            var question = await _questionsRepository.GetAsync(vote.QuestionId);
            var isCorrectAnswer = await _questionsRepository.CheckAnswerAsync(vote);
            if (question.QuestionType == QuestionType.Trivia)
            {
                if (isCorrectAnswer)
                    return Ok(new { votes = voteCounted, text = "Answer is correct" });
                else
                    return Ok(new { votes = voteCounted, text = "Answer is not correct" });
            }
            else
                return Ok(new { votes = voteCounted });
        }

    }
}
