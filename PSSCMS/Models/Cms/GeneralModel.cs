namespace PSSCMS.Models.Cms
{
    public class GeneralModel
    {
        public PageOwnerInfo OwnerSettings { get; set; }

        public GeneralModel()
        {
            OwnerSettings = new PageOwnerInfo();
        }
    }

    public class PageOwnerInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

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