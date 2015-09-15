﻿//  
// Copyright (c) Microsoft.  All rights reserved.
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//    http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

namespace Microsoft.Azure.Commands.ApiManagement.Commands
{
    using System.Globalization;
    using System.Management.Automation;
    using Microsoft.Azure.Commands.ApiManagement.Properties;

    [Cmdlet(VerbsCommon.Remove, "AzureRMApiManagement"), OutputType(typeof (bool))]
    public class RemoveAzureApiManagement : AzureApiManagementCmdletBase
    {
        [Parameter(
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "Name of resource group under which API Management service exists.")]
        [ValidateNotNullOrEmpty]
        public string ResourceGroupName { get; set; }

        [Parameter(
            ValueFromPipelineByPropertyName = true,
            Mandatory = true,
            HelpMessage = "Name of API Management service.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Gets or sets the switch to not confirm on the removal API Management service.
        /// </summary>
        [Parameter(HelpMessage = "Do not confirm on the removal of the API Management service.")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            var actionDescription = string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.RemoveAzureApiManagementDescription,
                    Name);

            var actionWarning = string.Format(
                CultureInfo.CurrentCulture,
                Resources.RemoveAzureApiManagementWarning,
                Name);

            // Do nothing if force is not specified and user cancelled the operation
            if (!Force.IsPresent &&
                !ShouldProcess(
                    actionDescription,
                    actionWarning,
                    Resources.ShouldProcessCaption))
            {
                return;
            }

            ExecuteCmdLetWrap(
                () => Client.DeleteApiManagement(ResourceGroupName, Name),
                PassThru.IsPresent);
        }
    }
}