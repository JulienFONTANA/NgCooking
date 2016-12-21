using ngCooking_Julien.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    public class CommunityDetailsViewModel : ViewModel
    {
        public int age { get; set; }
        public int nbRecettes { get; set; }
        public CommunityData cData { get; set; }
        public List<RecettesData> rData { get; set; }

        public CommunityDetailsViewModel()
        {
            cData = new CommunityData();
            rData = new List<RecettesData>();
        }
    }

    public class CommunityIndexViewModel : ViewModel
    {
        public List<CommunityData> cData { get; set; }
        public int fromIndex { get; set; }
        public int display { get; set; }
        public int dbSize { get; set; }

        public CommunityIndexViewModel()
        {
            display = 8;
            fromIndex = 0;
            cData = new List<CommunityData>();
            dbSize = CommunityModel.getDBSize(db);
        }
    }
}