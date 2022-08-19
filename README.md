# RestWebApiDotnetCore6

A simple REST Web Api using Entity Framework Core with in memory database and exploring some
aspects related with exception handling and the HATEOAS constraint of the REST architecture
web api desing.

## Exception Handling

The error handling with try-catch blocks altought of beeing simple make the developer 
always repeat the similar pieces of code all around the controllers. 
In order to addres this situation the project uses the exception midleware. The midleware
is added in the HTTP request pipeline scope and every exception throwed during the request pipeline 
will be handled using the custom exception handler. There is also a built in exception midleware but
in order to explore more meaningfulls HTTP Responses Status Codes we decided to use a customized one.

Next we have our custom exception midleware class:
```sh
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Handle custom exceptions here
        try
        {
            await _next(httpContext);
        }
        catch (FieldValidationException ex)
        {
            _logger.LogError($"Invalid Fields Exception: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (BusinessRuleException ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, FieldValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }
    private async Task HandleExceptionAsync(HttpContext context, BusinessRuleException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        }.ToString());
    }
}
```
One important aspect here is that the stack trace is never exposed at the returned error
object and instead the stack trace is logged using the default logging provider. 
This is a common security flaw we frequently see in many web api's and must be at all
costs avoided.

After the creation of the midleware class we create our extension of the 
application builder interface:

```sh
public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
```

The last part of midleware setup process is to add the midleware at the request
pipeline:

```sh
app.ConfigureCustomExceptionMiddleware();
```

Now we are ready to through any exceptions in our web api and be and rest assured because 
all will be treated in custom handler.

## REST Web Api Architectural Style with HATEOAS



