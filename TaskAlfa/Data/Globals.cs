
using System.Collections.Generic;
using TaskAlfa.Data.Models;

namespace TaskAlfa.Data
{
    public class Globals
    {
        public static string AppVersion = "1.0";
        public static Dictionary<UnterrichtsmodellEnum, string> UnterrichtsmodellDictionary => InitUnterrichtsmodellDictionary();
        public static Dictionary<ExeptionTypeEnum, string> ExceptionText => InitExceptionTextDictionary();
        public static List<Unterrichtsmodell> UnterrichtsmodellList => InitUnterrichtsmodellList();


        private static Dictionary<UnterrichtsmodellEnum, string> InitUnterrichtsmodellDictionary()
        {
            var dic = new Dictionary<UnterrichtsmodellEnum, string>();

            dic.Add(UnterrichtsmodellEnum.VZ, "VZ");
            dic.Add(UnterrichtsmodellEnum.TZ, "TZ");

            return dic;
        }

        private static Dictionary<ExeptionTypeEnum, string> InitExceptionTextDictionary()
        {
            var dic = new Dictionary<ExeptionTypeEnum, string>();

            dic.Add(ExeptionTypeEnum.Concurrency, "Ihre Daten sind nicht mehr aktuell. Bitte aktualisieren Sie den Datensatz und machen Ihre Änderungen erneut.");
            dic.Add(ExeptionTypeEnum.OldData, "Der Datensatz, den Sie aktualisieren möchten, existiert nicht (mehr). Bitte Aktualisieren sie Ihre Daten.");
            dic.Add(ExeptionTypeEnum.RemoveItem, "Der Datensatz kann nicht gelöscht werden, weil er zu anderen Datensätzen eine Verbindung hat. Entfernen Sie erst die verbundenen Datensätze.");

            return dic;
        }

        private static List<Unterrichtsmodell> InitUnterrichtsmodellList()
        {
            var list = new List<Unterrichtsmodell>
            {
            new Unterrichtsmodell {UnterrichtsmodellId = 1, Bezeichnung = "VZ", Beschreibung = "Vollzeit",  ReihenfolgeId = 1},
            new Unterrichtsmodell {UnterrichtsmodellId = 2, Bezeichnung = "TZ", Beschreibung = "Teilzeit", ReihenfolgeId = 2},
            new Unterrichtsmodell {UnterrichtsmodellId = 0, Bezeichnung = "--", Beschreibung = "--", ReihenfolgeId = 3},
            };

            return list;
        }
    }
}
