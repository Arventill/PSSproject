using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls.Expressions;
using PSS.Models;

namespace PSS
{
    public static class Utils
    {
        public static List<TextsModel> GetAllTexts()
        {
            var result = new List<TextsModel>();

            try
            {
                using (var db = new PssEntities())
                {
                    var resDb = (from a in db.Texts
                                 select a).ToList();

                    if (resDb == null || resDb.Count <= 0)
                    {
                        return result;
                    }

                    foreach (var item in resDb)
                    {
                        result.Add(new TextsModel
                        {
                            Name = item.Name,
                            Title = item.Title,
                            Desc = item.Description,
                            Fill = item.Fill
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<TextsModel>();
            }

            return result;
        }

        public static PSS.Models.HomeModel GetHomeModel()
        {
            var result = new PSS.Models.HomeModel();

            try
            {
                using (var db = new PssEntities())
                {
                    #region Owner

                    var ownerDbl = (from dbo in db.PageOwners
                                    select dbo).ToList();

                    if (ownerDbl == null || ownerDbl.Count <= 0)
                    {
                        return GetDefaultHomeModel();
                    }

                    var ownerDb = ownerDbl.FirstOrDefault();
                    result.PageOwnerInfo = new PageOwnerInfo
                    {
                        FirstName = ownerDb.Name,
                        LastName = ownerDb.LastName,
                        Address = ownerDb.Address,
                        Email = ownerDb.Email,
                        PhoneNumber = ownerDb.PhoneNumber,
                        SocialMedia = new SocialMedia
                        {
                            Twitter = ownerDb.Twitter,
                            Facebook = ownerDb.Facebook,
                            Github = ownerDb.Github
                        }
                    };

                    #endregion

                    #region Home

                    var homePagel = (from dbo in db.HomePages
                                     where dbo.ID == "main_text_set"
                                     select dbo).ToList();

                    if (homePagel == null || homePagel.Count <= 0)
                    {
                        return GetDefaultHomeModel();
                    }

                    var homePage = homePagel.FirstOrDefault();
                    result.PageTitle = homePage.PageTitle;
                    result.PageSubTitle = homePage.PageSubTitle;
                    result.ButtonText = homePage.ButtonText;
                    result.AdditionalTitle = homePage.AdditionalTitle;
                    result.AdditionalSubtitle = homePage.AdditionalSubtitle;

                    #endregion

                    #region Projects

                    var projectsDbl = (from dbo in db.HomeProjects
                                       orderby dbo.ID ascending
                                       select dbo).ToList();

                    if (projectsDbl == null || projectsDbl.Count != 3)
                    {
                        return GetDefaultHomeModel();
                    }

                    result.ProjectsSection.ProjTitle1st = projectsDbl[0].ProjTitle;
                    result.ProjectsSection.ProjSubtitle1st = projectsDbl[0].ProjSubtitle;
                    result.ProjectsSection.ProjImg1st = projectsDbl[0].ProjImgPath;

                    result.ProjectsSection.ProjTitle2nd = projectsDbl[1].ProjTitle;
                    result.ProjectsSection.ProjSubtitle2nd = projectsDbl[1].ProjSubtitle;
                    result.ProjectsSection.ProjImg2nd = projectsDbl[1].ProjImgPath;

                    result.ProjectsSection.ProjTitle3rd = projectsDbl[2].ProjTitle;
                    result.ProjectsSection.ProjSubtitle3rd = projectsDbl[2].ProjSubtitle;
                    result.ProjectsSection.ProjImg3rd = projectsDbl[2].ProjImgPath;

                    #endregion
                }

                return result;
            }
            catch (Exception e)
            {
                return GetDefaultHomeModel();
            }
        }

        public static bool SetSession(BookingModel model)
        {
            if (model == null)
            {
                return false;
            }

            try
            {
                using (var db = new PssEntities())
                {
                    var booksDb = (from b in db.Sessions.AsNoTracking()
                        where b.IP == model.IP
                        orderby b.RequestDate descending
                        select b).ToList();

                    if (booksDb == null || booksDb.Count() != 0 || booksDb?.FirstOrDefault()?.IP == model.IP)
                    {
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(model.FName)
                        || string.IsNullOrWhiteSpace(model.LName)
                        || string.IsNullOrWhiteSpace(model.Email)
                        || string.IsNullOrWhiteSpace(model.Phone)
                        || string.IsNullOrWhiteSpace(model.SessionType)
                        || model.DateTime < DateTime.Now || model.IP == "0")
                    {
                        return false;
                    }

                    var book = new Session
                    {
                        Name = model.FName,
                        LastName = model.LName,
                        Mail = model.Email,
                        PhoneNumber = model.Phone,
                        SessionDate = model.DateTime,
                        SessionStatusID = (int)SessionStatusEnum.Pending,
                        SessionTypeID = (int)SessionTypeEnum.Other,
                        IP = model.IP,
                        RequestDate = DateTime.Now
                    };

                    db.Sessions.Add(book);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static PSS.Models.HomeModel GetDefaultHomeModel() => new PSS.Models.HomeModel
        {
            PageTitle = "PHOTOGRAPHY",
            PageSubTitle = "Do you want to work with me? Feel free to book a day in the calendar tab above.",
            ButtonText = "SCROLL DOWN",
            AdditionalTitle = "Built with Bootstrap 5",
            AdditionalSubtitle = "Grayscale is a free Bootstrap theme created by Start Bootstrap. It can be yours right now, simply download the template on the preview page. The theme is open source, and you can use it for any purpose, personal or commercial. ",
            ProjectsSection = new ProjectsSection
            {
                ProjTitle1st = "Shoreline",
                ProjSubtitle1st = "Grayscale is open source and MIT licensed. This means you can use it for any project - even commercial projects! Download it, customize it, and publish your website!",
                ProjImg1st = "../../Content/Addons/assets/img/bg-masthead.jpg",
                ProjTitle2nd = "Misty",
                ProjSubtitle2nd = "An example of where you can put an image of a project, or anything else, along with a description.",
                ProjImg2nd = "../../Content/Addons/assets/img/demo-image-01.jpg",
                ProjTitle3rd = "Mountains",
                ProjSubtitle3rd = "Another example of a project with its respective description. These sections work well responsively as well, try this theme on a small screen!",
                ProjImg3rd = "../../Content/Addons/assets/img/demo-image-02.jpg"
            },
            PageOwnerInfo = new PageOwnerInfo
            {
                FirstName = "Filip",
                LastName = "Nowicki",
                Address = "4923 Market Street, Orlando FL",
                Email = "filip.nowicki1999@gmail.com",
                PhoneNumber = "+1 (555) 902-8832",
                SocialMedia = new SocialMedia
                {
                    Twitter = "https://twitter.com/arventill",
                    Facebook = "https://pl-pl.facebook.com/arventill",
                    Github = "https://github.com/Arventill"
                }
            }
        };

        public static PSS.Models.PortfolioModel GetPortfolioModel()
        {
            var result = new PSS.Models.PortfolioModel();

            try
            {
                using (var db = new PssEntities())
                {
                    #region Owner

                    var ownerDbl = (from dbo in db.PageOwners
                                    select dbo).ToList();

                    if (ownerDbl == null || ownerDbl.Count <= 0)
                    {
                        return GetDefaultPortfolioModel();
                    }

                    var ownerDb = ownerDbl.FirstOrDefault();
                    result.PageOwnerInfo = new PageOwnerInfo
                    {
                        FirstName = ownerDb.Name,
                        LastName = ownerDb.LastName,
                        Address = ownerDb.Address,
                        Email = ownerDb.Email,
                        PhoneNumber = ownerDb.PhoneNumber,
                        SocialMedia = new SocialMedia
                        {
                            Twitter = ownerDb.Twitter,
                            Facebook = ownerDb.Facebook,
                            Github = ownerDb.Github
                        }
                    };

                    #endregion

                    #region Portfolio

                    var portfolioPagel = (from dbo in db.PortfolioPages
                                          where dbo.ID == "main_text_set"
                                          select dbo).ToList();

                    if (portfolioPagel == null || portfolioPagel.Count <= 0)
                    {
                        return GetDefaultPortfolioModel();
                    }

                    var portfolioPage = portfolioPagel.FirstOrDefault();
                    result.PageTitle = portfolioPage.PageTitle;
                    result.PageSubTitle = portfolioPage.PageSubtitle;
                    result.ButtonText = portfolioPage.ButtonText;

                    #endregion

                    #region Photos

                    var photoDbl = (from dbo in db.PortfolioPhotos
                                    where dbo.IfActual == true
                                    select dbo).Take(100).ToList();

                    if (photoDbl == null || photoDbl.Count <= 0)
                    {
                        return GetDefaultPortfolioModel();
                    }

                    foreach (var item in photoDbl)
                    {
                        result.PortfolioPhotos.Add(new PSS.Models.PortfolioPhoto
                        {
                            ID = item.ID,
                            IfActual = item.IfActual,
                            ImgPath = item.ImgPath,
                            ImgDesc = item.ImgDescription
                        });
                    }

                    #endregion
                }

                return result;
            }
            catch (Exception e)
            {
                return GetDefaultPortfolioModel();
            }
        }

        public static PSS.Models.PortfolioModel GetDefaultPortfolioModel() => new PSS.Models.PortfolioModel
        {
            PageTitle = "PORTFOLIO",
            PageSubTitle = "The world of my best photos.",
            ButtonText = "SCROLL DOWN",
            PortfolioPhotos = new List<PSS.Models.PortfolioPhoto>
            {
                new PSS.Models.PortfolioPhoto
                {
                    ID = 1,
                    IfActual = true,
                    ImgDesc = "Desc. 1",
                    ImgPath = "../../Repos/StockSnap_0GZ83DHBCE.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 2,
                    IfActual = true,
                    ImgDesc = "Desc. 2",
                    ImgPath = "../../Repos/StockSnap_CFCYMMSHI3.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 3,
                    IfActual = true,
                    ImgDesc = "Desc. 3",
                    ImgPath = "../../Repos/StockSnap_EYXTQM6RQV.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 4,
                    IfActual = true,
                    ImgDesc = "Desc. 4",
                    ImgPath = "../../Repos/StockSnap_GID9EW0L7Z.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 5,
                    IfActual = true,
                    ImgDesc = "Desc. 5",
                    ImgPath = "../../Repos/StockSnap_KN8CIAGFRC.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 6,
                    IfActual = true,
                    ImgDesc = "Desc. 6",
                    ImgPath = "../../Repos/StockSnap_PWNRDMPYG1.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 7,
                    IfActual = true,
                    ImgDesc = "Desc. 7",
                    ImgPath = "../../Repos/StockSnap_VQT8LW1VT0.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 8,
                    IfActual = true,
                    ImgDesc = "Desc. 8",
                    ImgPath = "../../Repos/StockSnap_WD4JDFI3H6.jpg"
                },
                new PSS.Models.PortfolioPhoto
                {
                    ID = 9,
                    IfActual = true,
                    ImgDesc = "Desc. 9",
                    ImgPath = "../../Repos/StockSnap_YP7B573GOA.jpg"
                }
            },
            PageOwnerInfo = new PageOwnerInfo
            {
                FirstName = "Filip",
                LastName = "Nowicki",
                Address = "4923 Market Street, Orlando FL",
                Email = "filip.nowicki1999@gmail.com",
                PhoneNumber = "+1 (555) 902-8832",
                SocialMedia = new SocialMedia
                {
                    Twitter = "https://twitter.com/arventill",
                    Facebook = "https://pl-pl.facebook.com/arventill",
                    Github = "https://github.com/Arventill"
                }
            }
        };
    }
}