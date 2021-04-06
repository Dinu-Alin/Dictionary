using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryV3
{
    public class ObservableSet<T>:ObservableCollection<T>
    {
        public new void Add(T item)
        {
            if (Contains(item))
                return;

            base.Add(item);
        }

        public override string ToString()
        {
            return "empty";
        }
    }
}
