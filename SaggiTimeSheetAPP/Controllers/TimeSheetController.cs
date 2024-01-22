using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SaggiTimeSheetAPP.Models.DTO;

namespace SaggiTimeSheetAPP.Controllers
{
        public class TimeSheetController : Controller
        {

            Uri baseAddress = new Uri("http://localhost:8082/");
            private readonly HttpClient _client;

            public TimeSheetController()
            {
                _client = new HttpClient();
                _client.BaseAddress = baseAddress;
            }
            // GET: TimesheetController
            public ActionResult Index()
            {
                List<TimesheetDTO> timsheetList = new List<TimesheetDTO>();
                List<TimesheetEntryDTO> timsheetEntryList = new List<TimesheetEntryDTO>();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/TimeSheets").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    timsheetList = JsonConvert.DeserializeObject<List<TimesheetDTO>>(data);
                }
                HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "api/TimesheetEntries").Result;
                if (response1.IsSuccessStatusCode)
                {
                    string data = response1.Content.ReadAsStringAsync().Result;
                    timsheetEntryList = JsonConvert.DeserializeObject<List<TimesheetEntryDTO>>(data);
                }

                foreach (var item in timsheetList)
                {
                    item.WST = timsheetEntryList;
                }

                return View(timsheetList);
            }



        //// GET: TimesheetController/Create
        public ActionResult Create()
        {
            TimesheetDTO timesheet = new TimesheetDTO();
            TimesheetEntryDTO entry = new TimesheetEntryDTO();
            //TimesheetEntryDTO entry1 = new TimesheetEntryDTO();
            //TimesheetEntryDTO entry2 = new TimesheetEntryDTO();
            //TimesheetEntryDTO entry3 = new TimesheetEntryDTO();
            //TimesheetEntryDTO entry4 = new TimesheetEntryDTO();
            timesheet.WST.Add(entry);
            return View(timesheet);
        }

        // POST: TimesheetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimesheetDTO timeSheet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Initialize WST if null
                    timeSheet.WST = timeSheet.WST ?? new List<TimesheetEntryDTO>();

                    var timesheetEntrydto = timeSheet.WST;
                    string data = JsonConvert.SerializeObject(timeSheet);

                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "api/TimeSheets/", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserialize the response to get the created TimeSheetId
                        var responseData = response.Content.ReadAsStringAsync().Result;
                        var createdTimeSheet = JsonConvert.DeserializeObject<TimesheetDTO>(responseData);

                        // Set the TimeSheetId for each TimeSheetEntry
                        foreach (var entry in timesheetEntrydto)
                        {
                            entry.TimeSheetId = createdTimeSheet.TimeSheetId;
                        }

                        // Post the TimeSheetEntry data
                        foreach (var entry in timesheetEntrydto)
                        {
                            data = JsonConvert.SerializeObject(entry);
                            content = new StringContent(data, Encoding.UTF8, "application/json");
                            HttpResponseMessage entryResponse = _client.PostAsync(_client.BaseAddress + "api/TimesheetEntries/", content).Result;

                            if (!entryResponse.IsSuccessStatusCode)
                            {
                                // Handle the case where TimeSheetEntry creation fails
                                ModelState.AddModelError(string.Empty, "Failed to submit the timesheet entry.");
                                return View(timeSheet);
                            }
                        }

                        return RedirectToAction("Index");  // Redirect to the desired page after successful submission
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to submit the timesheet.");
                    }
                }

                // If ModelState is not valid, return the view with the model
                return View(timeSheet);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View(timeSheet);
            }
        }


		//-------------------------------------original
		////// GET: TimesheetController/Edit/5
		public ActionResult Edit(int id)
		{
			TimesheetDTO timeSheet = new TimesheetDTO();
			HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "api/TimeSheets/" + id).Result;

			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				timeSheet = JsonConvert.DeserializeObject<TimesheetDTO>(data);

				// Ensure WST is not null (initialize if empty)
				timeSheet.WST ??= new List<TimesheetEntryDTO>();
			}

			return View(timeSheet);
		}


		// POST: TimesheetController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, TimesheetDTO timesheet)
		{
			try
			{
				string data = JsonConvert.SerializeObject(timesheet);
				StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

				// Correct the URL and use PUT request for updating
				HttpResponseMessage response = _client.PutAsync(_client.BaseAddress +
					"api/TimeSheets/" + id, content).Result;
				HttpResponseMessage entryResponse = _client.PostAsync(_client.BaseAddress + "api/TimesheetEntries/", content).Result;

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}

				return View();
			}
			catch
			{
				return View();
			}
		}

		//GET: TimesheetController/Delete/5
		[HttpGet, ActionName("DeleteAction")]
		public ActionResult Delete(int id)
		{
			TimesheetDTO timesheet = new TimesheetDTO();
			HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"api/TimeSheets/{id}").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				timesheet = JsonConvert.DeserializeObject<TimesheetDTO>(data);
			}
			return View(timesheet);
		}

		// POST: TimesheetController/DeleteAction/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteAction(int id)
		{
			try
			{
				HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + $"api/TimeSheets/{id}").Result;

				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index"); // Changed return from View() to RedirectToAction("Index")
				}
				else
				{
					ModelState.AddModelError(string.Empty, $"Error deleting timesheet: {response.ReasonPhrase}");
					return View();
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "An unexpected error occurred.");
				return View();
			}
		}



		//////------------------------TTTT---------------
		///edit 
		//[HttpPost]
		//public ActionResult Edit(TimesheetDTO timeSheet)
		//{
		//	try
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			// Initialize WST if null
		//			timeSheet.WST = timeSheet.WST ?? new List<TimesheetEntryDTO>();

		//			var timesheetEntrydto = timeSheet.WST;
		//			string data = JsonConvert.SerializeObject(timeSheet);

		//			StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
		//			HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "api/TimeSheets/" + timeSheet.TimeSheetId, content).Result;

		//			if (response.IsSuccessStatusCode)
		//			{
		//				// Update the TimeSheetEntry data
		//				foreach (var entry in timesheetEntrydto)
		//				{
		//					data = JsonConvert.SerializeObject(entry);
		//					content = new StringContent(data, Encoding.UTF8, "application/json");
		//					HttpResponseMessage entryResponse;

		//					// Check if the entry has an ID (existing entry) and update it, otherwise create a new entry
		//					if (entry.TimeSheetEntryId > 0)
		//					{
		//						entryResponse = _client.PutAsync(_client.BaseAddress + "api/TimesheetEntries/" + entry.TimeSheetEntryId, content).Result;
		//					}
		//					else
		//					{
		//						entryResponse = _client.PostAsync(_client.BaseAddress + "api/TimesheetEntries/", content).Result;
		//					}

		//					if (!entryResponse.IsSuccessStatusCode)
		//					{
		//						// Handle the case where TimeSheetEntry update or creation fails
		//						ModelState.AddModelError(string.Empty, "Failed to update or create the timesheet entry.");
		//						return View(timeSheet);
		//					}
		//				}

		//				return RedirectToAction("Index");  // Redirect to the desired page after successful submission
		//			}
		//			else
		//			{
		//				ModelState.AddModelError(string.Empty, "Failed to update the timesheet.");
		//			}
		//		}

		//		// If ModelState is not valid, return the view with the model
		//		return View(timeSheet);
		//	}
		//	catch (Exception ex)
		//	{
		//		// Log the exception if needed
		//		ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
		//		return View(timeSheet);
		//	}
		//}




	}
}

