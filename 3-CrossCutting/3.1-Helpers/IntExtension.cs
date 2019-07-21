using System;

namespace WishList.Helpers
{
    public static class IntExtension
    {
        public static int ThrowIsZero(this int i) => i == 0 ? throw new Exception("O número não pode estar zerado") : i;
    }
}