using System.ComponentModel.DataAnnotations;

namespace Exam01.ViewModel;



public class LoginViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name ="Email")]
    public string Email{get;set;}
    [Required]
    [DataType(DataType.Password)]
    [Display(Name ="Password")]
    public string Password{get;set;}
    public string ReturnUrl {get;set;}
    
    [Display(Name ="Remember Me")]
    public bool IsRememberme{get;set;}
}