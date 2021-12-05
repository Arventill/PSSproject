using System.Web.Mvc;

namespace PSSCMS.Controllers
{
    public class CmsHomeController : Controller
    {

        public ActionResult Login() => View(nameof(Login));

        #region General

        public ActionResult Index()
        {
            var model = CmsUtils.GetGeneralModel();
            return View(nameof(Index), model);
        }

        public ActionResult ShowModalOwnerChange(string place)
        {
            var model = CmsUtils.GetGeneralModel();
            return PartialView(place, model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeFirstName_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "fname");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_FNameChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeLastName_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "lname");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_LNameChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeFirstLastName_PostModal(string nVal, string nVal2)
        {
            CmsUtils.ChangeOwnerValue(nVal, "flname", nVal2);
            var model = CmsUtils.GetGeneralModel();
            return View(nameof(Index), model);
        }

        [HttpPost]
        public ActionResult ChangeAge_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "age");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_AgeChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeAddress_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "address");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_AddressChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeEmail_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "mail");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_EmailChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangePhone_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "phone");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_PhoneChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeTwitter_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "twitter");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_TwitterChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeFb_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "fb");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_FacebookChange", model.OwnerSettings);
        }

        [HttpPost]
        public ActionResult ChangeGithub_PostModal(string nVal)
        {
            CmsUtils.ChangeOwnerValue(nVal, "github");
            var model = CmsUtils.GetGeneralModel();
            ViewBag.Status = "PASSED";
            return PartialView("_GeneralModal_GithubChange", model.OwnerSettings);
        }

        #endregion

        #region MyRegion
        public ActionResult Home()
        {
            var model = CmsUtils.GetHomeModel();
            return View(nameof(Home), model);
        }

        public ActionResult HomeTopEdit_ShowModal(string idd)
        {
            ViewBag.Status = "NOTHING";
            var model = CmsUtils.GetHomeModel();
            return PartialView("_HomeModal_TopValueChange", model);
        }

        [HttpPost]
        public ActionResult HomeTopEdit_PostModal(PSSCMS.Models.HomeModel postModel)
        {
            var model = CmsUtils.PostHomeTopChanges(postModel);
            ViewBag.Status = "PASSED";
            return PartialView("_HomeModal_TopValueChange", model);
        }

        public ActionResult HomeBotEdit_ShowModal(string idd)
        {

            ViewBag.Status = "NOTHING";
            var model = CmsUtils.GetHomeModel();
            return PartialView("_HomeModal_BotValueChange", model);
        }

        [HttpPost]
        public ActionResult HomeBotEdit_PostModal(PSSCMS.Models.HomeModel postModel)
        {
            var model = CmsUtils.PostHomeBotChanges(postModel);
            ViewBag.Status = "PASSED";
            return PartialView("_HomeModal_BotValueChange", model);
        }

        #region PortfolioReg

        public ActionResult Portfolio()
        {
            var model = CmsUtils.GetPortfolioModel();
            return View(nameof(Portfolio), model);
        }

        [HttpPost]
        public ActionResult PortfolioCustomizePhoto_ShowModal(string photoID)
        {
            var model = CmsUtils.GetPhotoToCustomize_ByID(photoID);
            return PartialView("_PortfolioModal_CustomizePhoto", model);
        }

        [HttpPost]
        public ActionResult PortfolioCustomizePhoto_PostModal(Models.CustomizePhotoRequest postModel)
        {
            if (postModel == null || postModel.ImgDesc.Length > 37)
            {
                postModel.Errors = "Model cannot be null and the desc cannot be longer than 37 characters!";
                return PartialView("_PortfolioModal_CustomizePhoto", postModel);
            }

            var model = CmsUtils.CustomizePhoto(postModel);
            ViewBag.Status = "PASSED";
            return PartialView("_PortfolioModal_CustomizePhoto", model);
        }

        [HttpPost]
        public ActionResult PortfolioAddPhoto_ShowModal(string photoID)
        {
            int.TryParse(photoID, out int pid);
            var model = new Models.CustomizePhotoRequest { ID = pid };
            return PartialView("_PortfolioModal_AddPhoto", model);
        }

        [HttpPost]
        public ActionResult PortfolioAddPhoto_PostModal(Models.CustomizePhotoRequest postModel)
        {
            if (postModel == null)
            {
                postModel.Errors = "Model cannot be null!";
                return PartialView("_PortfolioModal_CustomizePhoto", postModel);
            }

            var model = CmsUtils.CustomizePhoto(postModel);
            if (string.IsNullOrWhiteSpace(model.Errors))
                ViewBag.Status = "PASSED";

            return PartialView("_PortfolioModal_CustomizePhoto", model);
        }

        #endregion

        public ActionResult Calendar()
        {
            var model = CmsUtils.GetEventsToCalendar();
            return View(nameof(Calendar), model);
        }

        public ActionResult EditCalendarEvent_ShowModal(string seshID)
        {
            var model = CmsUtils.GetEventFromCalendar(seshID);
            return PartialView("_CalendarModal_EditRow", model);
        }

        public ActionResult EditCalendarEvent_PostModal(Session postModel)
        {
            _ = CmsUtils.PostEventChanges(postModel);
            ViewBag.Status = "PASSED";
            return PartialView("_CalendarModal_EditRow", postModel);
        }

        public ActionResult Cards() => View(nameof(Cards));
        #endregion
    }
}