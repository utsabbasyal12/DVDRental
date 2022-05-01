using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DVDRental.Areas.Identity.Data;

// Add profile data for application users by adding properties to the DVDRentalUser class
public class DVDRentalUser : IdentityUser
{
    //public int ShopNumber { get; set; }    
    public string? ShopName { get; set; }
}

