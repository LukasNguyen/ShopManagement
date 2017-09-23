using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleShop.Model.Models
{
    [Table("ContactDetails")]
    public class ContactDetail
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(250)]
        public string Email { get; set; }
        [MaxLength(250)]
        public string Website { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        public string Other { get; set; }
        //[DefaultValue(0)]
        public double? Lat { get; set; }
        //[DefaultValue(0)]
        public double? Lng { get; set; }
        public bool Status { get; set; }
    }
}
    