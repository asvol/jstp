using System;
using System.Collections.Generic;
using ArgumentException = System.ArgumentException;

namespace jstp
{
    public struct InterfaceId:IComparable<InterfaceId>, IEquatable<InterfaceId>
    {
        public bool Equals(InterfaceId other)
        {
            return string.Equals(Name, other.Name) && Equals(Version, other.Version);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ (Version != null ? Version.GetHashCode() : 0);
            }
        }

        public static bool operator ==(InterfaceId left, InterfaceId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(InterfaceId left, InterfaceId right)
        {
            return !left.Equals(right);
        }

        public InterfaceId(string name, Version version)
        {
            Name = name;
            Version = version;
        }

        public readonly string Name;
        public readonly Version Version;


        public static InterfaceId Parse(string src)
        {
            InterfaceId iid;
            if (!TryParse(src,out iid)) throw new ArgumentException("src");
            return iid;
        }

        /// <summary>
        /// Parse like 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="iid"></param>
        /// <returns></returns>
        public static bool TryParse(string src,out InterfaceId iid)
        {
            iid = new InterfaceId();

            if (string.IsNullOrWhiteSpace(src)) return false;
            var arr = src.Trim(ValidateHelper.WhiteSpace).Split(' ');
            
            

            if (arr.Length != 2) return false;

            
            if (string.IsNullOrWhiteSpace(arr[0])) return false;
            if (!ValidateHelper.IsValidName(arr[0])) return false;

            if (string.IsNullOrWhiteSpace(arr[1])) return false;
            if (!ValidateHelper.IsValidVersion(arr[1])) return false;

           

            iid = new InterfaceId(arr[0],System.Version.Parse(arr[1]));
            return true;

        }

        public int CompareTo(InterfaceId y)
        {
            var result = string.Compare(Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
            return result != 0 ? result : Version.CompareTo(y.Version);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is InterfaceId && Equals((InterfaceId) obj);
        }

        public override string ToString()
        {
            return Name + "[" + Version + "]";
        }
    }


    public class EntityKeyComparer : Comparer<InterfaceId>
    {
        public override int Compare(InterfaceId x, InterfaceId y)
        {
            var result = string.Compare(x.Name, y.Name, StringComparison.InvariantCultureIgnoreCase);
            return result != 0 ? result : x.Version.CompareTo(y.Version);
        }
    }
}