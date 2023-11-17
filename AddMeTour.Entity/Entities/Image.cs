using AddMeTour.Core.Entities;
using AddMeTour.Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddMeTour.Entity.Entities
{
    public class Image : EntityBase
    {
        public Image() { }
        public Image(string fileName, string fileType)
        {
            FileName = fileName;
            FileType = fileType;
        }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public ImageType ImageType { get; set; }

        public ICollection<Feature> Features { get; set; }
    }
}
