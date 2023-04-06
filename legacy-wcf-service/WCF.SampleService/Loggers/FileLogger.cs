// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: MIT-0

using System.IO;

namespace WCF.SampleService.Loggers
{
    public class FileLogger : IFileLogger
    {
        private string _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string error)
        {
            using (TextWriter textWriter = File.Exists(_logFilePath) ? File.AppendText(_logFilePath) : File.CreateText(_logFilePath))
            {
                if (!string.IsNullOrEmpty(error))
                {
                    textWriter.WriteLine("Exception: " + error.GetType().Name + " - " + error);
                }
                textWriter.Close();
            }
        }
    }
}