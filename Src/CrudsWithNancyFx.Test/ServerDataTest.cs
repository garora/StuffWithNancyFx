using System;
using System.Net;
using CrudsWithNancyFx.Models;
using CrudsWithNancyFx.Models.Persistence;
using Moq;
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
            mockServerDataRepository.Setup(d => d.Get(12)).Returns(FakeServerData());

            var browser =
                new Browser(c =>
                        c.Module<ServerDataModule>().Dependencies<IServerDataRepository>(mockServerDataRepository.Object));

            var response = browser.Get("ServerData/1", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

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
