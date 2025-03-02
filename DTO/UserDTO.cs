using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public record GetUserDTO(int Id, [EmailAddress] string UserName, string? FirstName, string? LastName);

    public record PostUserDTO(
    [Required][EmailAddress] string UserName,
    [StringLength(20, ErrorMessage = "First name can be between 2 till 8 chars", MinimumLength = 2)] string FirstName,
    [StringLength(20, ErrorMessage = "Last name can be between 2 till 8 chars", MinimumLength = 2)] string LastName,
    [Required] string Password);
}
