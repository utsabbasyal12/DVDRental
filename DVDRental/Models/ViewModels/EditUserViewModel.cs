﻿using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DVDRental.Models.ViewModels
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string ShopName { get; set; }

        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //public string City { get; set; }

        public List<string> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }


}
