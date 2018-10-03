namespace SampleApp.Api.Models
{
    public class Respose<TResult>
    {
        public TResult Result { get; set; }
        public string Message { get; set; }
    }
}