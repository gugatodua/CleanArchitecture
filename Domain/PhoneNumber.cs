using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class PhoneNumber
    {
        [Required]
        public NumberType Type { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Number { get; set; }
    }
}
