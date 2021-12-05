using System;
using System.IO;
using System.Linq;
using System.Security.Principal;
using PSSCMS.Models;
using PSSCMS.Models.Cms;
using PageOwnerInfo = PSSCMS.Models.Cms.PageOwnerInfo;
using SocialMedia = PSSCMS.Models.Cms.SocialMedia;

namespace PSSCMS
{
    public static class CmsUtils
    {
        public static Models.Cms.GeneralModel GetGeneralModel()
        {
            var result = new Models.Cms.GeneralModel();

            try
            {
                using (var db = new CmsPssEntities())
                {
                    #region Owner

                    var ownerDbl = (from dbo in db.PageOwners
                                    select dbo).ToList();

                    if (ownerDbl == null || ownerDbl.Count <= 0)
                    {
                        return new PSSCMS.Models.Cms.GeneralModel();
                    }

                    var ownerDb = ownerDbl.FirstOrDefault();
                    result.OwnerSettings = new PageOwnerInfo()
                    {
                        FirstName = ownerDb.Name,
                        LastName = ownerDb.LastName,
                        Age = ownerDb.Age,
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

                }

                return result;
            }
            catch (Exception e)
            {
                return new PSSCMS.Models.Cms.GeneralModel();
            }
        }

        public static Models.PageOwnerInfo ChangeOwnerValue(string nVal, string changedVal, string nVal2 = "")
        {
            var result = new Models.PageOwnerInfo();

            try
            {
                using (var db = new CmsPssEntities())
                {
                    #region Owner

                    var ownerDbl = (from dbo in db.PageOwners
                                    select dbo).ToList();

                    if (ownerDbl == null || ownerDbl.Count <= 0)
                    {
                        return new Models.PageOwnerInfo();
                    }

                    var ownerDb = ownerDbl.FirstOrDefault();

                    if (changedVal == "fname")
                    {
                        ownerDb.Name = nVal ?? ownerDb.Name;
                        db.SaveChanges();
                    }
                    else if (changedVal == "lname")
                    {
                        ownerDb.LastName = nVal ?? ownerDb.LastName;
                        db.SaveChanges();
                    }
                    else if (changedVal == "flname")
                    {
                        ownerDb.Name = nVal ?? ownerDb.Name;
                        ownerDb.LastName = nVal2 ?? ownerDb.LastName;
                        db.SaveChanges();
                    }
                    else if (changedVal == "age")
                    {
                        int.TryParse(nVal, out int iVal);
                        ownerDb.Age = iVal > 0 ? iVal : ownerDb.Age;
                        db.SaveChanges();
                    }
                    else if (changedVal == "address")
                    {
                        ownerDb.Address = nVal ?? ownerDb.Address;
                        db.SaveChanges();
                    }
                    else if (changedVal == "mail")
                    {
                        ownerDb.Email = nVal ?? ownerDb.Email;
                        db.SaveChanges();
                    }
                    else if (changedVal == "phone")
                    {
                        ownerDb.PhoneNumber = nVal ?? ownerDb.PhoneNumber;
                        db.SaveChanges();
                    }
                    else if (changedVal == "twitter")
                    {
                        ownerDb.Twitter = nVal ?? ownerDb.Twitter;
                        db.SaveChanges();
                    }
                    else if (changedVal == "fb")
                    {
                        ownerDb.Facebook = nVal ?? ownerDb.Facebook;
                        db.SaveChanges();
                    }
                    else if (changedVal == "github")
                    {
                        ownerDb.Github = nVal ?? ownerDb.Github;
                        db.SaveChanges();
                    }

                    #endregion

                }

                return result;
            }
            catch (Exception e)
            {
                return new Models.PageOwnerInfo();
            }
        }

        public static Models.PageOwnerInfo GetOwner()
        {
            var result = new Models.PageOwnerInfo();

            try
            {
                using (var db = new CmsPssEntities())
                {
                    #region Owner

                    var ownerDbl = (from dbo in db.PageOwners
                                    select dbo).ToList();

                    if (ownerDbl == null || ownerDbl.Count <= 0)
                    {
                        return new Models.PageOwnerInfo();
                    }

                    var ownerDb = ownerDbl.FirstOrDefault();
                    result = new Models.PageOwnerInfo()
                    {
                        FirstName = ownerDb.Name,
                        LastName = ownerDb.LastName,
                        Age = ownerDb.Age,
                        Address = ownerDb.Address,
                        Email = ownerDb.Email,
                        PhoneNumber = ownerDb.PhoneNumber,
                        SocialMedia = new Models.SocialMedia
                        {
                            Twitter = ownerDb.Twitter,
                            Facebook = ownerDb.Facebook,
                            Github = ownerDb.Github
                        }
                    };

                    #endregion

                }

                return result;
            }
            catch (Exception e)
            {
                return new Models.PageOwnerInfo();
            }
        }

        public static Session GetEventFromCalendar(string seshID)
        {
            try
            {
                int.TryParse(seshID, out int iID);
                using (var db = new CmsPssEntities())
                {
                    var seshDb = (from dbo in db.Sessions.AsNoTracking()
                                  where dbo.ID == iID
                                  select dbo).ToList();

                    if (seshDb == null || seshDb.Count != 1)
                    {
                        return new Session();
                    }

                    return seshDb.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                return new Session();
            }
        }

        public static Session PostEventChanges(Session model)
        {
            try
            {
                using (var db = new CmsPssEntities())
                {
                    var seshDb = (from dbo in db.Sessions
                        where dbo.ID == model.ID
                        select dbo).ToList();

                    if (seshDb == null || seshDb.Count != 1)
                    {
                        return new Session();
                    }

                    var sessionDb = seshDb.FirstOrDefault();
                    sessionDb.Name = model.Name;
                    sessionDb.LastName = model.LastName;
                    sessionDb.Mail = model.Mail;
                    sessionDb.PhoneNumber = model.PhoneNumber;
                    sessionDb.IP = model.IP;
                    sessionDb.SessionDate = model.SessionDate;
                    sessionDb.SessionTypeID = model.SessionTypeID;
                    sessionDb.SessionStatusID = model.SessionStatusID;
                    db.SaveChanges();

                    return sessionDb;
                }
            }
            catch (Exception e)
            {
                return new Session();
            }
        }

        public static PSSCMS.Models.HomeModel PostHomeTopChanges(PSSCMS.Models.HomeModel model)
        {
            try
            {
                using (var db = new CmsPssEntities())
                {
                    var homePagel = (from dbo in db.HomePages
                        where dbo.ID == "main_text_set"
                        select dbo).ToList();

                    if (homePagel == null || homePagel.Count <= 0)
                    {
                        return new PSSCMS.Models.HomeModel();
                    }

                    var homePage = homePagel.FirstOrDefault();
                    homePage.PageTitle = model.PageTitle;
                    homePage.PageSubTitle= model.PageSubTitle;
                    db.SaveChanges();

                    return model;
                }
            }
            catch (Exception e)
            {
                return new PSSCMS.Models.HomeModel();
            }
        }

        public static PSSCMS.Models.HomeModel PostHomeBotChanges(PSSCMS.Models.HomeModel model)
        {
            try
            {
                using (var db = new CmsPssEntities())
                {
                    var homePagel = (from dbo in db.HomePages
                        where dbo.ID == "main_text_set"
                        select dbo).ToList();

                    if (homePagel == null || homePagel.Count <= 0)
                    {
                        return new PSSCMS.Models.HomeModel();
                    }

                    var homePage = homePagel.FirstOrDefault();
                    homePage.AdditionalTitle = model.AdditionalTitle;
                    homePage.AdditionalSubtitle = model.AdditionalSubtitle;
                    db.SaveChanges();

                    return model;
                }
            }
            catch (Exception e)
            {
                return new PSSCMS.Models.HomeModel();
            }
        }

        public static PSSCMS.Models.HomeModel GetHomeModel()
        {
            var result = new PSSCMS.Models.HomeModel();

            try
            {
                using (var db = new CmsPssEntities())
                {
                    #region Owner

                    var ownerDbl = (from dbo in db.PageOwners
                                    select dbo).ToList();

                    if (ownerDbl == null || ownerDbl.Count <= 0)
                    {
                        return new PSSCMS.Models.HomeModel();
                    }

                    var ownerDb = ownerDbl.FirstOrDefault();
                    result.PageOwnerInfo = new PSSCMS.Models.PageOwnerInfo
                    {
                        FirstName = ownerDb.Name,
                        LastName = ownerDb.LastName,
                        Address = ownerDb.Address,
                        Email = ownerDb.Email,
                        PhoneNumber = ownerDb.PhoneNumber,
                        SocialMedia = new PSSCMS.Models.SocialMedia
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
                        return new PSSCMS.Models.HomeModel();
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
                        return new PSSCMS.Models.HomeModel();
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
                return new PSSCMS.Models.HomeModel();
            }
        }

        public static PSSCMS.Models.PortfolioModel GetPortfolioModel()
        {
            //FilesMigrationProcess();
            var result = new PSSCMS.Models.PortfolioModel();

            try
            {
                using (var db = new CmsPssEntities())
                {
                    #region Owner

                    var ownerDbl = (from dbo in db.PageOwners
                                    select dbo).ToList();

                    if (ownerDbl == null || ownerDbl.Count <= 0)
                    {
                        return new PSSCMS.Models.PortfolioModel();
                    }

                    var ownerDb = ownerDbl.FirstOrDefault();
                    result.PageOwnerInfo = new PSSCMS.Models.PageOwnerInfo
                    {
                        FirstName = ownerDb.Name,
                        LastName = ownerDb.LastName,
                        Address = ownerDb.Address,
                        Email = ownerDb.Email,
                        PhoneNumber = ownerDb.PhoneNumber,
                        SocialMedia = new PSSCMS.Models.SocialMedia
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
                        return new PSSCMS.Models.PortfolioModel();
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
                        return new PSSCMS.Models.PortfolioModel();
                    }

                    foreach (var item in photoDbl)
                    {
                        result.PortfolioPhotos.Add(new PSSCMS.Models.PortfolioPhoto
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
                return new PSSCMS.Models.PortfolioModel();
            }
        }

        public static PSSCMS.Models.Cms.CalendarModel GetEventsToCalendar()
        {
            var result = new PSSCMS.Models.Cms.CalendarModel();

            try
            {
                using (var db = new CmsPssEntities())
                {
                    #region Owner

                    var sessionsDbl = (from dbo in db.Sessions
                                       select dbo).ToList();

                    if (sessionsDbl == null || sessionsDbl.Count <= 0)
                    {
                        return new PSSCMS.Models.Cms.CalendarModel();
                    }

                    foreach (var sess in sessionsDbl)
                    {
                        if (sess.SessionStatusID == (int)SessionStatusEnum.Pending)
                        {
                            result.PendingSessionModel.Add(new PendingSessionModel
                            {
                                ID = sess.ID,
                                FirstName = sess.Name,
                                LastName = sess.LastName,
                                Mail = sess.Mail,
                                SessionDate = sess.SessionDate,
                                PhoneNumber = sess.PhoneNumber,
                                SessionTypeID = sess.SessionTypeID,
                                SessionStatusID = sess.SessionStatusID
                            });
                        }
                        else if (sess.SessionStatusID == (int)SessionStatusEnum.Approved)
                        {
                            result.ApprovedSessionModel.Add(new ApprovedSessionModel
                            {
                                ID = sess.ID,
                                FirstName = sess.Name,
                                LastName = sess.LastName,
                                Mail = sess.Mail,
                                SessionDate = sess.SessionDate,
                                PhoneNumber = sess.PhoneNumber,
                                SessionTypeID = sess.SessionTypeID,
                                SessionStatusID = sess.SessionStatusID
                            });
                        }
                        else if (sess.SessionStatusID == (int)SessionStatusEnum.Canceled)
                        {
                            result.CanceledSessionModel.Add(new CanceledSessionModel
                            {
                                ID = sess.ID,
                                FirstName = sess.Name,
                                LastName = sess.LastName,
                                Mail = sess.Mail,
                                SessionDate = sess.SessionDate,
                                PhoneNumber = sess.PhoneNumber,
                                SessionTypeID = sess.SessionTypeID,
                                SessionStatusID = sess.SessionStatusID
                            });
                        }
                        else
                        {
                            result.OtherSessionModel.Add(new OtherSessionModel
                            {
                                ID = sess.ID,
                                FirstName = sess.Name,
                                LastName = sess.LastName,
                                Mail = sess.Mail,
                                SessionDate = sess.SessionDate,
                                PhoneNumber = sess.PhoneNumber,
                                SessionTypeID = sess.SessionTypeID,
                                SessionStatusID = sess.SessionStatusID
                            });
                        }
                    }

                    #endregion
                }

                return result;
            }
            catch (Exception e)
            {
                return new PSSCMS.Models.Cms.CalendarModel();
            }
        }

        public static Models.CustomizePhotoRequest GetPhotoToCustomize_ByID(string sId)
        {
            var result = new Models.CustomizePhotoRequest();
            try
            {
                if (int.TryParse(sId, out int iId))
                {
                    using (var db = new CmsPssEntities())
                    {
                        var photoExactDb = (from dbo in db.PortfolioPhotos.AsNoTracking()
                                            where dbo.ID == iId
                                            select dbo).ToList();

                        if (photoExactDb == null || photoExactDb.Count != 1)
                        {
                            return new PSSCMS.Models.CustomizePhotoRequest();
                        }

                        var photoExact = photoExactDb.FirstOrDefault();
                        result.IfActual = photoExact.IfActual;
                        result.ID = photoExact.ID;
                        result.ImgDesc = photoExact.ImgDescription;
                        result.ImgPath = photoExact.ImgPath;
                        result.Errors = string.Empty;
                    }

                    return result;
                }
                else
                {
                    return new Models.CustomizePhotoRequest { Errors = "Error during parsing ID occurred!" };
                }
            }
            catch (Exception e)
            {
                return new Models.CustomizePhotoRequest { Errors = "Error during img getting occurred: " + e.Message };
            }
        }

        public static Models.CustomizePhotoRequest CustomizePhoto(Models.CustomizePhotoRequest request)
        {
            var result = new Models.CustomizePhotoRequest();
            try
            {
                using (var db = new CmsPssEntities())
                {
                    if (request.ID == -1 && request.file != null)
                    {
                        string targetPath = @"E:\studia\PSSCMS\Repos";
                        string path = Path.Combine(targetPath, Path.GetFileName(request.file.FileName));
                        request.file.SaveAs(path);

                        request.ImgPath = @"../../Repos/" + request.file.FileName;
                        FilesMigrationProcess();

                        db.PortfolioPhotos.Add(new PortfolioPhoto
                        {
                            IfActual = true,
                            ImgDescription = string.Empty,
                            ImgPath = request.ImgPath
                        });
                        db.SaveChanges();

                        return new CustomizePhotoRequest { Errors = string.Empty };
                    }

                    var photoExactDb = (from dbo in db.PortfolioPhotos
                                        where dbo.ID == request.ID && dbo.IfActual == true
                                        select dbo).ToList();

                    if (photoExactDb == null || photoExactDb.Count != 1)
                    {
                        return new Models.CustomizePhotoRequest { Errors = "No images with this ID! " };
                    }

                    var photoExact = photoExactDb.FirstOrDefault();
                    photoExact.ImgDescription =
                        photoExact.ImgDescription != request.ImgDesc
                        ? request?.ImgDesc ?? photoExact.ImgDescription
                        : photoExact.ImgDescription;

                    if (request.file != null)
                    {
                        string targetPath = @"E:\studia\PSSCMS\Repos";
                        string path = Path.Combine(targetPath, Path.GetFileName(request.file.FileName));
                        request.file.SaveAs(path);

                        request.ImgPath = @"../../Repos/" + request.file.FileName;
                        FilesMigrationProcess();
                    }

                    photoExact.ImgPath =
                        photoExact.ImgPath != request.ImgPath
                        ? request?.ImgPath ?? photoExact.ImgPath
                        : photoExact.ImgPath;

                    int bRes = db.SaveChanges();

                    if (bRes == 1)
                        result.Errors = string.Empty;
                    else
                        result.Errors = "Zapisana ilość wierszy jest różna od 1!";
                }

                return result;
            }
            catch (Exception e)
            {
                return new Models.CustomizePhotoRequest { Errors = "Error during img getting occurred: " + e.Message };
            }
        }

        public static bool FilesMigrationProcess()
        {
            string fileName = "";
            string sourcePath = @"E:\studia\PSSCMS\Repos";
            string targetPath = @"E:\studia\PSS\Repos";

            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                foreach (string s in files)
                {
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }

                return true;
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
                return false;
            }
        }
    }
}