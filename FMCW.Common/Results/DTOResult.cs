using System;

namespace FMCW.Common.Results
{
    public class DTOResult<T> : BaseResult<T, ErrorResult>, IBaseErrorResult
        where T: BaseDTO
    {
        public static DTOResult<T> Ok(T ok)
            => new DTOResult<T>
            {
                ResultOk = ok,
                ResultOperation = ResultOperation.Ok,
                Success = true
            };

        public static DTOResult<T> Error(Exception ex)
           => new DTOResult<T>
           {
               ResultOperation = ResultOperation.Error,
               ResultError = ErrorResult.Build(ex),
               Success = false
           };

        public static implicit operator DTOResult<T>(T t)
            => new DTOResult<T>
            {
                ResultOk = t,
                ResultOperation = ResultOperation.Ok,
                Success = true
            };
    }
}
