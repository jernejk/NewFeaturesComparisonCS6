using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NewFeaturesComparisonCS6
{
    public class SharedUtils
    {
        /// <summary>
        /// Simple log method. It returns reverse bool value of letItThrow which will be useful in C# 6.
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="letItThrow">Should this exception be thrown or handled.</param>
        /// <returns>Returns whether this exception should be handled or not.</returns>
        public static bool Log(Exception exception, bool letItThrow = false)
        {
            Debug.WriteLine(exception.ToString());
            return !letItThrow;
        }

        /// <summary>
        /// This method will throw a exception while also logging that exception before exception bubbles up
        /// </summary>
        public static async Task ThrowExceptionIfTrueOtherwiseWaitAsync(bool throwException)
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
    }
}
