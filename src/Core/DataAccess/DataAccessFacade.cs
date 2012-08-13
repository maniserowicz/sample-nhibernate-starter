using System;
using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace Procent.Samples.NHibernateStarter.Core.DataAccess
{
	public static class DataAccessFacade
	{
		#region Default session factory creation

		private static ISessionFactory _sessionFactory;
		private static readonly object _syncRoot = new object();

		// Default way of opening a new session;
		// creates ONE SessionFactory for the whole system
		// and returns a new session on each request
		private static readonly Func<ISession> _defaultOpenSession = () =>
		                                                             	{
		                                                             		if (_sessionFactory == null)
		                                                             		{
		                                                             			lock (_syncRoot)
		                                                             			{
		                                                             				if (_sessionFactory == null)
		                                                             					Configure();
		                                                             			}
		                                                             		}

		                                                             		return _sessionFactory.OpenSession();
		                                                             	};

		private static void Configure()
		{
			_sessionFactory = Fluently.Configure()
				.Database(
				MsSqlConfiguration.MsSql2005.ConnectionString(builder => builder.FromConnectionStringWithKey("default"))
				)
				.Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
				               	.Conventions.Add(ForeignKey.EndsWith("Id"))
				               	.Conventions.Add(Table.Is(ci => ci.EntityType.Name + "s"))
				)
				.BuildSessionFactory();
		}

		#endregion

		#region Opening a new session

		// Logic responsible for opening a session can be changed on a thread-by-thread basis.
		// Threads that have not modified this behavior will retrieve a default implementation's result.
		[ThreadStatic]
		private static Func<ISession> _openSession;

		public static Func<ISession> OpenSession
		{
			set { _openSession = value; }
			get { return _openSession ?? _defaultOpenSession; }
		}

		#endregion

		/// <summary>
		/// Perform a transactional operation.
		/// </summary>
		/// <remarks>Creates a new session and opens a new transaction. Passed operations are executed in new transaction's scope.</remarks>
		public static void InTransaction(Action<ISession> operation)
		{
			using (var session = OpenSession())
			{
				using (var tx = session.BeginTransaction())
				{
					operation(session);

					tx.Commit();
				}
			}
		}
	}
}