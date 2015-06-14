using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Math;

namespace NewFeaturesComparisonCS6
{
    /// <summary>
    /// C# 6 (.NET 4.6) implementation.
    /// </summary>
    public class NewFeatures : IFeatureComparison
    {
        /// <summary>
        /// Gets a read only property value.
        /// </summary>
        public bool IsReadOnly { get; } = true;

        /// <summary>
        /// Gets the name of a property (in this case IsReadOnly property)
        /// NOTE: This is refactor friendly code.
        /// </summary>
        public string IsReadOnlyPropertyName { get; } = nameof(IsReadOnly);

        /// <summary>
        /// Simple event handler.
        /// </summary>
        public event EventHandler OperationCompleted;

        /// <summary>
        /// Simple Math.Pow calculation.
        /// </summary>
        public double Compute(double a, double b) => Pow(a, b);

        /// <summary>
        /// A bit more complex async method when using C# 5 language.
        /// In C# 6 await is now supported in catch and finally statements!
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
                // Do analytics, uploading bug infos, etc. This will throw exception if throwExceptionInCatch is true.
                await SharedUtils.ThrowExceptionIfTrueOtherwiseWaitAsync(throwExceptionInCatch);
            }
            finally
            {
                // Finally has to execute regardless what happened above.
                // Perhaps log something or wait for resources to get free.
                RaiseEvent();
            }
        }

        /// <summary>
        /// Simple event invocation.
        /// </summary>
        public void RaiseEvent()
        {
            OperationCompleted?.Invoke(this, EventArgs.Empty);
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
                    // If this exception is not handled anywhere, debugger will highlight line bellow.
                    // Value of rogueVariable and everything else in this scope is available.
                    throw new Exception("Evil exception!!!");
                }
            }
            catch (Exception ex) when (SharedUtils.Log(ex, letItThrow: true))
            {
                Debug.WriteLine("This will never execute due exception filtering!");
            }
        }

        /// <summary>
        /// A simple demonstration of a dictionary settings.
        /// </summary>
        /// <returns>Returns dictionary of settings.</returns>
        public Dictionary<string, string> GetSettings()
        {
            // More compact and "JSON like" syntax
            return new Dictionary<string, string>
            {
                ["EnableStuff"] = "True",
                ["OffsetStuff"] = "1234",
                ["WidthStuff"] = "12345",
                ["HeightStuff"] = "1234",
            };
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

            string value = null;
            if (!string.IsNullOrWhiteSpace(key))
            {
                settings.TryGetValue(key, out value);
            }

            // Using Elvis operator and $" for significantly shorter code.
            return $"{key} => `{value ?? "key not found"}` with length {value?.Length ?? -1}";
        }
    }
}
