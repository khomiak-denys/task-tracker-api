using DomainFramework.Errors;
using DomainFramework.Results;
using Microsoft.AspNetCore.Mvc;

namespace ServiceDefaults.ErrorHandling
{
    public static class ResultExtensions
    {
        public static IActionResult Match(this Result resultor, IActionResult result, Func<Error, IActionResult> onFailure)
        {
            return resultor.IsSuccess ? result : onFailure(resultor.Error);
        }
    }
}
