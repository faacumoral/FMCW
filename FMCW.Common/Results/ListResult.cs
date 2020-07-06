using System;
using System.Collections.Generic;

namespace FMCW.Common.Results
{
    public class ListResult<T> : BaseResult<List<T>, ErrorResult>, IBaseErrorResult
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

        public static implicit operator ListResult<T>(List<T> t)
          => new ListResult<T>
          {
              ResultOk = t,
              ResultOperation = ResultOperation.Ok,
              Success = true
          };
    }
}
