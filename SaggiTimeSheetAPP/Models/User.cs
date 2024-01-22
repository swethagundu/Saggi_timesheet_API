﻿namespace SaggiTimeSheetAPP.Models
{
    public class User
    {
        public User()
        {
            //Approvals = new HashSet<Approval>();
            Projects = new HashSet<Project>();
            TimeSheets = new HashSet<TimeSheet>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        //public virtual ICollection<Approval> Approvals { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }

    }
}
