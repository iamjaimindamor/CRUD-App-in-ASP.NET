using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentRegisterationForm.Models
{
    public class FetchingModel
    {
       
        [Required]
        public int ID { get; set; }

        public List<SelectListItem> Studs { get; set; }
    }
}
