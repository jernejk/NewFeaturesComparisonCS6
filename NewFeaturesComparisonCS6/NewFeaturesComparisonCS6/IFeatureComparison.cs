using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewFeaturesComparisonCS6
{
    public interface IFeatureComparison
    {
        /// <summary>
        /// Gets a read only property value.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Gets the name of a property (in this case IsReadOnly property)
        /// </summary>
        string IsReadOnlyPropertyName { get; }

        /// <summary>
        /// Simple event handler.
        /// </summary>
        event EventHandler OperationCompleted;

        /// <summary>
        /// Simple Math.Pow calculation.
        /// </summary>
        double Compute(double a, double b);

        Task ThrowExceptionIfTrueOtherwiseWaitAsync(bool throwException);

        /// <summary>
        /// Simple log method. It returns reverse bool value of letItThrow which will be useful in C# 6.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="letItThrow">Should this exception be thrown or handled.</param>
        /// <returns>Returns whether this exception should be handled or not.</returns>
        bool Log(Exception exception, bool letItThrow = false);

        /// <summary>
        /// A bit more complex async method when using C# 5 language.
        /// Try, catch and finally contains async methods and awaits are required.
        /// </summary>
        Task DoSomeWorkWithTryCatchFinallyAsync(bool throwExceptionInCatch);

        /// <summary>
        /// Simple event invocation.
        /// </summary>
        void RaiseEvent();

        /// <summary>
        /// This method will throw a exception while also logging that exception before exception bubbles up
        /// </summary>
        void ThrowAndLogException();

        /// <summary>
        /// A simple demonstration of a dictionary settings.
        /// </summary>
        /// <returns>Returns dictionary of settings.</returns>
        Dictionary<string, string> GetSettings();

        /// <summary>
        /// Formats a settings based on their key.
        /// It should gracefully handle missing keys and null settings values.
        /// </summary>
        /// <param name="key">Setting key</param>
        /// <returns>Returns information about a setting.</returns>
        string GetFormattedSettingValue(string key);
    }
}
