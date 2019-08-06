using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace  GetAddDelete
{
    class Program
    {
        private const string serverUri = "http://localhost:82/0/ServiceModel/EntityDataService.svc/";
        private const string authServiceUtri = "http://localhost:82/ServiceModel/AuthService.svc/Login";
        private static readonly XNamespace ds = "http://schemas.microsoft.com/ado/2007/08/dataservices";
        private static readonly XNamespace dsmd = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";
        private static readonly XNamespace atom = "http://www.w3.org/2005/Atom";
        static void Main(string[] args)
        {
            GetOdataObjectByFilter();
            Console.ReadKey();
            AddBpmEntityByOdataHttpExample();
            Console.ReadKey();
            DeleteBpmEntityByOdataHttpExample();
            Console.ReadKey();
        }


        public static void GetOdataObjectByFilter()
        {
            string userName = "Supervisor";
            string personName = "Карменова Аида Геннадиевна"; // ФИО контакта 
            string requestUri = serverUri + "ContactCommunicationCollection?$filter=Contact/Name eq '" + personName + // фильтр по ФИО 
                                            "'and CreatedBy/Name eq '" + userName + "'";
            var request = HttpWebRequest.Create(requestUri) as HttpWebRequest;
            request.Method = "GET";
            request.Credentials = new NetworkCredential(userName, "Supervisor");
            using (var response = request.GetResponse())
            {
                XDocument xmlDoc = XDocument.Load(response.GetResponseStream());
                var contacts = from entry in xmlDoc.Descendants(atom + "entry")
                               select new
                               {
                                   Id = new Guid(entry.Element(atom + "content")
                                                     .Element(dsmd + "properties")
                                                     .Element(ds + "Id").Value),
                                   Number = entry.Element(atom + "content")
                                               .Element(dsmd + "properties")
                                               .Element(ds + "Number").Value
                               };
                foreach (var contact in contacts) //contact.Number
                {
                    Console.WriteLine("Средства связи пользователя: " + contact.Number);//поэкспер.
                }
            }
        }
        public static void AddBpmEntityByOdataHttpExample()
        {
            var content = new XElement(dsmd + "properties",
                          new XElement(ds + "ContactId", "8EECBE2D-44F8-42FD-BD71-6353FB588780"), // ID контакта 
                          new XElement(ds + "CommunicationTypeId", "EE1C85C3-CFCB-DF11-9B2A-001D60E938C6"), //ID типа средства связи типа email
                          new XElement(ds + "Number", "a@mail.ru")); // вводимые данные название колонки
            var entry = new XElement(atom + "entry", 
                        new XElement(atom + "content",
                        new XAttribute("type", "application/xml"), content));//менять не над
            Console.WriteLine(entry.ToString());
            var request = (HttpWebRequest)HttpWebRequest.Create(serverUri + "ContactCommunicationCollection/");
            request.Credentials = new NetworkCredential("Supervisor", "Supervisor");
            request.Method = "POST";
            request.Accept = "application/atom+xml";
            request.ContentType = "application/atom+xml;type=entry";
            using (var writer = XmlWriter.Create(request.GetRequestStream()))
            {
                entry.WriteTo(writer);
            }
            using (WebResponse response = request.GetResponse())
            {
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.Created)
                {
                    Console.WriteLine("Средство связи успешно создано!");
                }
            }
        }

        public static void DeleteBpmEntityByOdataHttpExample()
        {
            string contactId = "0613F80E-930B-4921-A8EF-37682314A4FC"; // ID удаляемого средства связи
            var request = (HttpWebRequest)HttpWebRequest.Create(serverUri
                    + "ContactCommunicationCollection(guid'" + contactId + "')");
            request.Credentials = new NetworkCredential("Supervisor", "Supervisor");
            request.Method = "DELETE";
            using (WebResponse response = request.GetResponse())
            {
                Console.WriteLine("Средство связи успешно удалено!");
            }
        }
    }
}