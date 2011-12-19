namespace RestfulRouting.Format.ResultExposal
{
    public static class FormatResultExtensions
    {
        public static ActionResultExposer ExposeActionResult(this FormatResult actionResult)
        {
            return new ActionResultExposer(actionResult.FormatCollection);
        }
    }
}