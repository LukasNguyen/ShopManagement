using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaleShop.Web.Models
{
    public class OrderViewModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { get; set; }

        [Required]
        [MaxLength(256)]
        public string PaymentMethod { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public string PaymentStatus { get; set; }

        public bool Status { get; set; }

        [MaxLength(128)]
        public string CustomerID { get; set; }

        public IEnumerable<OrderDetailViewModel> OrderDetails { get; set; }
    }
}