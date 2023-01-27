using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers.GuidHelper
{
    public class GuidManager
    {
        public static string CreateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
