using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace VisitCardEfCore.ViewModel
{
    public class CreateVisitCardViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        [Required][Phone]
        public string PhoneNumber { get; set; }
        [Required][EmailAddress]
        public string Email { get; set; }
        
        
    }
}