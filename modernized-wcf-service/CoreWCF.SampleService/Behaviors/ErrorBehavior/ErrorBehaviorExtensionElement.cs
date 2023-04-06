// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

#if NETFRAMEWORK
using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using WCF.SampleService.Loggers;


namespace WCF.SampleService.Behaviors.ErrorBehavior
{
    public class ErrorBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(ErrorBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            //var loggerFilePath = ConfigurationManager.AppSettings["logFilePath"].ToString();
            var loggerFilePath = string.Empty; 
            var logger = new FileLogger(loggerFilePath);
            return new ErrorBehavior(typeof(GlobalErrorHandler), logger);
        }
    }
}
#endif