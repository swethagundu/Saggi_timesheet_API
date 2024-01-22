using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaggiTimeSheetAPP.Models;

namespace SaggiTimeSheetAPP.Controllers
{
    public class ProjectController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:8082/");
        private readonly HttpClient _client;
        public ProjectController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        //----GET Course DATA-----//
        [HttpGet]

        public IActionResult Index()
        {
            List<Project> projectlist = new List<Project>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Projects").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                projectlist = JsonConvert.DeserializeObject<List<Project>>(data);
            }
            return View(projectlist);
        }


        ////////-----CREATE Course-----//

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Project project)
        {
            try
            {
                string data = JsonConvert.SerializeObject(project);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress +
                    "api/Projects/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Project Created";
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
        //////////---update

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {


                Project project = new Project();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Projects/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    project = JsonConvert.DeserializeObject<Project>(data);
                }
                return View(project);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();

            }

        }
        [HttpPost]
        public IActionResult Edit(Project project)
        {

            string data = JsonConvert.SerializeObject(project);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "api/Projects/" + project.ProjectId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        //////////-------------Delete-------------------

        [HttpGet]

        public IActionResult Delete(int id)
        {
            try
            {

                Project project = new Project();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/Projects/" + id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    project = JsonConvert.DeserializeObject<Project>(data);
                }
                return View(project);
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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "api/Projects/" + id).Result;
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
