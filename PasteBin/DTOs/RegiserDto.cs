using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.Dto
{
    public class RegiserDto
    {
        [Required]
        public string Name { get; set; }

    }
}
