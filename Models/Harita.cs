namespace bulgarita.Models;

public class Harita
{
    public double EnlemDrc;
    public double BoylamDrc;
    public string Bulgarca_Latin_İsim;
    public string Bulgarca_Kiril_İsim;
    public string Türkçe_İsim;
    public string Osmanlıca_İsim;
    public string Bölge_Türü;
    public string Üst_Bölge;
    public string Kimlik;

    internal Harita() {}
    internal Harita(double EnlemDrc, double BoylamDrc, string Bulgarca_Latin_İsim, string Bulgarca_Kiril_İsim,
    string Türkçe_İsim, string Osmanlıca_İsim, string Bölge_Türü, string Üst_Bölge, string Kimlik)
    {
        this.EnlemDrc = EnlemDrc;
        this.BoylamDrc = BoylamDrc;
        this.Bulgarca_Latin_İsim = Bulgarca_Latin_İsim;
        this.Bulgarca_Kiril_İsim = Bulgarca_Kiril_İsim;
        this.Türkçe_İsim = Türkçe_İsim;
        this.Osmanlıca_İsim = Osmanlıca_İsim;
        this.Bölge_Türü = Bölge_Türü;
        this.Üst_Bölge = Üst_Bölge;
        this.Kimlik = Kimlik;
    }

    public virtual string[] ToStringArray()
    {
        string[] çıktı = new string[9];
        çıktı[0] = this.EnlemDrc.ToString();
        çıktı[1] = this.BoylamDrc.ToString();
        çıktı[2] = this.Bulgarca_Latin_İsim;
        çıktı[3] = this.Bulgarca_Kiril_İsim;
        çıktı[4] = this.Türkçe_İsim;
        çıktı[5] = this.Osmanlıca_İsim;
        çıktı[6] = this.Bölge_Türü;
        çıktı[7] = this.Üst_Bölge;
        çıktı[8] = this.Kimlik;
        return çıktı;
    }
}