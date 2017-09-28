using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleShop.Model.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
