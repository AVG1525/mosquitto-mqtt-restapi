
using System.Collections.Generic;

namespace MosquittoSub_API
{
    public class Mem : IMem
    {
        private static List<string> SubMemory = new List<string>();

        public void Add(string value)
        {
            SubMemory.Add(value);
        }

        public IList<string> GetAll()
        {
            return SubMemory;
        }
    }
}
