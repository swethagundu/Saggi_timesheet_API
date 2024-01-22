using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaggiTimeSheetAPP.Models;

namespace SaggiTimeSheetAPP.Controllers
{
    public class RoleController : Controller
    {

        Uri baseAddress = new Uri("http://localhost:8082/");
        // Uri baseAddress = new Uri("https://localhost:7038/");
        private readonly HttpClient _client;
        public RoleController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        //----GET Course DATA-----//
        [HttpGet]

        public IActionResult Index()
        {
            List<Role> roleslist = new List<Role>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Role").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                roleslist = JsonConvert.DeserializeObject<List<Role>>(data);
            }
            return View(roleslist);
        }


        ////-----CREATE Course-----//

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Role role)
        {
            try
            {
                string data = JsonConvert.SerializeObject(role);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress +
                    "api/Role/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Role Created";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }
        ////---update

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {


                Role role = new Role();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Role/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    role = JsonConvert.DeserializeObject<Role>(data);
                }
                return View(role);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }
        [HttpPost]
        public IActionResult Edit(Role role)
        {

            string data = JsonConvert.SerializeObject(role);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "api/Role/" + role.RoleId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        ////-------------Delete-------------------

        [HttpGet]

        public IActionResult Delete(int id)
        {
            try
            {

                Role role = new Role();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Role/" + id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    role = JsonConvert.DeserializeObject<Role>(data);
                }
                return View(role);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }


        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "api/Role/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();

        }
    }
}