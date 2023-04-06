// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

namespace WCF.SampleService.Loggers
{
    public interface IFileLogger
    {
        void Log(string error);
    }
}
