﻿using System.ComponentModel.DataAnnotations;

namespace DVDRental.Models
{
    public class Member
    {
        [Key]
        public int MemberNumber { get; set; }
        public string? MemberLastName { get; set; }
        public string? MemberFirstName { get; set; }
        public string? MemberAddress { get; set; }
        public DateOnly MemberDOB { get; set; }
        public MembershipCategory? MembershipCategory { get; set; }
    }
}
