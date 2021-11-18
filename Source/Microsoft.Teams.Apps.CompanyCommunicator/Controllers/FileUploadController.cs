namespace Microsoft.Teams.Apps.CompanyCommunicator.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Teams.Apps.CompanyCommunicator.Helpers;
    using Microsoft.Teams.Apps.CompanyCommunicator.Models;
    using Microsoft.Teams.Apps.CompanyCommunicator.Repositories;
    using Microsoft.Teams.Apps.CompanyCommunicator.ViewModels;

    public class FileUploadController : ControllerBase
    {
        private readonly IDbRepository repository;
        private readonly IConfiguration configuration;

        private readonly IHostingEnvironment hostingEnvironment;


        public FileUploadController(IDbRepository repository, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(FileUploadViewModel fileUpload)
        {
            if (ModelState.IsValid)
            {
                string uploadsfolder = Path.Combine(this.hostingEnvironment.WebRootPath, "Files");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileUpload.File.FileName;
                string fileLocation = @"wwwroot/Files/" + uniqueFileName;
                if (fileUpload.File != null)
                {
                    // Write it to server.
                    using (FileStream fs = System.IO.File.Create(fileLocation))
                    {
                        await fileUpload.File.CopyToAsync(fs);
                    }

                    // this method return bool value as success/fail, this can be used in case want to return msg user in failure
                    if (await this.repository.UploadFileToSPAsync(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation); // Delete the file once file upload is succesful.

                        SubmitNewResourceRequestAction model = new SubmitNewResourceRequestAction
                        {
                            ResourceInfo = fileUpload,
                        };
                        model.ResourceInfo.SharePointFilePath =
                            this.configuration["https://infinionco0.sharepoint.com/sites/CompanyCommunicatorApp"] +
                            "/UserUploaded%20Documents/Forms/AllItems.aspx?id=/teams/platform/UserUploaded Documents/" +
                            fileUpload.File.FileName +
                            "&parent=/teams/platform/UserUploaded Documents";
                        model.type = Constants.SubmitNewRequest;
                        model.ResourceInfo.FileName = fileUpload.File.FileName;
                        model.AppId = configuration["1c07cd26-a088-4db8-8928-ace382fa219f"];
                        return JSON(model);

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to upload your file. Please try again later.");
                    }
                }
            }

            return Json("File not found"); // more appropriate with json
        }

        [HttpGet]
        public IActionResult ImageUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImageUpload(ImageUploadViewModel imagefileModel)
        {
            if (this.ModelState.IsValid)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imagefileModel.Image.FileName;
                string fileLocation = @"wwwroot/Images/" + uniqueFileName;
                if (imagefileModel.Image != null)
                {
                    // Write it to Files Folder.
                    using (FileStream fs = System.IO.File.Create(fileLocation))
                    {
                        await imagefileModel.Image.CopyToAsync(fs);
                    }

                    var imageUploadModel = new ImageUploadAction()
                    {
                        ImageUrl = this.configuration["BaseUri"] + "/Images/" + uniqueFileName,
                        type = Constants.UploadNewImage,
                    };

                    return View("ImageCreate", imageUploadModel);
                }
            }

            return View(imagefileModel);
        }
    }
}