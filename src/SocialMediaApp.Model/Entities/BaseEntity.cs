﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Model.Entities
{
    public class BaseEntity
    {
       
        public int Id { get; set; }
        public DateTime  CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
