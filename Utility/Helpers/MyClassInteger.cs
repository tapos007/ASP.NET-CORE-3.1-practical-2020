using System.Collections.Generic;

namespace Utility.Helpers
{
    public class MyClassInteger
    {
        private   List<int> Info { get; set; } = new List<int>();

        public void AddInfo(int aData)
        {
            Info.Add(aData);
        }

        public List<int> GetAllData()
        {
            return Info;
        }
    }
}