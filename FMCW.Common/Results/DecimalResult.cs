using System;

namespace FMCW.Common.Results
{
    public class DecimalResult : BaseResult<decimal, ErrorResult>, IBaseErrorResult
    {
        public static DecimalResult Ok(decimal ok)
            => new DecimalResult()
            {
                ResultOk = ok,
                ResultOperation = ResultOperation.Ok,
                Success = true
            };

        public static DecimalResult Error(Exception ex, ResultOperation result = ResultOperation.Error)
           => new DecimalResult()
           {
               ResultError = ErrorResult.Build(ex),
               ResultOperation = result,
               Success = false
           };

        public static DecimalResult Error(ErrorResult error, ResultOperation result = ResultOperation.Error)
            => new DecimalResult()
            {
                ResultError = error,
                ResultOperation = result,
                Success = false
            };

        public static DecimalResult Error(string ex, ResultOperation result = ResultOperation.Error)
           => new DecimalResult()
           {
               ResultError = ErrorResult.Build(ex),
               ResultOperation = result,
               Success = false
           };

    }
}
