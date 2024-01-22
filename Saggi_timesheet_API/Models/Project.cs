using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Saggi_timesheet_API.Models
{
    public class Project
    {
        public Project()
        {
            TimeSheetEntries = new HashSet<TimesheetEntry>();
        }
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = null!;
        public string ProjectCode { get; set; } = null!;
        public int? ProjectManagerId { get; set; }

        public virtual User? ProjectManager { get; set; }
        public virtual ICollection<TimesheetEntry> TimeSheetEntries { get; set; }

    }
}
