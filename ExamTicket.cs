using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jk_Buh_Prof
{
    class ExamTicket
    {
        public int Section { get; set; }
        public int Number { get; set; }
        
        public string Question { get; set; }
        public int? TrueAnswer { get; set; }
        public List<string> Answer { get; set; } = new List<string>();

        //TODO: Picture

        public ExamTicket(int section, int number)
        {
            Section = section;
            Number = number;
            Answer.Add("Ответ 1");
        }
    }
}
