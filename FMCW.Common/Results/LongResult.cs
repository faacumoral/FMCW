using System;

namespace FMCW.Common.Results
{
    public class LongResult : BaseResult<long, ErrorResult>, IBaseErrorResult
    {
        public static LongResult Ok(long ok)
            => new LongResult()
            {
                ResultOk = ok,
                ResultOperation = ResultOperation.Ok,
                Success = true
            };

        public static LongResult Error(Exception ex, ResultOperation result = ResultOperation.Error)
           => new LongResult()
           {
               ResultError = ErrorResult.Build(ex),
               ResultOperation = result,
               Success = false
           };

        public static LongResult Error(ErrorResult error, ResultOperation result = ResultOperation.Error)
             => new LongResult()
             {
                 ResultError = error,
                 ResultOperation = result,
                 Success = false
             };

        public static LongResult Error(string ex, ResultOperation result = ResultOperation.Error)
           => new LongResult()
           {
               ResultError = ErrorResult.Build(ex),
               ResultOperation = result,
               Success = false
           };

    }
}
