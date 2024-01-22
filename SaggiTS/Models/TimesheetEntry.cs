using System.ComponentModel.DataAnnotations;

namespace SaggiTS.Models
{
    public class TimesheetEntry
    {
        //For Everyday
        [Key]

        public int TimeSheetEntryId { get; set; }
        public int? TimeSheetId { get; set; }
        public virtual TimeSheet? TimeSheet { get; set; }
        public int? ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public DateTime EntryDate { get; set; }
        public int HoursWorked { get; set; }
    }
}
