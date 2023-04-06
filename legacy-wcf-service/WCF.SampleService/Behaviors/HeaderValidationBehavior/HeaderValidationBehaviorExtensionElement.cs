// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System;
using System.ServiceModel.Configuration;

namespace WCF.SampleService.Behaviors.HeaderValidationBehavior
{
    public class HeaderValidationBehaviorExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(HeaderValidationBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new HeaderValidationBehavior();
        }
    }
}