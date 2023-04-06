// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
#if NETFRAMEWORK
    using System.ServiceModel;
#else
    using CoreWCF;
#endif

namespace WCF.SampleService.Contracts
{
    [ServiceContract]
    public interface IAuthService
    {
        [OperationContract]
        bool Authenticate(LoginInfo loginInfo);
    }

    [DataContract]
    public class LoginInfo
    {
        string email = "";
        string password = "";
        [DataMember]
        [Required]
        [EmailAddress]
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        [DataMember]
        [Required]
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
    }
}