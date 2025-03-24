using System;
using System.Collections.Generic;

namespace LocalizationSystem
{
    [Serializable]
    public class LocalizationData
    {
        public List<LocalKeyValue> localKeys = new List<LocalKeyValue>();
    }
}