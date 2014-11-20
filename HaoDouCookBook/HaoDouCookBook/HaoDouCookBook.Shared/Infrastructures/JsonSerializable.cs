using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.Infrastructures
{
    public abstract class CustomJsonSerializableBase
    {
        public abstract bool Deserialize(string json);
    }
}
