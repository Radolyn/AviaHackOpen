#region

using System;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using DinkToPdf;
using DocumentFormat.OpenXml.Packaging;
using OpenXmlPowerTools;
using ColorMode = DinkToPdf.ColorMode;

#endregion

namespace DocRecycle.Signer
{
    public static class DocxToPdf
    {
        private static readonly SynchronizedConverter Converter = new SynchronizedConverter(new PdfTools());

        public static string ConvertToPdf(string filename)
        {
            var fileInfo = new FileInfo(filename);
            var fullFilePath = fileInfo.FullName;
            var htmlText = string.Empty;
            try
            {
                htmlText = ParseDOCX(fileInfo);
            }
            catch (OpenXmlPackageException e)
            {
                if (e.ToString().Contains("Invalid Hyperlink"))
                {
                    using (var fs = new FileStream(fullFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        UriFixer.FixInvalidUri(fs, FixUri);
                    }

                    htmlText = ParseDOCX(fileInfo);
                }
            }

            var doc2 = new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        HtmlContent = htmlText,
                        WebSettings = {DefaultEncoding = "utf-8"}
                    }
                }
            };

            var resPdf = filename + ".pdf";
            var converted = Converter.Convert(doc2);
            File.WriteAllBytes(resPdf, converted);

            return resPdf;
        }

        public static Uri FixUri(string brokenUri)
        {
            var newURI = string.Empty;
            if (brokenUri.Contains("mailto:"))
            {
                var mailToCount = "mailto:".Length;
                brokenUri = brokenUri.Remove(0, mailToCount);
                newURI = brokenUri;
            }
            else
            {
                newURI = " ";
            }

            return new Uri(newURI);
        }

        public static string ParseDOCX(FileInfo fileInfo)
        {
            try
            {
                var byteArray = File.ReadAllBytes(fileInfo.FullName);
                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Write(byteArray, 0, byteArray.Length);
                    using (var wDoc =
                        WordprocessingDocument.Open(memoryStream, true))
                    {
                        var imageCounter = 0;
                        var pageTitle = fileInfo.FullName;
                        var part = wDoc.CoreFilePropertiesPart;
                        if (part != null)
                            pageTitle = (string) part.GetXDocument()
                                .Descendants(DC.title)
                                .FirstOrDefault() ?? fileInfo.FullName;

                        var settings = new WmlToHtmlConverterSettings
                        {
                            AdditionalCss = "body { margin: 1cm auto; max-width: 20cm; padding: 0; }",
                            PageTitle = pageTitle,
                            FabricateCssClasses = true,
                            CssClassPrefix = "pt-",
                            RestrictToSupportedLanguages = false,
                            RestrictToSupportedNumberingFormats = false,
                            ImageHandler = imageInfo =>
                            {
                                ++imageCounter;
                                var extension = imageInfo.ContentType.Split('/')[1].ToLower();
                                ImageFormat imageFormat = null;
                                if (extension == "png")
                                {
                                    imageFormat = ImageFormat.Png;
                                }
                                else if (extension == "gif")
                                {
                                    imageFormat = ImageFormat.Gif;
                                }
                                else if (extension == "bmp")
                                {
                                    imageFormat = ImageFormat.Bmp;
                                }
                                else if (extension == "jpeg")
                                {
                                    imageFormat = ImageFormat.Jpeg;
                                }
                                else if (extension == "tiff")
                                {
                                    extension = "gif";
                                    imageFormat = ImageFormat.Gif;
                                }
                                else if (extension == "x-wmf")
                                {
                                    extension = "wmf";
                                    imageFormat = ImageFormat.Wmf;
                                }

                                if (imageFormat == null) return null;

                                string base64 = null;
                                try
                                {
                                    using (var ms = new MemoryStream())
                                    {
                                        imageInfo.Bitmap.Save(ms, imageFormat);
                                        var ba = ms.ToArray();
                                        base64 = Convert.ToBase64String(ba);
                                    }
                                }
                                catch (ExternalException)
                                {
                                    return null;
                                }

                                var format = imageInfo.Bitmap.RawFormat;
                                var codec = ImageCodecInfo.GetImageDecoders()
                                    .First(c => c.FormatID == format.Guid);
                                var mimeType = codec.MimeType;

                                var imageSource =
                                    string.Format("data:{0};base64,{1}", mimeType, base64);

                                var img = new XElement(Xhtml.img,
                                    new XAttribute(NoNamespace.src, imageSource),
                                    imageInfo.ImgStyleAttribute,
                                    imageInfo.AltText != null
                                        ? new XAttribute(NoNamespace.alt, imageInfo.AltText)
                                        : null);
                                return img;
                            }
                        };

                        var htmlElement = WmlToHtmlConverter.ConvertToHtml(wDoc, settings);
                        var html = new XDocument(new XDocumentType("html", null, null, null),
                            htmlElement);
                        var htmlString = html.ToString(SaveOptions.DisableFormatting);
                        return htmlString;
                    }
                }
            }
            catch
            {
                return "The file is either open, please close it or contains corrupt data";
            }
        }
    }
}