using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDelivery.Common.Exceptions;
public class NullOrEmptyDataException : BaseException
{
    public NullOrEmptyDataException()
    {
    }

    public NullOrEmptyDataException(string message) : base(message)
    {
    }

    public static void CheckString(string value, string nameOfField)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new NullOrEmptyDataException($"{nameOfField} is null or empty");
    }
}
