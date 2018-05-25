using System;
using AliKuli.UtilitiesNS.Encryption;

namespace AliKuli.Extentions
{
    public static class Encryption
    {
        public static string Encrypt(this string str, string phrase)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            if (string.IsNullOrWhiteSpace(phrase))
                throw new Exception("EncryptionLibrary.Encrypt. Phrase is null. Unable to Encrypt.");

            return EncryptionEngine.Encrypt(str, phrase);
        }

        public static string Encrypt(this string str, string phrase, bool isEncrypt)
        {
            if (!isEncrypt)
                return str;

            return str.Encrypt(phrase);
        }





        public static string Decrypt(this string str, string phrase)
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            if (string.IsNullOrWhiteSpace(phrase))
                throw new Exception("EncryptionLibrary.Decrypt. Phrase is null. Unable to Decrypt.");

            return EncryptionEngine.Decrypt(str, phrase);

        }

        public static string Decrypt(this string str, string phrase, bool isEncrypt)
        {
            if (!isEncrypt)
                return str;

            return str.Decrypt(phrase);

        }
    }
}
