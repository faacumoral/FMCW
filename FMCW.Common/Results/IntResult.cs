using System;

namespace FMCW.Common.Results
{
    public class IntResult : BaseResult<int, ErrorResult>, IBaseErrorResult
    {
        public static IntResult Ok(int ok)
            => new IntResult
            {
                ResultOk = ok,
                ResultOperation = ResultOperation.Ok,
                Success = true
            };

        public static IntResult Error(Exception ex)
           => new IntResult
           {
               ResultError = ErrorResult.Build(ex),
               ResultOperation = ResultOperation.Error,
               Success = false
           };

    }
}
