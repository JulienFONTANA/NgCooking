using System.Web;
using System.Web.Optimization;

namespace ngCooking_Julien
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/_glyphicons.css",
                      "~/Content/_grid.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/reset.css",
                      "~/Content/style.css"));

            // Bundle maisons
            bundles.Add(new ScriptBundle("~/bundles/NavigationScripts").Include(
                        "~/Scripts/Scripts/NavigationScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/CommunityScripts").Include(
                        "~/Scripts/Scripts/CommunityScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/IngredientsScripts").Include(
                        "~/Scripts/Scripts/IngredientsScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/ReceipeScripts").Include(
                        "~/Scripts/Scripts/ReceipeScripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/ReceipeScriptsDetails").Include(
                        "~/Scripts/Scripts/ReceipeScriptsDetails.js"));            
        }
    }
}
