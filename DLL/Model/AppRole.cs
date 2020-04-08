using System;
using DLL.Model.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DLL.Model
{
    public class AppRole : IdentityRole<int>,ITrackable,ISoftDelete
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}