using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaleShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int ID { get; set; }
        [MaxLength(250,ErrorMessage = "Tên không được vượt quá 250 ký tự")]
        [Required(ErrorMessage = "Tên là trường bắt buộc nhập")]
        public string Name { get; set; }
        [MaxLength(250, ErrorMessage = "Email không được vượt quá 250 ký tự")]
        public string Email { get; set; }
        [MaxLength(500, ErrorMessage = "Tin nhắn không được vượt quá 500 ký tự")]
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Trạng thái là trường bắt buộc chọn")]
        public bool Status { get; set; }
        public ContactDetailViewModel ContactDetail { get; set; }
    }
}