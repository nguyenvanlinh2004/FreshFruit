using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FreshFruit.Models
{
	public class CompanyInfo
	{
		[Key]
		public int Id { get; set; }

		[Required, StringLength(100)]
		public string CompanyName { get; set; } = string.Empty;

		[StringLength(255)]
		public string? Address { get; set; }

		[StringLength(20)]
		public string? Phone { get; set; }

		[StringLength(100)]
		public string? Email { get; set; }

		[StringLength(100)]
		public string? Website { get; set; }

		[StringLength(255)]
		public string? LogoUrl { get; set; }
	}
}