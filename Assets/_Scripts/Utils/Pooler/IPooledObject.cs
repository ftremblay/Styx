﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts.Utils.Pooler
{
    public interface IPooledObject
    {
        void OnSpawn();
    }
}
