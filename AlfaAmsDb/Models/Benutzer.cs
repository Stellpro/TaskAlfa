namespace AlfaAmsDb.Models
{
    public class Benutzer //: BaseModel
    {
        public int BenutzerId { get; set; }
        public string Login { get; set; }
        public string Passwort { get; set; }
        public int BenutzerrolleId { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public int AlfaStandortNr { get; set; }
    }
}
