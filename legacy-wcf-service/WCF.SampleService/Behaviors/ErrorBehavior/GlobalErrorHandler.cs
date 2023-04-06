// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using WCF.SampleService.Loggers;

namespace WCF.SampleService.Behaviors.ErrorBehavior
{
    public class GlobalErrorHandler : IErrorHandler
    {
        private IFileLogger _fileLogger;
        public GlobalErrorHandler(IFileLogger fileLogger)
        {
            _fileLogger = fileLogger;

        }
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
        }

        /// <summary>
        /// Log an error, then allow the error to be handled as usual.
        /// Return true if the error is considered as already handled
        /// </summary>
        public bool HandleError(Exception error)
        {
            _fileLogger.Log(error.Message);
            return true;
        }
    }
}