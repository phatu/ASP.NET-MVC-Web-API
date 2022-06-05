using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Details()
        {
            IEnumerable<PersonModel> personList;
            HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Person").Result;
            personList = response.Content.ReadAsAsync<IEnumerable<PersonModel>>().Result;

            return View(personList);
        }


        [HttpGet]
        public ActionResult AddOrEditPerson(int Id = 0)
        {
            PersonModel model = new PersonModel();

            if (Id == 0)
                return View(new PersonModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.GetAsync("Person/" + Id.ToString()).Result;
                return View(response.Content.ReadAsAsync<PersonModel>().Result);
            }

        }

        [HttpPost]
        public ActionResult AddOrEditPerson(PersonModel p)
        {
            if (p.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.webApiClient.PostAsJsonAsync("Person", p).Result;
                TempData["SuccessMessage"] = "Saved successfully";
            }
            else
            {

                HttpResponseMessage response = GlobalVariables.webApiClient.PutAsJsonAsync("Person/" + p.Id, p).Result;
                TempData["SuccessMessage"] = "Updated successfully";

            }
            return RedirectToAction("Details");
        }

        [HttpPost]
        public ActionResult DeletePerson(int Id)
        {
            HttpResponseMessage response = GlobalVariables.webApiClient.DeleteAsync("Person/" + Id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted successfully";
            return Json("Details", JsonRequestBehavior.AllowGet);
        }



    }
}