using System.ComponentModel.DataAnnotations;

namespace SaggiTimeSheetAPP.Models.DTO
{
    public class TimesheetDTO
    {
     
        public int TimeSheetId { get; set; }
        public int? UserId { get; set; }
        public string WeekStarting { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string ApprovalStatus { get; set; } = null!;
        public int TotalHours { get; set; }
        [Required]
        public List<TimesheetEntryDTO> WST { get; set; } = new List<TimesheetEntryDTO>();
    }
}
