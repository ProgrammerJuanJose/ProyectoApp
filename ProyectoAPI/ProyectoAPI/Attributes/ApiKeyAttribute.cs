using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProyectoAPI.Attributes
{
        //Ayuda a limitar la forma en la que se puede consumir un recurso de controlador (End Point)

        [AttributeUsage(validOn: AttributeTargets.All)]
        public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
        {

            private readonly string _apiKey = "ProyectoApiKeyJJ";

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 404,
                        Content = "Llamada no contiene información de seguridad..."
                    };
                    return;
                }
                var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                var ApiKeyValue = appSettings.GetValue<string>(_apiKey);

                if (!ApiKeyValue.Equals(ApiSalida))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        Content = "ApiKey inválida..."
                    };
                    return;
                }

                await next();

            }


        }
    }
