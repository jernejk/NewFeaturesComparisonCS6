using static System.Console;

namespace NewFeaturesComparisonCS6
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static async void ShowStuff(IFeatureComparison stuff)
        {
            stuff.OperationCompleted += Stuff_OperationCompleted;

            WriteLine("Is readonly property name: " + stuff.IsReadOnlyPropertyName);
            WriteLine("Is readonly: " + stuff.IsReadOnly);
            WriteLine("Compute: " + stuff.Compute(2, 4));

            WriteLine("Setting value for EnableStuff: " + stuff.GetFormattedSettingValue("EnableStuff"));
            WriteLine("Setting value for NonExisting: " + stuff.GetFormattedSettingValue("NonExisting"));
            WriteLine("Setting value for ExsitingWithNullValue: " + stuff.GetFormattedSettingValue("ExsitingWithNullValue"));

            await stuff.DoSomeWorkWithTryCatchFinallyAsync();

            WriteLine("---------------------");

            stuff.OperationCompleted -= Stuff_OperationCompleted;
        }

        private static void Stuff_OperationCompleted(object sender, System.EventArgs e)
        {
            WriteLine("DoSomeWorkWithTryCatchFinallyAsync completed.");
        }
    }
}
