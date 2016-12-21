using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Infrastructure
{
    public class JsonRecettesData
    {
        public string id { get; set; }
        public string name { get; set; }
        public int creatorId { get; set; }
        public bool isAvailable { get; set; }
        public string picture { get; set; }
        public int calories { get; set; }
        public ICollection<string> ingredients { get; set; }
        public string preparation { get; set; }
        public ICollection<JsonCommentsData> comments { get; set; }
    }

    public class JsonCommentsData
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string recetteId { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int mark { get; set; }
    }
}