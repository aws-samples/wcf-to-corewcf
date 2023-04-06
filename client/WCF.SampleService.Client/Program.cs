// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using WCF.SampleService.Client.AuthServiceReference;
using WCF.SampleService.Client.CalculatorServiceReference;
using System;
using System.ServiceModel;

namespace WCF.SampleService.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region Invoke methods of CalculatorService

            CalculatorServiceClient calculatorServiceClient = new CalculatorServiceClient();

            int sumResult = calculatorServiceClient.Add(4, 5);

            try
            {
                calculatorServiceClient.Divide(10, 0);
            }
            catch (FaultException)
            {
                Console.WriteLine("FaultException is thrown as expected");
            }

            #endregion


            #region Invoke methods of UserService

            AuthServiceClient authServiceClient = new AuthServiceClient();

            LoginInfo loginInfo = new LoginInfo
            {
                Email = "dummyemail@domain.com",
                Password = "password"
            };
            bool isAuthenticated = authServiceClient.Authenticate(loginInfo);

            #endregion

            Console.ReadLine();
        }
    }
}
