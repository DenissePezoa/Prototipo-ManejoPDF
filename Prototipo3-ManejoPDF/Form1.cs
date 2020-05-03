using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
namespace Prototipo3_ManejoPDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Document document = new Document(iTextSharp.text.PageSize.A4);
           // document.PageSize.BackgroundColor = new iTextSharp.text.BaseColor(255,255,255);
              
            document.PageSize.Rotate();
            //creamos un instancia del objeto escritor de documento
            PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream
            ("Code.pdf", System.IO.FileMode.Create));

            //definimos la manera de inicialización de abierto del documento.
            //esto, hará que veamos al inicio, todas la páginas del documento
            //en la parte izquierda
            writer.ViewerPreferences = PdfWriter.PageModeUseThumbs;
            

            //abrimos el documento para agregarle contenido
            document.Open();

            //creamos la fuente
            iTextSharp.text.Font myfont = new iTextSharp.text.Font(
            FontFactory.GetFont(FontFactory.COURIER, 10, iTextSharp.text.Font.ITALIC));

            Paragraph myParagraph = new Paragraph("Primera prueba ziiii \n", myfont);
            myParagraph.Add(new Paragraph(textBox1.Text, myfont));
            //agregar todo el paquete de texto
            document.Add(myParagraph);
           
            //esto es importante, pues si no cerramos el document entonces no se creara el pdf.
            document.Close();

            //esto es para abrir el documento y verlo inmediatamente después de su creación
            //System.Diagnostics.Process.Start("AcroRd32.exe", "Code.pdf");

            //Disable el boton y habilitar el otro
            MessageBox.Show("Pdf creado con exito ");
            button1.Enabled = false;
            button2.Enabled = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string oldFile = textBox6.Text;
            string newFile = "temp777.pdf";
            
            PdfReader reader = new PdfReader(oldFile);
            

            PdfStamper stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.Append));
            //Image img = Image.getInstance(IMG);
            float x = Convert.ToSingle(textBox2.Text);
            float y = Convert.ToSingle(textBox3.Text);
            float w = Convert.ToSingle(textBox5.Text);
            float h = Convert.ToSingle(textBox4.Text);

            PdfContentByte contentunder = stamper.GetUnderContent(1);
            contentunder.SetColorStroke(BaseColor.RED);
            contentunder.Rectangle(x, y, w, h);
            //axAcroPDF1.RectangleToScreen(new System.Drawing.Rectangle(10, 10, 20, 100));
            contentunder.Stroke();


            //img.setAbsolutePosition(x, y);
            //stamper.getOverContent(1).addImage(img);
            /*iTextSharp.text.Rectangle linkLocation = new iTextSharp.text.Rectangle(x, y, x + w, y + h);
            PdfDestination destination = new PdfDestination(PdfDestination.FIT);
            PdfAnnotation link = PdfAnnotation.CreateLink(stamper.Writer,
                    linkLocation, PdfAnnotation.HIGHLIGHT_INVERT,
                    1 , destination);
            PdfBorderArray border =(new PdfBorderArray(0, 0, 0));
            link.Border = border;
            stamper.AddAnnotation(link, 1);*/

            
            
            stamper.Close();
            reader.Close();
            File.Replace(@newFile, oldFile, @"backup.pdf.bac");
            MessageBox.Show("Pdf modificado con exito, se ha guardado un backup de la versión anterior ");

            axAcroPDF1.src = "C:\\Users\\Denisse\\Desktop\\prototipos\\Prototipo3-ManejoPDF\\Prototipo3-ManejoPDF\\bin\\Debug\\"+oldFile;
            /*
            // open the reader
            PdfReader reader = new PdfReader(oldFile);
            iTextSharp.text.Rectangle size = reader.GetPageSizeWithRotation(1);
            Document document = new Document(size);

            // open the writer
            
            
            FileStream fs = new FileStream(newFile, FileMode.Create, FileAccess.Write);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            //PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream
            //("Code.pdf", System.IO.FileMode.Create));
            document.Open();

            // the pdf content
            PdfContentByte cb = writer.DirectContent;

            // select the font properties
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.DARK_GRAY);
            cb.SetFontAndSize(bf, 8);

            // write the text in the pdf content
            cb.BeginText();
            string text = "Some random blablablabla...";
            // put the alignment and coordinates here
            cb.ShowTextAligned(1, text, 520, 640, 0);
            cb.EndText();
            cb.BeginText();
            text = "Other random blabla...";
            // put the alignment and coordinates here
            cb.ShowTextAligned(2, text, 100, 200, 0);
            cb.EndText();

            // create the new page and add it to the pdf
            PdfImportedPage page = writer.GetImportedPage(reader, 1);
            cb.AddTemplate(page, 0, 0);

            // close the streams and voilá the file should be changed :)
            document.Close();
            fs.Close();
            writer.Close();
            reader.Close();*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*string src = "ejemplo.pdf";
            string dest = "Ejemplo4.pdf";

            File.Copy(src,dest);
           
            
                MessageBox.Show("Pdf copiado con exito");*/
            String archivo = textBox6.Text;

            PdfReader inputDocument = new PdfReader(archivo);

            StringBuilder text = new StringBuilder();

            for (int page = 1; page <= inputDocument.NumberOfPages; page++)

            {



                ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();

                string currentText = PdfTextExtractor.GetTextFromPage(inputDocument, page, strategy);



                currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                text.Append(currentText);

            }

            inputDocument.Close();

            MessageBox.Show(text.ToString());

        }
    }
}
