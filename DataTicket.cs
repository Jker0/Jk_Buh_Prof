using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jk_Buh_Prof
{
    static class DataTicket
    {
        public static List<ExamTicket> examTickets { get; set; } = new List<ExamTicket>();


        public static void InitProgram()
        {
            string path = @"Data.xml";
            if (!File.Exists(path))
            {
                XDocument xdoc = new XDocument();
                xdoc.Save(path);
            }
        }

        public static void RemoveQuestion(int sector, int number)
        {
            examTickets.RemoveAll(t => t.Section == sector && t.Number == number);
        }

        public static void RemoveSector(int indexSector)
        {
            DataTicket.examTickets.RemoveAll(t => t.Section == indexSector);
        }
        
    }
}
