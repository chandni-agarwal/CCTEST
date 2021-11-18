using Microsoft.AspNetCore.Http;
using Microsoft.Teams.Apps.CompanyCommunicator.Models;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.Teams.Apps.CompanyCommunicator.Models
{
    public class FileUploadModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
