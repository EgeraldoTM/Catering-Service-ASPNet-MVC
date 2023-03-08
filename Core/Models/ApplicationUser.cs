using System.ComponentModel.DataAnnotations;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace CateringService.Models;

public class ApplicationUser : IdentityUser
{
	[StringLength(50)]
	public string FirstName { get; set; } = null!;

	[StringLength(50)]
	public string LastName { get; set; } = null!;

	[DataType(DataType.Date)]
	public DateTime BirthDay { get; set; }

	[StringLength(40)]
	public string IdentificationNumber { get; set; } = null!;

	[DataType(DataType.Date)]
	public DateTime RegistrationDate { get; set; }
	public DateTime LastLogin { get; set; }
	public bool IsDeleted { get; set; }
	public ICollection<Order> Orders { get; set; } = null!;
}
