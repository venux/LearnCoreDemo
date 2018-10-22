using System;

namespace Golden
{
    public class Thing
    {
        public int Get(int number) => Newtonsoft.Json.JsonConvert.DeserializeObject<int>($"{number}");
    }
}
