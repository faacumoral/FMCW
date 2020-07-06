namespace FMCW.Common.Results
{
    public class BaseResult<Tok, Terror>
    {
        public Tok ResultOk { get; set; } = default;
        public Terror ResultError { get; set; } = default;
        public ResultOperation ResultOperation { get; set; } = ResultOperation.Ok; 
        public bool Success { get; set; } = true;
    }
}
