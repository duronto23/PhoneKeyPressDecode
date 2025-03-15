using System.Text;

namespace PhoneKeyPressDecode.Utility;

public static class Extensions
{
    public static void SafelyRemoveLast(this StringBuilder stringBuilder)
    {
        if (stringBuilder.Length > 0)
        {
            stringBuilder.Length--;
        }
    }
}