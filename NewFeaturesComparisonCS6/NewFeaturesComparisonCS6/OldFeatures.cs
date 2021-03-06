﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewFeaturesComparisonCS6
{
    /// <summary>
    /// C# 5 (.NET 4.5) features.
    /// </summary>
    public class OldFeatures : IFeatureComparison
    {
        private bool isReadyOnly = true;

        /// <summary>
        /// Gets a read only property value.
        /// </summary>
        public bool IsReadOnly { get { return isReadyOnly; } }

        /// <summary>
        /// Gets the name of a property (in this case IsReadOnly property)
        /// NOTE: I could use LINQ, reflection and other things to make it refactor friendly.
        /// </summary>
        public string IsReadOnlyPropertyName { get { return "IsReadOnly"; } }

        /// <summary>
        /// Simple event handler.
        /// </summary>
        public event EventHandler OperationCompleted;

        /// <summary>
        /// Simple Math.Pow calculation.
        /// </summary>
        public double Compute(double a, double b)
        {
            return Math.Pow(a, b);
        }

        /// <summary>
        /// A bit more complex async method when using C# 5 language.
        /// Try, catch and finally contains async methods and awaits are required.
        /// </summary>
        public async Task DoSomeWorkWithTryCatchFinallyAsync(bool throwExceptionInCatch)
        {
            Exception tryException = null;

            try
            {
                ThrowAndLogException();
            }
            catch (Exception e)
            {
                // This is used for async catch logic.
                tryException = e;
            }

            Exception catchException = null;

            // Before C# 6 we could not use async in catch so we need to write this code to handle catch logic.
            if (tryException != null)
            {
                // This must be in try catch and any exception are thrown after finally
                try
                {
                    // Do analytics, uploading bug infos, etc. This will throw exception if throwExceptionInCatch is true.
                    await SharedUtils.ThrowExceptionIfTrueOtherwiseWaitAsync(throwExceptionInCatch);
                }
                catch (Exception e)
                {
                    // This is what we will rethrow after finally.
                    catchException = e;
                }
            }


            // Finally has to execute regardless what happened above.
            // Perhaps log something or wait for resources to get free.
            RaiseEvent();

            // Throw exception if exception occurred while processing exception from the first try.
            if (catchException != null)
            {
                throw catchException;
            }
        }

        /// <summary>
        /// Simple event invocation.
        /// </summary>
        public void RaiseEvent()
        {
            // Thread safe implementation for raising events.
            var handle = OperationCompleted;
            if (handle != null)
            {
                handle(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// This method will throw a exception while also logging that exception before exception bubbles up
        /// </summary>
        public void ThrowAndLogException()
        {
            try
            {
                // A local variable for try statement.
                bool rogueVariable = true;

                if (rogueVariable)
                {
                    // Debugger will not stop here if exception is not handled.
                    // Exception is handled and then rethrown.
                    throw new Exception("Evil exception!!!");
                }
            }
            catch (Exception ex)
            {
                // Log exception before rethrowing.
                SharedUtils.Log(ex);

                // If this exception is not handled, debugger will highlight this line
                // instead the line of the original exception. Context of try statement (local variables like rogueVariable above) are lost.
                // Therefore culprit rogueVariable has no value when debugger reaches this point.
                throw;
            }
        }

        /// <summary>
        /// A simple demonstration of a dictionary settings.
        /// </summary>
        /// <returns>Returns dictionary of settings.</returns>
        public Dictionary<string, string> GetSettings()
        {
            // Old way of initializing dictionaries
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings.Add("EnableStuff", "True");
            settings.Add("OffsetStuff", "1234");
            settings.Add("WidthStuff", "12345");
            settings.Add("HeightStuff", "1234");

            return settings;
        }

        /// <summary>
        /// Formats a settings based on their key.
        /// It should gracefully handle missing keys and null settings values.
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <returns>Returns information about a setting.</returns>
        public string GetFormattedSettingValue(string key)
        {
            var settings = GetSettings();

            string value;
            int valueLenght = -1;

            if (settings.TryGetValue(key, out value))
            {
                valueLenght = value.Length;
            }

            return string.Format("{0} => `{1}` with length {2}",
                key,
                value ?? "key not found",
                valueLenght);
        }
    }
}
