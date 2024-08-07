namespace Authentication.LIB.Entities
{
	public class UsedToken : BaseEntity
	{
		public string Token { get; set; }
		public DateTime UsedAt { get; set; }
	}

}
