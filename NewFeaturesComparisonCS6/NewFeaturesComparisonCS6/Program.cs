using System;
using System.Threading.Tasks;

using static System.Console;

namespace NewFeaturesComparisonCS6
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Old featured");
            ShowStuff(new OldFeatures()).Wait();

            WriteLine("New featured");
            ShowStuff(new NewFeatures()).Wait();

            WriteLine();
            WriteLine("Press enter key to exit...");
            ReadLine();
        }

        private static async Task ShowStuff(IFeatureComparison stuff)
        {
            stuff.OperationCompleted += Stuff_OperationCompleted;

            WriteLine("Is readonly property name: " + stuff.IsReadOnlyPropertyName);
            WriteLine("Is readonly: " + stuff.IsReadOnly);
            WriteLine("Compute: " + stuff.Compute(2, 4));

            await stuff.DoSomeWorkWithTryCatchFinallyAsync(false);

            try
            {
                await stuff.DoSomeWorkWithTryCatchFinallyAsync(true);
            }
            catch (Exception exception)
            {
                WriteLine("Expected exception for DoSomeWorkWithTryCatchFinallyAsync.");
            }

            WriteLine();

            WriteLine("Setting value for EnableStuff: " + stuff.GetFormattedSettingValue("EnableStuff"));
            WriteLine("Setting value for NonExisting: " + stuff.GetFormattedSettingValue("NonExisting"));
            WriteLine("Setting value for NullValue: " + stuff.GetFormattedSettingValue("NullValue"));
            
            WriteLine("---------------------");

            stuff.OperationCompleted -= Stuff_OperationCompleted;
        }

        private static void Stuff_OperationCompleted(object sender, System.EventArgs e)
        {
            WriteLine("DoSomeWorkWithTryCatchFinallyAsync completed.");
        }
    }
}
