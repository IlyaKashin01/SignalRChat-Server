using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Common.OperationResult
{
    public enum OperationCode: short
    {
        Ok = 1,
        UnhandledError = 2,
        Error = 3,
        ValidationError = 4,
        EntityWasNotFound = 5,
        AlreadyExists = 6,
        Unauthorized = 7,
        Forbidden = 8
    }
}
