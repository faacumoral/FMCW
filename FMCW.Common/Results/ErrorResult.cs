using System;

namespace FMCW.Common.Results
{
    public class ErrorResult
    {
        public string ErrorMessage { get; set; } = string.Empty;
        public string FullException { get; set; } = string.Empty;
        public string FriendlyErrorMessage { get; set; } = string.Empty;


        public static ErrorResult Build(Exception ex)
            => new ErrorResult
            {
                ErrorMessage = ex.Message,
                FullException = ex.ToString(),
                FriendlyErrorMessage = ex.Message
            };

        public static ErrorResult Build(string ex)
           => new ErrorResult
           {
               ErrorMessage = ex,
               FullException = ex,
               FriendlyErrorMessage = ex
           };
    }
}
