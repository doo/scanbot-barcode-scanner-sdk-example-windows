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
            new Feature(0, "barcode-scanner", "Barcode Scanner RTU-UI", "Ready-made Scanbot Camera page: Capture and retrieve a single Barcode Result"),
            new Feature(1, "barcode-scanner", "Barcode Scanner Layout", "Custom BarcodeScannerComponent implementation with endless recognition"),
            new Feature(2, "import", "Import Barcode Image", "Import an existing image containing one or more barcodes"),
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
