namespace FMCW.Common.Results
{
    public class BaseResult<Tok, Terror> where 
        Tok: class where 
        Terror : class 
    {
        public Tok ResultOK { get; set; } = null;
        public Terror ResultError { get; set; } = null;
        public bool Success { get; set; } = true;
    }
}
