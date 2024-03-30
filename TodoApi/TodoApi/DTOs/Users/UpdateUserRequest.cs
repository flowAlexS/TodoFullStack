﻿namespace TodoApi.DTOs.Users
{
    public class UpdateUserRequest
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? CurrentPassword { get; set; }    

        public string? NewPassword { get; set; }

        public IFormFile? ProfilePicture { get; set; }
    }
}
