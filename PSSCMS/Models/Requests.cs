using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PSSCMS.Models
{
    public class Requests
    {
    }

    public class CustomizePhotoRequest : PortfolioPhoto
    {
        public string Errors { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]  
        public HttpPostedFileBase file { get; set; }
}
}