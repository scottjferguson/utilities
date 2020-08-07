using Core.Application;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Common;

namespace Utilities.FileProcessors.PDF
{
    [Processor(Name = "SplitPdf")]
    public class SplitPdfFileProcessor : UtilitiesBase, IProcessor
    {
        private const string PdfFilePath = @"C:\Users\scott\Documents\PDFSplit\TaxAffidavit.pdf";
        private const string OutputPath  = @"C:\Users\scott\Documents\PDFSplit\result";
        private const int PagesPerSplit  = 4;

        public async Task ProcessAsync(CancellationToken cancellationToken)
        {
            int pageNameSuffix = 0;

            var file = new FileInfo(PdfFilePath);

            string pdfFileName = file.Name.Substring(0, file.Name.LastIndexOf("."));

            CreateOutputDirectory();

            using var reader = new PdfReader(PdfFilePath);

            for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber += PagesPerSplit)
            {
                pageNameSuffix++;

                string newPdfFileName = string.Format(pdfFileName + "-{0}", pageNameSuffix);

                SplitAndSaveInterval(reader, OutputPath, pageNumber, PagesPerSplit, newPdfFileName);

                Console.WriteLine($"Created {newPdfFileName}");
            }
        }

        private void SplitAndSaveInterval(PdfReader reader, string outputPath, int startPage, int interval, string pdfFileName)
        {
            var document = new Document();

            var copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));

            document.Open();

            for (int pageNumber = startPage; pageNumber < (startPage + interval); pageNumber++)
            {
                if (reader.NumberOfPages >= pageNumber)
                {
                    copy.AddPage(copy.GetImportedPage(reader, pageNumber));
                }
                else
                {
                    break;
                }
            }

            document.Close();
        }

        private void CreateOutputDirectory()
        {
            if (!Directory.Exists(OutputPath))
            {
                Directory.CreateDirectory(OutputPath);
            }
        }
    }
}
