namespace bulgarita.Models;

public class Harita
{
    public double EnlemDrc;
    public double BoylamDrc;
    public string Mevcut_İsim;//latin bulgarca 
    public string Düzenlenmiş_İsim;//kaldıralım?
    public string KirilBulgr_İsim;
    public string Türkçe_İsim;
    public string Üst_Bölge;
    public string Bölge_Türü;
    public string Kimlik;

    internal Harita() {}
    internal Harita(double EnlemDrc, double BoylamDrc, string Mevcut_İsim,
    /*string KirilBulgr_İsim, string Türkçe_İsim,*/ string Üst_Bölge, string Bölge_Türü, string Kimlik)
    {
        this.EnlemDrc = EnlemDrc;
        this.BoylamDrc = BoylamDrc;
        this.Mevcut_İsim = Mevcut_İsim;
        //this.KirilBulgr_İsim = KirilBulgr_İsim;
        //this.Türkçe_İsim = Türkçe_İsim;
        this.Üst_Bölge = Üst_Bölge;
        this.Bölge_Türü = Bölge_Türü;
        this.Kimlik = Kimlik;
    }
}