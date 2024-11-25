﻿using LookatBackend.Models;

namespace LookatBackend.Dtos.User
{
    public class CreateUserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; } // Changed to string
        public DateTime Date { get; set; }
        public string PhysicalIdNumber { get; set; }
        public string Purok { get; set; }
        public string BarangayLoc { get; set; }
        public string CityMunicipality { get; set; }
        public string Province { get; set; }
        public string FullAddress => $"{Purok}, {BarangayLoc}, {CityMunicipality}, {Province}";
        public string Email { get; set; }
        // commnetd because not so sure pa public IFormFile ProfilePicture { get; set; }
        // also need mo verify para naay barngay public string? BarangayId { get; set; } // Made nullable
        //public Barangay Barangay { get; set; }
    }
}

// pass onto this the data you want to add so by that don't add id for example