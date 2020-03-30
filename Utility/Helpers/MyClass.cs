using System.Collections.Generic;

namespace Utility.Helpers
{
    public class MyClass
    {
        private   List<string> Info { get; set; } = new List<string>();

        public void AddInfo(string aData)
        {
            Info.Add(aData);
        }

        public List<string> GetAllData()
        {
            return Info;
        }
    }
}