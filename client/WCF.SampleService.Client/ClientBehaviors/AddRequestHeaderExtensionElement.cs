// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System;
using System.ServiceModel.Configuration;

namespace WCF.SampleService.Client.ClientBehaviors
{
    public class AddRequestHeaderExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(AddRequestHeaderBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new AddRequestHeaderBehavior();
        }
    }
}
