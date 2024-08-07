using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.LIB.Models
{
    public class ChangeEmailModel
    {
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
    }

}
