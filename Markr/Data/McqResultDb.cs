using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markr.DataHandling.Data {
    [Table("McqTestResults")]
    public class McqResultDb {
        [Key]
        [Required]
        public int ResultId { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(10)]
        public string StudentNumber { get; set; }

        public int TestId { get; set; }

        //public DateTime ScannedOn { get; set; }

        public int AvailableMarks { get; set; }

        public int ObtainedMarks { get; set; }
    }
}
