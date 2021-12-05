namespace PSSCMS.Models
{
    public class HomeModel
    {
        public string PageTitle { get; set; }

        public string PageSubTitle { get; set; }

        public string ButtonText { get; set; }

        public string AdditionalTitle { get; set; }

        public string AdditionalSubtitle { get; set; }

        public ProjectsSection ProjectsSection { get; set; }

        public PageOwnerInfo PageOwnerInfo { get; set; }

        public HomeModel()
        {
            ProjectsSection = new ProjectsSection();
            PageOwnerInfo = new PageOwnerInfo();
        }
    }

    public class ProjectsSection
    {
        public string ProjTitle1st { get; set; }

        public string ProjSubtitle1st { get; set; }

        public string ProjImg1st { get; set; }


        public string ProjTitle2nd { get; set; }

        public string ProjSubtitle2nd { get; set; }

        public string ProjImg2nd { get; set; }


        public string ProjTitle3rd { get; set; }

        public string ProjSubtitle3rd { get; set; }

        public string ProjImg3rd { get; set; }
    }

    public class PageOwnerInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Age { get; set; }

        public SocialMedia SocialMedia { get; set; }

        public PageOwnerInfo()
        {
            SocialMedia = new SocialMedia();
        }
    }

    public class SocialMedia
    {
        public string Twitter { get; set; }

        public string Facebook { get; set; }

        public string Github { get; set; }
    }
}