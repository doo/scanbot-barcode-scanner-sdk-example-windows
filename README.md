# Scanbot Barcode Scanner SDK Example App for Windows

This example app shows how to integrate the [Scanbot Barcode Scanner SDK for Windows](https://scanbot.io/developer/uwp-barcode-scanner/).

## What is the Scanbot Barcode Scanner SDK?

The Scanbot Windows Barcode Scanner SDK makes it possible to integrate barcode scanning on your UWP app. It features real-time barcode detection as well as detection from still images.

## Trial License

The Scanbot Windows Barcode Scanner SDK will not run without a license. A trial license key is required for evaluation or testing.

To test the Scanbot SDK without crashing, you can get a free “no-strings-attached” trial license. Please submit the [Trial License Form](https://scanbot.io/trial/) on our website. You will need to provide the following package name (aka. application ID) of this
example app: `io.scanbot.example.sdk.barcode.windows`

## Free Developer Support

We provide free "no-strings-attached" developer support for the implementation & testing of the Scanbot SDK.
If you encounter technical issues with integrating the Scanbot SDK or need advice on choosing the appropriate
framework or features, please visit our [Support Page](https://docs.scanbot.io/support/).

## Supported Barcode Types

- [1D Barcodes](https://scanbot.io/products/barcode-software/1d-barcode-scanner/): [Codabar](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/codabar), [Code 39](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/code-39), [Code 93](https://scanbot.io/products/barcode-software/1d-barcode-scanner/code-93/), [Code 128](https://scanbot.io/products/barcode-software/1d-barcode-scanner/code-128/), [IATA 2 of 5](https://scanbot.io/products/barcode-software/1d-barcode-scanner/standard-2-of-5/), [Industrial 2 of 5](https://scanbot.io/products/barcode-software/1d-barcode-scanner/industrial-2-of-5/), [ITF](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/itf), [EAN-8](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/ean-code), [EAN-13](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/ean-code), [MSI Plessey](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/msi-plessey), RSS 14, [RSS Expanded (Databar)](https://scanbot.io/products/barcode-software/1d-barcode-scanner/gs1-databar/), [UPC-A](https://scanbot.io/products/barcode-software/1d-barcode-scanner/upc/), [UPC-E](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/upc-code).
- [2D Barcodes](https://scanbot.io/products/barcode-software/2d-barcode-scanner/): [Aztec](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/aztec), [Data Matrix](https://scanbot.io/en/sdk/scanner-sdk/barcode-scanner-sdk/datamatrix), [PDF417](https://scanbot.io/products/barcode-software/2d-barcode-scanner/pdf417/), [QR Code](https://scanbot.io/products/barcode-software/2d-barcode-scanner/qr-code/).

💡 Also check out our blog post [Types of barcodes](https://scanbot.io/blog/types-of-barcodes/).


## Supported Data Parsers:

- [AAMVA](https://scanbot.io/blog/drivers-license-barcode-parser/): Parse the AAMVA data format from PDF-417 barcodes on US driver's licenses.
- Boarding pass data from PDF417 barcodes.
- Parser for German Medical Certificates (aka. Disability Certificate or AU-Bescheinigung) coded in a PDF-417 barcode.
- [GS1](https://scanbot.io/products/barcode-software/1d-barcode-scanner/gs1-databar/) encoded data from barcodes.
- Data from PDF-417 barcodes on ID Cards.
- Parse and extract data from XML of Data Matrix barcodes on Medical Plans (German Medikationsplan).
- Data parser of QR-Code values printed on SEPA pay forms.
- vCard data from a QR-Code (e.g. on business cards).
- [Swiss QR](https://scanbot.io/products/barcode-software/2d-barcode-scanner/swiss-qr/) data from a QR-Code for easy, automatic and efficient payments.

For more details please refer to the SDK documentation.


## Documentation

For the developer guide and full API reference of the Scanbot Windows Barcode Scanner SDK please check out the
[documentation](https://docs.scanbot.io/barcode-scanner-sdk/windows/introduction/).

## How does it work?

The Windows SDK is a native SDK built for UWP via 
[C++/WinRT](https://docs.microsoft.com/en-us/windows/uwp/cpp-and-winrt-apis/intro-to-using-cpp-with-winrt) 
and camera access via `Windows.Media` API.
It is available a NuGet package [`Scanbot.BarcodeSDK.Windows`](https://www.nuget.org/packages/Scanbot.BarcodeSDK.Windows/).

## How to run the example apps?

Open it in Visual Studio 2019/2022, make sure NuGet packages are restored, and run the example – simple as that!

