﻿using System.ComponentModel.DataAnnotations;

namespace SaggiTS.Models
{
    public class Approval
    {
        [Key]
        public int ApprovalId { get; set; }
        public int? TimeSheetId { get; set; }
        public int? ApproverUserId { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string ApprovalStatus { get; set; } = null!;

        public virtual User? ApproverUser { get; set; }
        public virtual TimeSheet? TimeSheet { get; set; }
    }
}
