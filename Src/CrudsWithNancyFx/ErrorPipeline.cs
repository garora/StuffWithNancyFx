using System;
using System.Text;
using CrudsWithNancyFx.Exceptions;
using Nancy;
using Nancy.Bootstrapper;

namespace CrudsWithNancyFx
{
    public class ErrorPipeline : IApplicationStartup
    {
        //Hook all kind of errors here
        //we are simply defined one custom exception for demo
        public void Initialize(IPipelines pipelines)
        {
            pipelines.OnError += (context, exception) =>
            {
                if (exception is ServerDataNotFoundException)
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        ContentType = "text/html",
                        Contents = (stream) =>
                        {
                            var errorMessage =
                                Encoding.UTF8.GetBytes(exception.Message);
                            stream.Write(errorMessage, 0,errorMessage.Length);
                        }
                    };

                //If not expected exception simply throw 500 exception
                return HttpStatusCode.InternalServerError;
            };
        }
    }
}