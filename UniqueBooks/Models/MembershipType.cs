using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UniqueBooks.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        public string MembershipName { get; set; }
        public short SignupFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        //reduction of Magic No. in Costume validation class Min18YearsIfAMember
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}