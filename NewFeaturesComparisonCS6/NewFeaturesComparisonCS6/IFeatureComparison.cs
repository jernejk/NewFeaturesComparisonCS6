using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewFeaturesComparisonCS6
{
    public interface IFeatureComparison
    {
        /// <summary>
        /// Simple event handler.
        /// </summary>
        event EventHandler OperationCompleted;

        /// <summary>
        /// Gets a read only property value.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Gets the name of a property (in this case IsReadOnly property)
        /// </summary>
        string IsReadOnlyPropertyName { get; }

        /// <summary>
        /// Simple Math.Pow calculation.
        /// </summary>
        double Compute(double a, double b);

        /// <summary>
        /// A bit more complex async method when using C# 5 language.
        /// Try, catch and finally contains async methods and awaits are required.
        /// </summary>
        Task DoSomeWorkWithTryCatchFinallyAsync();

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
