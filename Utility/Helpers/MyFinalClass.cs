using System.Collections.Generic;

namespace Utility.Helpers
{
    public class MyFinalClass <T> 
    {
        
        private   List<T> Info { get; set; } = new List<T>();

        public void AddInfo(T aData)
        {
            Info.Add(aData);
        }
        
        public List<T> GetAllData()
        {
            return Info;
        }
    }
}