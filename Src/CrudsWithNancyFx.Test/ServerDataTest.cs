using System;
using CrudsWithNancyFx.Exceptions;
using CrudsWithNancyFx.Models;
using CrudsWithNancyFx.Models.Persistence;
using Moq;
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

namespace CrudsWithNancyFx.Test
{

    [TestFixture]
    public class ServerDataTest
    {
        [Test]
        public void Should_return_ServerData_When_Exists()
        {
            var mockServerDataRepository = new Mock<IServerDataRepository>();
            mockServerDataRepository.Setup(d => d.Get(1)).Returns(FakeServerData());

            var browser =
                new Browser(c =>
                        c.Module<ServerDataModule>().Dependencies<IServerDataRepository>(mockServerDataRepository.Object));

            var response = browser.Get("ServerData/1", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            }).Body.AsString();

            Assert.That(response, Is.Not.Null);

            Assert.That(response.Contains("1009"));
        }

        [Test]
        public void Should_Throw_NotFound_Exception_When_Does_Not_Exist()
        {
            var mockServerDataRepository = new Mock<IServerDataRepository>();
            mockServerDataRepository.Setup(d => d.Get(12)).Throws(new ServerDataNotFoundException("ServerData with Id 12 Not Found"));

            var browser =
                new Browser(c =>
                        c.Module<ServerDataModule>().Dependencies<IServerDataRepository>(mockServerDataRepository.Object));

            var response = browser.Get("ServerData/12", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            var responseBody = response.Body.AsString();


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(responseBody, Is.EqualTo("ServerData with Id 12 Not Found"));

        }

        [Test]
        public void Should_Update_Existing_ServerData()
        {
            var mockServerDataRepository = new Mock<IServerDataRepository>();
            var newServerData = FakeServerData();

            mockServerDataRepository.Setup(d => d.Add(newServerData)).Returns(newServerData);

            var browser =
                new Browser(c =>
                        c.Module<ServerDataModule>().Dependencies<IServerDataRepository>(mockServerDataRepository.Object));
            newServerData.IP = "127.1.1.1";
            var response = browser.Put("ServerData/" + newServerData.Id, with =>
            {
                with.JsonBody<ServerData>(newServerData);
            });


            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        }

        private static ServerData FakeServerData()
        {
            return new ServerData
            {
                Id = 1,
                IP = "127.0.0.01",
                IsDirty = true,
                OrderNumber = 1,
                Type = 1,
                RecordIdentifier = 1009,
                InitialDate = new DateTime(2014, 11, 1),
                EndDate = new DateTime(2014, 11, 2)
            };
        }
    }
}
