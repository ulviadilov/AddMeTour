using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.HomeReviews
{
    public class HomeReviewAddViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public string Title { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string UserCountry { get; set; }
        [NotMapped]
        public IFormFile AvatarImageFile { get; set; }
    }
}
