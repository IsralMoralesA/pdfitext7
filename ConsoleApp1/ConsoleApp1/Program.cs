// See https://aka.ms/new-console-template for more information

using HandlebarsDotNet;
using iText.Commons.Utils;
using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Font;
using Org.BouncyCastle.Asn1.Ocsp;

Console.WriteLine("Hello, World!");


var basePath = "C:\\Users\\asmet.morales\\Documents\\PDF\\ConsoleApp1\\ConsoleApp1\\";

string templateHtml = File.ReadAllText(System.IO.Path.Combine(basePath, "templates/Orden.html"));

var header = File.ReadAllText(System.IO.Path.Combine(basePath, "templates/header.html"));
var supplies = File.ReadAllText(System.IO.Path.Combine(basePath, "templates/supplies.html"));
var signatures = File.ReadAllText(System.IO.Path.Combine(basePath, "templates/signatures.html"));
var tableinfo = File.ReadAllText(System.IO.Path.Combine(basePath, "templates/tableinfo.html"));
var observation = File.ReadAllText(System.IO.Path.Combine(basePath, "templates/observation.html"));

Handlebars.RegisterTemplate("header", header);
Handlebars.RegisterTemplate("supplies", supplies);
Handlebars.RegisterTemplate("signatures", signatures);
Handlebars.RegisterTemplate("observation", observation);
Handlebars.RegisterTemplate("tableinfo", tableinfo);



var template = Handlebars.Compile(templateHtml);



var data = new
{
    RequestNum = "1234",
    Fecha = System.DateTime.Now,
    SignatureApplicant = "La firma",
    SignatureApplicant_Name = "QUIEN FIRMA",
    WarehouseSignature = "OTRA FIRMA",
    observations = "Hola"
};

var invoiceHtml = template(data);


/*string file = System.IO.Path.Combine(basePath, $@"Documents\Temp_Invoice_.pdf");
HtmlConverter.ConvertToPdf(invoiceHtml, new FileStream(file, FileMode.Create));
*/


ConverterProperties properties = new ConverterProperties();
properties.SetFontProvider(new DefaultFontProvider(true, true, true));

/*ConverterProperties converterProperties = new ConverterProperties();
FontProvider fontProvider = new DefaultFontProvider(true, true, true);
converterProperties.SetFontProvider(fontProvider);*/

string file = System.IO.Path.Combine(basePath, $@"Documents\Temp_Invoice_.pdf");
PdfWriter writer = new PdfWriter(file);
PdfDocument pdf = new PdfDocument(writer);
pdf.SetDefaultPageSize(PageSize.LETTER);
//pdf.SetDefaultPageSize(new PageSize(PageSize.LETTER.Rotate()));
var document = HtmlConverter.ConvertToDocument(invoiceHtml, pdf, properties);
document.Close();