using CrudsWithNancyFx.Models;
using CrudsWithNancyFx.Models.Persistence;
using Nancy;
using Nancy.ModelBinding;

namespace CrudsWithNancyFx
{
    public class ServerDataModule : NancyModule
    {
        private readonly IServerDataRepository _serverDataRepository;

        public ServerDataModule(IServerDataRepository serverDataRepository)
            : base("/serverdata")
        {
            _serverDataRepository = serverDataRepository;

            Get["/"] = _ => Response.AsJson<object>(serverDataRepository.GetAll());

            Get["/{id:int}"] = _ => FormatterExtensions.AsJson<object>(Response, serverDataRepository.Get(_.id));

            Post["/"] = _ =>
            {
                var data = serverDataRepository.Add(this.Bind<ServerData>());

                return data != null ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            };

            Put["/{id:int}"] = _ =>
            {
                var data = this.Bind<ServerData>();
                data.Id = _.id;

                var isSuccess = serverDataRepository.Update(data);
                return isSuccess ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;
            };

            Delete["/{id:int}"] = _  =>
            {
                serverDataRepository.Delete(_.id);
                return HttpStatusCode.OK;
            };
        }

    }
}