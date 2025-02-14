using System.Text.Json;

namespace Aplication.CustomEntities
{
    public class LowerCaseUnderscoreNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return name.ToLower();
        }
    }
}
