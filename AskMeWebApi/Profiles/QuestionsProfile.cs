using AskMeWebApi.DataModels;
using AskMeWebApi.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMeWebApi.Profiles
{
    public class QuestionsProfile : Profile
    {
        public QuestionsProfile()
        {
            CreateMap<QuestionDto, Question>()
                .ForSourceMember(dest=>dest.Answers,opt=> opt.DoNotValidate());
            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.Answers, src => src.Ignore());
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();
            CreateMap<VoteDto, Vote>();
            CreateMap<Question, QuestionAllDto>()
                .ForMember(dest => dest.Answers, src => src.Ignore());
        }
    }
}
