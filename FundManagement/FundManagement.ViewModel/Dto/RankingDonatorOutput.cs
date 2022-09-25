using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundManagement.ViewModel.Dto
{
    public class RankingDonatorOutput
    {
        public int MemberID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal TotalMoney { get; set; }
        public long Ranking { get; set; }
    }
}
