using ngCooking_Julien.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ngCooking_Julien.Models
{
    static public class CommunityModel
    {
        public static int getDBSize(ngCookingDB db)
        {
            return db.Communaute.Count();
        }

        public static int fromIndex(int from, int display, int dbSize)
        {
            if (from < 0)
            {
                return 0;
            }
            else if (from + display < dbSize)
            {
                return from;
            }
            else
            {
                return (dbSize - display);
            }
        }

        public static void FillIndexCom(CommunityIndexViewModel CommunityView, string selectorValue)
        {
            var CommunityList = new List<Communaute>();
            var RecettesList = new List<Recettes>();

            switch (selectorValue)
            {
                case "az": // Alpha
                    CommunityList = CommunityView.db.Communaute.OrderBy(r => r.surname).Skip(CommunityView.fromIndex).Take(CommunityView.display).ToList();
                    break;

                case "za": // Inverse Alpha
                    CommunityList = CommunityView.db.Communaute.OrderByDescending(r => r.surname).Skip(CommunityView.fromIndex).Take(CommunityView.display).ToList();
                    break;

                case "prod": // Plus de recettes d'abord
                    CommunityList = CommunityView.db.Communaute.OrderByDescending(r => r.recettes.Count).Skip(CommunityView.fromIndex).Take(CommunityView.display).ToList();
                    break;

                case "prod_desc": // Moins de recettes d'abord
                    CommunityList = CommunityView.db.Communaute.OrderBy(r => r.recettes.Count).Skip(CommunityView.fromIndex).Take(CommunityView.display).ToList();
                    break;

                case "exp": // Meilleurs notes d'abord
                    CommunityList = CommunityView.db.Communaute.OrderByDescending(r => r.level).Skip(CommunityView.fromIndex).Take(CommunityView.display).ToList();
                    break;

                case "nov": // Moins bonnes notes d'abord
                    CommunityList = CommunityView.db.Communaute.OrderBy(r => r.level).Skip(CommunityView.fromIndex).Take(CommunityView.display).ToList();
                    break;

                default:
                    CommunityList = CommunityView.db.Communaute.OrderBy(r => r.surname).Skip(CommunityView.fromIndex).Take(CommunityView.display).ToList();
                    break;
            }

            foreach (var cList in CommunityList)
            {
                var tmp = new CommunityData();

                tmp.id = cList.id;
                tmp.firstname = cList.firstname;
                tmp.surname = cList.surname;
                tmp.level = cList.level;
                tmp.picture = cList.picture;

                CommunityView.cData.Add(tmp);
            }
        }

        public static void FillDetailsCom(CommunityDetailsViewModel CommunityView, int? id)
        {
            CommunityView.nbRecettes = 0;
            if (id == null)
            {
                return;
            }

            // Community part of the view
            Communaute com = new Communaute();
            com = CommunityView.db.Communaute.Find(id);

            CommunityView.cData.id = com.id;
            CommunityView.cData.firstname = com.firstname;
            CommunityView.cData.surname = com.surname;
            CommunityView.cData.level = com.level;
            CommunityView.cData.picture = com.picture;
            CommunityView.cData.bio = com.bio;
            CommunityView.cData.city = com.city;
            CommunityView.cData.birth = com.birth;
            CommunityView.age = DateTime.Today.Year - com.birth;

            // Receipe part of the view, if any
            List<Recettes> ReceipeList = CommunityView.db.Recettes
                                            .Where(r => r.creatorId == com.id)
                                            .OrderByDescending(r => r.comments.Average(m => m.mark))
                                            .ToList();

            foreach (var rdata in ReceipeList)
            {
                var tmp = new RecettesData();

                tmp.id = rdata.id;
                tmp.name = rdata.name;
                tmp.picture = rdata.picture;
                if (rdata.comments.Count != 0)
                    tmp.rating = Convert.ToInt32(rdata.comments.Average(c => c.mark));
                else
                    tmp.rating = 2;

                CommunityView.nbRecettes += 1;

                CommunityView.rData.Add(tmp);
            }
        }
    }
}