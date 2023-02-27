﻿using System;
using System.Security.Cryptography;

namespace ScienceArchive.Core.Utils
{
    /// <summary>
    /// Generator of different strings
    /// </summary>
    public static class StringGenerator
    {
        /// <summary>
        /// Generate new salt. Primary for passwords
        /// </summary>
        /// <param name="size"></param>
        /// <returns>Salt for password</returns>
        /// <exception cref="Exception"></exception>
        public static string CreateSalt(int size = 64)
        {
            var byteSalt = RandomNumberGenerator.GetBytes(size);

            if (byteSalt == null)
            {
                throw new Exception("Cannot generate password salt");
            }

            var salt = byteSalt.ToString();

            if (salt == null)
            {
                throw new Exception("Cannot convert salt to string");
            }

            return salt;
        }
    }
}
