using System;
using System.Collections.Generic;

namespace FMCW.Common.Results
{
    public class ListResult<T> : BaseResult<List<T>, ErrorResult>
    {
        public static ListResult<T> Ok(List<T> ok)
            => new ListResult<T>
            {
                ResultOk = ok,
                ResultOperation = ResultOperation.Ok,
                Success = true
            };

        public static ListResult<T> Error(Exception ex)
           => new ListResult<T>
           {
               ResultOperation = ResultOperation.Error,
               ResultError = ErrorResult.Build(ex),
               Success = false
           };

    }
}
