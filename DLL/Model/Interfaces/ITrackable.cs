using System;

namespace DLL.Model.Interfaces
{
    public interface ITrackable
    {
        DateTimeOffset CreatedAt { set; get; }
        string CreatedBy { get; set; }
        DateTimeOffset UpdatedAt { set; get; }
        string UpdatedBy { get; set; }
    }
}