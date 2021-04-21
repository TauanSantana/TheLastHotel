using System;
using System.Collections.Generic;
using System.Text;

namespace TheLastHotel.Repository.Database
{
    public enum EnumMongoErrorCode
    {
        OK = 0,
        INTERNAL_ERROR = 1,
        HOST_UNREACHABLE = 6,
        HOST_NOT_FOUND = 7,
        UNKNOWN_ERROR = 8,
    }
}
