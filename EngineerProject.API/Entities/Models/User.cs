using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineerProject.API.Entities.Models
{
    public class User
    {
        public List<Comment> Comments { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; }

        public List<UserGroup> Groups { get; set; }

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Login { get; set; }

        public List<Message> Messages { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        public bool ReceiveNotifications { get; set; }

        public string RecoveryCode { get; set; }

        public DateTime? RecoveryExpirationDate { get; set; }
    }
}