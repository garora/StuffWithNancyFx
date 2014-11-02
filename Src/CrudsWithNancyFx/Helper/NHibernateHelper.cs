using CrudsWithNancyFx.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace CrudsWithNancyFx.Helper
{
    public class NHibernateHelper
    {

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();

        }
        private static ISessionFactory _sessionFactory;
        //ToDo- change connection string or put it into config file
        const string ConnectionString = @"data source=GAURAV-ARORA;initial catalog=crudwepapi;integrated security=True;";
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    CreateSessionFactory();

                return _sessionFactory;
            }
        }

        private static void CreateSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString).ShowSql)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ServerData>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();
        }
    }
}