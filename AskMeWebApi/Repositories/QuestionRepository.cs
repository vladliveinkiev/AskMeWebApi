using AskMeWebApi.DataModels;
using AskMeWebApi.Dtos;
using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi.Repositories
{
    public class QuestionRepository : IQuestionsRepository
    {
        private readonly IMongoCollection<Question> _questions;
        private readonly IMongoCollection<Vote> _votes;
        private readonly IMongoCollection<Answer> _answers;
        private readonly IMapper _mapper;

        public QuestionRepository(IMongoClient mongoClient, IMapper mapper)
        {
            _mapper = mapper;
            var db = mongoClient.GetDatabase("AskmeDb");
            _questions = db.GetCollection<Question>(nameof(Question));
            _votes = db.GetCollection<Vote>(nameof(Vote));
            _answers = db.GetCollection<Answer>(nameof(Answer));
        }

        public async Task<QuestionDto> GetAsync(Guid questionId)
        {
            Question question;
            QuestionDto result=new QuestionDto();
            question = (await _questions.FindAsync(f => f.Id == questionId)).FirstOrDefault();
            if (question!=null)
            {
                List<Answer> answersForQuestion = (await _answers.FindAsync(f => f.QuestionId == questionId)).ToList();
                result = _mapper.Map<QuestionDto>(question);
                result.Answers = _mapper.Map<List<AnswerDto>>(answersForQuestion);
            }
            return result;
        }

        public void InsertQuestion(Question question)
        {
            _questions.InsertOne(question);
        }

        public async Task<double> VoteAsync(Vote vote)
        {
            await _votes.InsertOneAsync(vote);
            double countVotes = _votes.CountDocuments(c => c.QuestionId == vote.QuestionId);
            return countVotes;

        }

        public double GetVotesForQuestion(Guid questionId)
        {
            double countVotes = _votes.CountDocuments(c => c.QuestionId == questionId);
            return countVotes;
        }

        public void InsertAnswers(List<Answer> answers)
        {
            _answers.InsertMany(answers);
        }

        public async Task<bool> CheckAnswerAsync(Vote vote)
        {
            var answer = (await _answers.FindAsync(f => f.QuestionId == vote.QuestionId && f.AnswerId == vote.AnswerId)).FirstOrDefault();
            if (answer!=null)
            {
                return answer.IsCorrect;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<QuestionAllDto>> GetAllAsync()
        {
            var questions = (await _questions.FindAsync(f=>true)).ToList();
            var questionAllDto = _mapper.Map<List<QuestionAllDto>>(questions);
            var answers = (await _answers.FindAsync(f => true)).ToList();
            foreach (var item in questionAllDto)
            {
                item.Answers = answers.Where(w => w.QuestionId == item.Id).ToList();
            }

            return questionAllDto;

        }
    }
}
