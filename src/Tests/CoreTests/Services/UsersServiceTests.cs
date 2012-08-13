using NHibernate.Linq;
using NUnit.Framework;
using Procent.Samples.NHibernateStarter.Core.DataAccess;
using Procent.Samples.NHibernateStarter.Core.Entities;
using Procent.Samples.NHibernateStarter.Core.Services;
using Procent.Samples.NHibernateStarter.Tests.Core.DataAccess;
using Procent.Samples.NHibernateStarter.Tests.Core.Utils;
using System.Linq;

namespace Procent.Samples.NHibernateStarter.Tests.Core.Services
{
	[TestFixture]
	public class UsersServiceTests : DbTestBase
	{
		[Test]
		public void AddsNewUser()
		{
			var newUser = new User()
			              	{
			              		UserName = RandomValues.String(),
			              		Age = RandomValues.Number(),
			              	};

			new UsersService().AddUser(newUser);

			User fetched;
			using (var session = DataAccessFacade.OpenSession())
			{
				fetched = session.Linq<User>().Where(x => x.UserName == newUser.UserName).SingleOrDefault();
			}

			Assert.IsNotNull(fetched);
			Assert.AreEqual(newUser.Age, fetched.Age);
		}
	}
}