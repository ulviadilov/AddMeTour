using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.ViewModels.Partners
{
    public class PartnerViewModel
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
    }
}
