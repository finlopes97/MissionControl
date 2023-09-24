namespace MissionControl;

public class NameLibrary
{
    private List<string> names;

    public NameLibrary()
    {
        names = new List<string>();
        names.Add("Big Boss");
        names.Add("Liquid Snake");
        names.Add("Revolver Ocelot");
        names.Add("Meryl Silverburgh");
        names.Add("Raiden");
        names.Add("Gray Fox");
        names.Add("Quiet");
        names.Add("Otacon");
        names.Add("The Boss");
        names.Add("EVA");
        names.Add("Psycho Mantis");
        names.Add("Vamp");
        names.Add("Sniper Wolf");
        names.Add("Skull Face");
        names.Add("Crying Wolf");
        names.Add("Raven");
        names.Add("Solidus Snake");
        names.Add("The Sorrow");
        names.Add("The End");
        names.Add("The Fear");
        names.Add("The Fury");
        names.Add("The Pain");
    }

    public string GetRandomName()
    {
        Random rand = new Random();
        int index = rand.Next(names.Count);
        return names[index];
    }
}