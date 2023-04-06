// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System.ServiceModel;

namespace WCF.SampleService.Contracts
{
    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        int Add(int n1, int n2);

        [OperationContract]
        int Subtract(int n1, int n2);

        [OperationContract]
        int Multiply(int n1, int n2);

        [OperationContract]
        int Divide(int n1, int n2);

        [OperationContract]
        int Factorial(int n);
    }
}
