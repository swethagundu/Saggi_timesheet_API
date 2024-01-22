using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaggiTimeSheetAPP.Models;

namespace SaggiTimeSheetAPP.Controllers
{
    public class TimesheetEntryController : Controller
    {
            Uri baseAddress = new Uri("http://localhost:8082/");
            private readonly HttpClient _client;
            public TimesheetEntryController()
            {
                _client = new HttpClient();
                _client.BaseAddress = baseAddress;
            }
            [HttpGet]
            public IActionResult Index()
            {
                List<TimesheetEntry> timesheetentryList = new List<TimesheetEntry>();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/TimesheetEntries").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    timesheetentryList = JsonConvert.DeserializeObject<List<TimesheetEntry>>(data);
                }
                return View(timesheetentryList);
            }
            //[HttpGet]
            //public IActionResult Create()
            //{
            //    return View();
            //}
            //[HttpPost]
            //public IActionResult Create(TimesheetEntry timesheetentry)
            //{
            //    try
            //    {
            //        string data = JsonConvert.SerializeObject(timesheetentry);
            //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            //        HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "api/TimesheetEntries/", content).Result;
            //        if (response.IsSuccessStatusCode)
            //        {

            //            return RedirectToAction("Index");
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        return View();
            //    }
            //    return View();
            //}

            //[HttpGet] // to update the date
            //public IActionResult Edit(int id)
            //{
            //    try
            //    {
            //    TimesheetEntry timesheetentry = new TimesheetEntry();
            //        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/TimesheetEntries/" + id).Result;
            //        if (response.IsSuccessStatusCode)
            //        {
            //            //if true deserialize the data
            //            string data = response.Content.ReadAsStringAsync().Result;
            //            timesheetentry = JsonConvert.DeserializeObject<TimesheetEntry>(data);
            //        }
            //        return View(timesheetentry);
            //    }
            //    catch (Exception)
            //    {

            //        return View();
            //    }


            //}
            //[HttpPost]
            //public IActionResult Edit(TimesheetEntry timesheetentry)
            //{
            //    //serialize into json format
            //    try
            //    {
            //        string data = JsonConvert.SerializeObject(timesheetentry);
            //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            //        HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "api/TimesheetEntries/" + timesheetentry.TimeSheetEntryId, content).Result;
            //        if (response.IsSuccessStatusCode)
            //        {
            //            return RedirectToAction("Index");
            //        }
            //        return View();
            //    }
            //    catch (Exception)
            //    {

            //        return View();
            //    }


            //}
            //[HttpGet]
            //public IActionResult Delete(int id)
            //{
            //    try
            //    {
            //    TimesheetEntry timesheetentry = new TimesheetEntry();
            //        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/TimesheetEntries/" + id).Result;

            //        if (response.IsSuccessStatusCode)
            //        {
            //            string data = response.Content.ReadAsStringAsync().Result;
            //            timesheetentry = JsonConvert.DeserializeObject<TimesheetEntry>(data);
            //        }
            //        return View(timesheetentry);
            //    }
            //    catch (Exception)
            //    {

            //        return View();
            //    }

            //}
            //[HttpPost, ActionName("Delete")]
            //public IActionResult DeleteConfirmed(int id)
            //{
            //    try
            //    {
            //        HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "api/TimesheetEntries/" + id).Result;
            //        if (!response.IsSuccessStatusCode)
            //        {
            //            return RedirectToAction("Index");
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        return View();
            //    }
            //    return View();
            //}
        }
    }

