namespace WebApi.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;


    public class TokenActionFilter :
        IActionFilter
    {
        readonly Token _token;

        public TokenActionFilter(Token token)
        {
            _token = token;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["Token"];
            if (!string.IsNullOrWhiteSpace(token))
                _token.Value = token;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}