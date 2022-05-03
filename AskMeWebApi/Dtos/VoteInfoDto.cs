namespace AskMeWebApi.Dtos
{
    public class VoteInfoDto
    {
        public double Answers { get; set; }
        public bool? AnswerCorrect { get; set; } = null;
    }
}
