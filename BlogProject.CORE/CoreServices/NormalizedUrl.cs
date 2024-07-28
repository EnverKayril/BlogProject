using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.CORE.CoreServices
{
    public class NormalizedUrl
    {
        public static string TurkishToEnglish(string name)
        {
            string turkishCharacter = "ığşüöç ";
            string englishCharacter = "igsuoc-";

            for (int i = 0; i < turkishCharacter.Length; i++)
            {
                name = name.ToLower().Replace(turkishCharacter[i], englishCharacter[i]);
            }

            return name;
        }
    }
}