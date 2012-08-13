namespace Procent.Samples.NHibernateStarter.Core.Entities
{
	public class User
	{
		public virtual int Id { get; set; }
		public virtual string UserName { get; set; }
		public virtual int Age { get; set; }
	}
}