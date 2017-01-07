﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyClinic.Infrastructure;

namespace MyClinic.Core
{
    public interface ISequenceRepository
    {
        string GetNormalSeqNumber();
        string GetSpecialSeqNumber();
    }
}
