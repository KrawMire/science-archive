﻿using System;
using ScienceArchive.Core.Utils;

namespace ScienceArchive.Core.Entities
{
    /// <summary>
    /// Represents user
    /// </summary>
    public class User
    {
        private Guid _id = Guid.Empty;
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _passwordSalt = string.Empty;

        internal User(Guid id = default, string? salt = null)
        {
            Id = id == default ? Guid.NewGuid() : id;
            _passwordSalt = salt ?? StringGenerator.CreateSalt();
        }

        /// <summary>
        /// Identifier of the user
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Name of the user
        /// </summary>
        public required string Name
        {
            get => _name;
            set
            {
                if (StringValidator.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Name must not be empty or containing only whitespaces");
                }

                _name = value;
            }
        }

        /// <summary>
        /// E-mail of the user
        /// </summary>
        public required string Email
        {
            get => _email;
            set
            {
                if (!StringValidator.IsEmail(value))
                {
                    throw new Exception("Email value is invalid!");
                }

                _email = value;
            }
        }

        /// <summary>
        /// Login of the user which is used to authorize
        /// </summary>
        public required string Login
        {
            get => _login;
            set => _login = value;
        }

        /// <summary>
        ///	Hash of the salted user password
        /// </summary>
        public required string Password
        {
            get => _password;
            set
            {
                // TODO: Create hashing algorithm
                _password = value + _passwordSalt;
            }
        }
    }
}
