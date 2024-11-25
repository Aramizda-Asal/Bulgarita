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

    public Harita() {}
    public Harita(double EnlemDrc, double BoylamDrc, string Bulgarca_Latin_İsim, string Bulgarca_Kiril_İsim,
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
    public Harita(string harita)
    {
        string[] haritaStringArray = harita.Split("/");
        this.EnlemDrc = Convert.ToDouble(haritaStringArray[0]);
        this.BoylamDrc = Convert.ToDouble(haritaStringArray[1]);
        this.Bulgarca_Latin_İsim = haritaStringArray[2];
        this.Bulgarca_Kiril_İsim = haritaStringArray[3];
        this.Türkçe_İsim = haritaStringArray[4];
        this.Osmanlıca_İsim = haritaStringArray[5];
        this.Bölge_Türü = haritaStringArray[6];
        this.Üst_Bölge = haritaStringArray[7];
        this.Kimlik = haritaStringArray[8];
    } 

    public string[] ToStringArray() 
    {
        string[] çıktı =
        [//köşeli parantez??
            this.EnlemDrc.ToString().Replace(',', '.'),
            this.BoylamDrc.ToString().Replace(',', '.'),
            this.Bulgarca_Latin_İsim,
            this.Bulgarca_Kiril_İsim,
            this.Türkçe_İsim,
            this.Osmanlıca_İsim,
            this.Bölge_Türü,
            this.Üst_Bölge,
            this.Kimlik,
        ];
        return çıktı;
    }
}