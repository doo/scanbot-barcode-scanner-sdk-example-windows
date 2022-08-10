using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barcode.SDK.Example.Model
{
    internal class Feature
    {
        public static List<Feature> List = new List<Feature>
        {
            new Feature(0, "barcode-scanner", "Barcode Scanner Page", "Ready-made component: Plug & play barcode scanning"),
            new Feature(1, "import", "Import Barcode Image", "Import an existing image containing one or more barcodes"),
        };

        public int Id { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Feature(int id, string image, string title, string description)
        {
            Id = id;
            Image = "Assets/feature-icons/" + image + ".png";
            Title = title;
            Description = description;
        }
    }
}
