using System.Collections.Generic;

namespace Day02.Models
{
    public static class Globals
    {
        //ID should NEVER be the name - but it's useful for the example
        public static Dictionary<string, Album> AlbumDictionary = new Dictionary<string, Album>();
        public static void InitData()
        {
            AlbumDictionary.Add("pace", new Album("PACE", "FewJar", 5));
            AlbumDictionary.Add("afewsides", new Album("AFewSides", "FewJar", 7));
            AlbumDictionary.Add("blossom", new Album("Blossom", "MilkyChance", 10));
            AlbumDictionary.Add("forever", new Album("Forever", "Mystery Skulls", 5));
            AlbumDictionary.Add("concreteandgold", new Album("Concrete And Gold", "Foo Fighters", 5));
        }
    }
}