namespace FMCW.Common.Results
{
    public interface IBaseErrorResult 
    {
        ErrorResult ResultError { get; set; }
        ResultOperation ResultOperation { get; set; }
        bool Success { get; set; }
    }
}
