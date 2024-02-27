namespace bulgarita.Models;
public class Dandik
{
    public string Name {get;set;}
    public int DandikSeviyesi {get;set;}
    public Dandik(string name, int seviye)
    {
        this.Name = name;
        DandikSeviyesi = seviye;
    }
}