using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Constants
{
    internal static class DBTypes
    {
        public const string Nvarchar = "character varying";
        public const string NvarcharMax = "character varying";
        public const string Varchar = "character varying";
        public const string VarcharMax = "character varying";
        public const string Boolean = "boolean";
        public const string Int = "integer";     
        public const string DateTime = "timestamp without time zone";
        public const string UniqueIdentifier = "uuid";
        public const string Varbinary = "bytea";
        public const string VarbinaryMax = "bytea";
        public const string Money = "money";
        public const string Decimal = "numeric";
        public const string TextArray = "text[]";
        public const string FloatArray = "float[]";
        public const string Float = "float";
    }
}
