namespace FreshFruit.Models.ViewModel
{
	public class UserViewModel
	{
		public int AccountId { get; set; }
		public string? Email { get; set; }
		public int Role { get; set; }
		public int? AccountStatus { get; set; }

		public string? Fullname { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }
		public string? MemberEmail { get; set; }
		public DateOnly? Dob { get; set; }
		public int? MemberStatus { get; set; }
	}
}
