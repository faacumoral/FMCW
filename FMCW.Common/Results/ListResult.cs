using System;
using System.Collections.Generic;

namespace FMCW.Common.Results
{
    public class ListResult<T> : BaseResult<List<T>, ErrorResult>
    {
        public static ListResult<T> Build(List<T> ok, Exception ex)
            => new ListResult<T>
            {
                ResultOK = ok,
                ResultError = ErrorResult.Build(ex)
            };

    }
}
