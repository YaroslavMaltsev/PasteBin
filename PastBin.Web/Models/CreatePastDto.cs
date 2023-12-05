﻿using System.ComponentModel.DataAnnotations;

namespace PasteBinWeb.ResourceModel
{
    public class CreatePastDto
    {
        [Required(ErrorMessage = "Название поста не должно быть пустым")]
        [StringLength(30,ErrorMessage = "Пожалуйста сократите название поста")]
        public string Title { get; set; }
        [Required]
        public DateTime DtateCreate { get; set; }
        [Required]
        public double DateSave { get; set; }
        [Required]
        public string URL { get; set; }
        public string HashUrl { get; set; }
    }
}
