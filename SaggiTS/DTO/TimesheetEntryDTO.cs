namespace SaggiTS.DTO
{
    public class TimesheetEntryDTO
    {
        public int TimeSheetEntryId { get; set; }
        public int? TimeSheetId { get; set; }
        public int? ProjectId { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now.Date;
        public int HoursWorked { get; set; }
    }
}
