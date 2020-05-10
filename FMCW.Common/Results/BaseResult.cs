namespace FMCW.Common.Results
{
    public class BaseResult<Tok, Terror>
        where Tok : class
        where Terror : class
    {
        public Tok ResultOk { get; set; } = null;
        public Terror ResultError { get; set; } = null;
        public ResultOperation ResultOperation { get; set; } = ResultOperation.Ok; 
        public bool Success { get; set; } = true;
    }
}
