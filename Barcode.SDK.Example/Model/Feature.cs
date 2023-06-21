using System.Collections.Generic;

namespace Barcode.SDK.Example.Model
{
    internal struct Feature
    {
        public enum FeatureType
        {
            ReadyToUseUI,
            CustomComponent,
            ImportImage
        }

        public static List<Feature> ExampleFeatures = new List<Feature>
        {
            new Feature(FeatureType.ReadyToUseUI, "barcode-scanner", "Barcode Scanner RTU-UI", "Ready-made Scanbot Camera page: Capture and retrieve a single Barcode Result"),
            new Feature(FeatureType.CustomComponent, "barcode-scanner", "Barcode Scanner Layout", "Custom BarcodeScannerComponent implementation with endless recognition"),
            new Feature(FeatureType.ImportImage, "import", "Import Barcode Image", "Import an existing image containing one or more barcodes"),
        };

        public FeatureType Type { get; private set; }

        public string Image { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public Feature(FeatureType type, string image, string title, string description)
        {
            Type = type;
            Image = "Assets/feature-icons/" + image + ".png";
            Title = title;
            Description = description;
        }
    }
}
