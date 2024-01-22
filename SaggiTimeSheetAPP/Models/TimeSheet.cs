namespace SaggiTimeSheetAPP.Models
{
    public class TimeSheet
    {
        public TimeSheet()
        {
            //Approvals = new HashSet<Approval>();
            TimeSheetEntries = new HashSet<TimesheetEntry>();
        }

        public int TimeSheetId { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public string WeekStarting { get; set; } 
        public int TotalHours { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string ApprovalStatus { get; set; } = null!;

        ////public virtual ICollection<Approval> Approvals { get; set; }
        public virtual ICollection<TimesheetEntry> TimeSheetEntries { get; set; }
    }
}
