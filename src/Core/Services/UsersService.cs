using Procent.Samples.NHibernateStarter.Core.DataAccess;
using Procent.Samples.NHibernateStarter.Core.Entities;

namespace Procent.Samples.NHibernateStarter.Core.Services
{
	public class UsersService
	{
		public void AddUser(User user)
		{
			DataAccessFacade.InTransaction(session => session.Save(user));
		}
	}
}