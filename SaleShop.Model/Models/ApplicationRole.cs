using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SaleShop.Model.Models
{
    public class ApplicationRole :IdentityRole
    {
        public ApplicationRole() : base()
        {
            
        }
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
