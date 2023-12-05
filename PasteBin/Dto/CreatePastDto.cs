﻿using System.ComponentModel.DataAnnotations;

namespace PasteBinApi.ResourceModel
{
    public class CreatePastDto
    {
        [Required(ErrorMessage = "Название поста не должно быть пустым")]
        [StringLength(30,ErrorMessage = "Пожалуйста сократите название поста")]
        public string Title { get; set; }
        public DateTime DtateCreate { get; set; }
        public double DateSave { get; set; }
    }
}
