using AskMeWebApi.DataModels;
using AskMeWebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AskMeWebApi.Repositories
{
    public interface IQuestionsRepository
    {
        public void InsertQuestion(Question question);
        public void InsertAnswers(List<Answer> answers);
        public Task<QuestionDto> GetAsync(Guid questionId);
        public Task<List<QuestionAllDto>> GetAllAsync();
        public Task<double> VoteAsync(Vote vote);
        public double GetVotesForQuestion(Guid questionId);
        public Task<bool> CheckAnswerAsync(Vote vote);
    }
}
