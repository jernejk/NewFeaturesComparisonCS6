using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NewFeaturesComparisonCS6
{
    public class OldFeatures : IFeatureComparison
    {
        private bool isReadyOnly = true;

        public bool IsReadOnly { get { return isReadyOnly; } }

        public string IsReadOnlyPropertyName { get { return "IsReadOnly"; } }

        public event EventHandler OperationCompleted;

        public double Compute(double a, double b)
        {
            return Math.Pow(a, b);
        }

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
                    await ThrowExceptionIfTrueOtherwiseWaitAsync(throwExceptionInCatch);
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

        public async Task ThrowExceptionIfTrueOtherwiseWaitAsync(bool throwException)
        {
            if (throwException)
            {
                throw new Exception();
            }
            else
            {
                // Simulate async work.
                await Task.Delay(500);
            }
        }

        public void RaiseEvent()
        {
            // Thread safe implementation for raising events.
            var handle = OperationCompleted;
            if (handle != null)
            {
                OperationCompleted(this, EventArgs.Empty);
            }
        }

        public bool Log(Exception exception, bool letItThrow = false)
        {
            Debug.WriteLine(exception.ToString());
            return !letItThrow;
        }

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
                Log(ex);

                // If this exception is not handled, debugger will highlight this line
                // instead the line of the original exception, context (local variables) where exception occurred.
                // Therefore culprit rogueVariable is not visible from here.
                throw;
            }
        }

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
