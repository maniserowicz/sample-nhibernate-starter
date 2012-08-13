using System;
using NUnit.Framework;

namespace Procent.Samples.NHibernateStarter.Tests.Core.DataAccess
{
	public abstract class DbTestBase
	{
		protected InMemoryDatabase _database;

		[SetUp]
		public void SetupDatabase()
		{
			_database = new InMemoryDatabase();
			Console.WriteLine("----------------------------");
			Console.WriteLine("Database test started...");
			Console.WriteLine("----------------------------");
		}

		[TearDown]
		public void TeardownDatabase()
		{
			_database.Dispose();
			Console.WriteLine("----------------------------");
			Console.WriteLine("Database test finished...");
			Console.WriteLine("----------------------------");
		}
	}
}