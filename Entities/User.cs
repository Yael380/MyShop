using System.ComponentModel.DataAnnotations;

namespace MyShop;

public class User
{
    public int Id { get; set; }
    [EmailAddress]
    public string UserName { get; set; }
    public string Password { get; set; }

   // [StringLength(20, ErrorMessage = "Name can be between 2 till 20 letters", MinimumLength = 2)] 
    public string FirstName { get; set; }
    
  //  [StringLength(20, ErrorMessage = "Name can be between 2 till 20 letters", MinimumLength = 2)]
    public string LastName { get; set; }

}
