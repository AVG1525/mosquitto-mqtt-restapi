using System.Collections;
using System.Collections.Generic;

namespace MosquittoSub_API
{
    public interface IMem
    {
        public void Add(string value);
        public IList<string> GetAll();
    }
}
