namespace FMCW.Common.Results
{
    public interface IBaseResult
    {
        ResultOperation ResultOperation { get; set; }
        bool Success { get; set; }
    }
}

