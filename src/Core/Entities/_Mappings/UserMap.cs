using FluentNHibernate.Mapping;

namespace Procent.Samples.NHibernateStarter.Core.Entities.Mappings
{
	public class UserMap : ClassMap<User>
	{
		public UserMap()
		{
			Id(x => x.Id);
			Map(x => x.UserName);
			Map(x => x.Age);
		}
	}
}