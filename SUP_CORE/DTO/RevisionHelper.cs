using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUP_CORE.DTO
{
    public static class RevisionHelper
    {
        //for view purpose convert rev integer to alphabatic
        public static string ConvertToAlphabet(string rev, bool? isAlpha)
        {
            if (isAlpha == true && int.TryParse(rev, out int revNumber))
            {
                return ((char)('a' + revNumber)).ToString(); // Convert number to letter
            }
            return rev; // Return as-itis if IsAlphaRevNo is false or conversion fails
        }

        //nutral all data if wrong input inserted
        public static string NormalizeRev(string rev, bool? isAlphaRev)
        {
            if (string.IsNullOrEmpty(rev)) return rev; // If empty, return as-is

            if (isAlphaRev == true) // If expecting alphabetic input
            {
                return int.TryParse(rev, out _) ? rev : ConvertToNumericRev(rev); // If numeric, keep it; otherwise, convert it
            }
            else // If expecting numeric input
            {
                return char.IsLetter(rev[0]) ? ConvertToNumericRev(rev) : rev; // If alphabetic, convert; otherwise, keep
            }
        }

        //convert alphabatic to numeric as per nautralized flow
        public static string ConvertToNumericRev(string rev)
        {
            if (!string.IsNullOrEmpty(rev) && char.IsLetter(rev[0]))
            {
                return (char.ToLower(rev[0]) - 'a').ToString(); // Convert alphabet to numeric position
            }
            return rev; // Return as is if IsAlphaRev == "0" or Rev is already numeric
        }
    }
}
