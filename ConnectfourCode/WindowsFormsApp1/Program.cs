using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        private static WebBrowser wb1 = new WebBrowser();

        [STAThread]
        static void Main(string[] args)
        {
            runBrowserThread(new Uri("https://connect4.gamesolver.org"));
        }

        private static void runBrowserThread(Uri url)
        {
            var th = new Thread(() => {
                var br = new WebBrowser();
                br.DocumentCompleted += Br_DocumentCompleted; ;
                br.Navigate(url);
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private static void Br_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Retrieve string content of document
            var document = ((WebBrowser)sender).Document;
            var documentAsIHtmlDocument3 = (mshtml.IHTMLDocument3)document.DomDocument;
            var content = documentAsIHtmlDocument3.documentElement.innerHTML;

            //Parse content with html agility pack or whatever

            //Click on button
            wb1.Document.GetElementById("player_2").InvokeMember("click");

            Application.ExitThread();
        }
    }
}
