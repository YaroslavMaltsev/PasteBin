using System.ComponentModel.DataAnnotations;

namespace PasteBinWeb.Dto
{
    public class RegiserDto
    {
        [Required]
        public string Name { get; set; }

    }
}
