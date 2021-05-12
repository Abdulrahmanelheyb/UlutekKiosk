using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UlutekKioskModels
{
    public class MysqldbParam
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public enum ValueType
        {
            //
            // Summary:
            //     MySql.Data.MySqlClient.MySqlDbType.Decimal
            //     A fixed precision and scale numeric value between -1038 -1 and 10 38 -1.
            Decimal = 0,
            //
            // Summary:
            //     MySql.Data.MySqlClient.MySqlDbType.Byte
            //     The signed range is -128 to 127. The unsigned range is 0 to 255.
            Byte = 1,
            //
            // Summary:
            //     MySql.Data.MySqlClient.MySqlDbType.Int16
            //     A 16-bit signed integer. The signed range is -32768 to 32767. The unsigned range
            //     is 0 to 65535
            Int16 = 2,
            //
            // Summary:
            //     MySql.Data.MySqlClient.MySqlDbType.Int32
            //     A 32-bit signed integer
            Int32 = 3,
            //
            // Summary:
            //     System.Single
            //     A small (single-precision) floating-point number. Allowable values are -3.402823466E+38
            //     to -1.175494351E-38, 0, and 1.175494351E-38 to 3.402823466E+38.
            Float = 4,
            //
            // Summary:
            //     MySql.Data.MySqlClient.MySqlDbType.Double
            //     A normal-size (double-precision) floating-point number. Allowable values are
            //     -1.7976931348623157E+308 to -2.2250738585072014E-308, 0, and 2.2250738585072014E-308
            //     to 1.7976931348623157E+308.
            Double = 5,
            //
            // Summary:
            //     A timestamp. The range is '1970-01-01 00:00:00' to sometime in the year 2037
            Timestamp = 7,
            //
            // Summary:
            //     MySql.Data.MySqlClient.MySqlDbType.Int64
            //     A 64-bit signed integer.
            Int64 = 8,
            //
            // Summary:
            //     Specifies a 24 (3 byte) signed or unsigned value.
            Int24 = 9,
            //
            // Summary:
            //     Date The supported range is '1000-01-01' to '9999-12-31'.
            Date = 10,
            //
            // Summary:
            //     Time
            //     The range is '-838:59:59' to '838:59:59'.
            Time = 11,
            //
            // Summary:
            //     DateTime The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.
            DateTime = 12,
            //
            // Summary:
            //     Datetime The supported range is '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.
            Datetime = 12,
            //
            // Summary:
            //     A year in 2- or 4-digit format (default is 4-digit). The allowable values are
            //     1901 to 2155, 0000 in the 4-digit year format, and 1970-2069 if you use the 2-digit
            //     format (70-69).
            Year = 13,
            //
            // Summary:
            //     Obsolete Use Datetime or Date type
            Newdate = 14,
            //
            // Summary:
            //     A variable-length string containing 0 to 65535 characters
            VarString = 15,
            //
            // Summary:
            //     Bit-field data type
            Bit = 16,
            //
            // Summary:
            //     JSON
            JSON = 245,
            //
            // Summary:
            //     New Decimal
            NewDecimal = 246,
            //
            // Summary:
            //     An enumeration. A string object that can have only one value, chosen from the
            //     list of values 'value1', 'value2', ..., NULL or the special "" error value. An
            //     ENUM can have a maximum of 65535 distinct values
            Enum = 247,
            //
            // Summary:
            //     A set. A string object that can have zero or more values, each of which must
            //     be chosen from the list of values 'value1', 'value2', ... A SET can have a maximum
            //     of 64 members.
            Set = 248,
            //
            // Summary:
            //     A binary column with a maximum length of 255 (2^8 - 1) characters
            TinyBlob = 249,
            //
            // Summary:
            //     A binary column with a maximum length of 16777215 (2^24 - 1) bytes.
            MediumBlob = 250,
            //
            // Summary:
            //     A binary column with a maximum length of 4294967295 or 4G (2^32 - 1) bytes.
            LongBlob = 251,
            //
            // Summary:
            //     A binary column with a maximum length of 65535 (2^16 - 1) bytes.
            Blob = 252,
            //
            // Summary:
            //     A variable-length string containing 0 to 255 bytes.
            VarChar = 253,
            //
            // Summary:
            //     A fixed-length string.
            String = 254,
            //
            // Summary:
            //     Geometric (GIS) data type.
            Geometry = 255,
            //
            // Summary:
            //     Unsigned 8-bit value.
            UByte = 501,
            //
            // Summary:
            //     Unsigned 16-bit value.
            UInt16 = 502,
            //
            // Summary:
            //     Unsigned 32-bit value.
            UInt32 = 503,
            //
            // Summary:
            //     Unsigned 64-bit value.
            UInt64 = 508,
            //
            // Summary:
            //     Unsigned 24-bit value.
            UInt24 = 509,
            //
            // Summary:
            //     A text column with a maximum length of 255 (2^8 - 1) characters.
            TinyText = 749,
            //
            // Summary:
            //     A text column with a maximum length of 16777215 (2^24 - 1) characters.
            MediumText = 750,
            //
            // Summary:
            //     A text column with a maximum length of 4294967295 or 4G (2^32 - 1) characters.
            LongText = 751,
            //
            // Summary:
            //     A text column with a maximum length of 65535 (2^16 - 1) characters.
            Text = 752,
            //
            // Summary:
            //     Variable length binary string.
            VarBinary = 753,
            //
            // Summary:
            //     Fixed length binary string.
            Binary = 754,
            //
            // Summary:
            //     A guid column.
            Guid = 854
        }
        public ValueType VType { get; set; }
    }
}
