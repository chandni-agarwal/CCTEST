namespace Microsoft.Teams.Apps.CompanyCommunicator.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Image Upload View Model
    /// /// </summary>
    public class ImageUploadViewModel
    {
        /// <summary>
        /// Gets or sets File
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }

        /// <summary>
        /// Gets or sets UploadedFilePath
        /// </summary>
        [DisplayName("Image URL")]
        public string UploadedFilePath { get; set; }
    }
}
