using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PSS.Models
{
    public class PortfolioModel
    {
        public string PageTitle { get; set; }

        public string PageSubTitle { get; set; }

        public string ButtonText { get; set; }

        public List<PortfolioPhoto> PortfolioPhotos { get; set; }

        public PageOwnerInfo PageOwnerInfo { get; set; }

        public PortfolioModel()
        {
            PortfolioPhotos = new List<PortfolioPhoto>();
            PageOwnerInfo = new PageOwnerInfo();
        }
    }

    public class PortfolioPhoto
    {
        public int ID { get; set; }

        public bool IfActual { get; set; }

        public string ImgPath { get; set; }

        [MaxLength(37)]
        public string ImgDesc { get; set; }
    }
}