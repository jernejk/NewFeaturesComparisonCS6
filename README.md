# NewFeaturesComparisonCS6
Comparison between C# 5 and C# 6 features in a console app (applies to all platforms)

Presented features:
- Auto-property initializes
- nameof
- Method lamdba expressions
- Using static
- Await in catch and finally statements
- Null conditional operator (or sometimes called Elvis operator)
- Exception filtering
- Dictionary initializer
- String interpolation

---

### Auto-property initializer 
Instead of using a private field which is exposed in a property without a setter you can now initialize auto properties.
##### C# 5
```csharp
private bool isReadyOnly = true;
public bool IsReadOnly { get { return isReadyOnly; } }
```
##### C# 6
```csharp
public bool IsReadOnly { get; } = true;
```

### nameof 
Hard coding names of types, properties, fields, etc. as string is a bad idea for refactoring.
This can be solved with LINQ, reflections and other hacks, however they may be expensive and are now unnecessary.

With nameof we get compile time string values of variables, types, fiels, methods, etc.

##### C# 5
```csharp
RaisePropertyChange("PropertyName");
```
##### C# 6
```csharp
RaisePropertyChange(nameof(PropertyName));
```

Additional samples for nameof:
```csharp
nameof(this.FieldName) // "FieldName"
nameof(Model.Employee[0].FullName) // "FullName"

var abc;
nameof(abc) // abc
```

### Method lamdba expressions
One line code in methods can be replaced with lambda expressions.

##### C# 5
```csharp
public string GetString()
{
	return "Hello world";
}
```
##### C# 6
```csharp
public string GetString() => { "Hello world" }
``` 


### Using static
This allows you to use static methods of a class without writing their class.
Useful for classes like System.Math and System.Console.

##### C# 5
```csharp
double value = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2)) * Math.Sin(radius);
```
##### C# 6
```csharp
using static System.Math;

// ...
double value = Sqrt(Pow(a, 2) + Pow(b, 2)) * Sin(radius);
``` 

### Await in catch and finally statements
In C# 5 we could not await results in catch and finally statements. A correct try/catch/finally can be a great pain to implement if all three statements requires await.
This image shows how to handle such scenarios and simpler has become in C# 6.

// TODO: Add image when on GitHub repository

### Null conditional operator - ?. (or sometimes called Elvis operator)
Null-conditional operators ?. address many of the situations where code tends to drown in null-checking. They let you access members and elements only when the receiver is not-null, providing a null result otherwise

##### C# 5
```csharp
// Basic null checking before getting values
string tag;
if (frameworkElement != null)
{
    tag = frameworkElement.Tag as string;
}

// Raising events null and thread safe
var handler = EventHandler;
if (EventHandler != null)
{
	EventHandler();
}

// String length or -1 if null
int length = -1;
if (value != null)
{
	length = value.Length;
}
```
##### C# 6
```csharp
// Basic null checking before getting values
string tag = frameworkElement?.Tag as string;

// Raising events null and thread safe
EventHandler?.Invoke();

// String length or -1 if null.
// If value is null, value?.Length will return null. Operator ?? will use -1 if value?.Lenght expression is null.
int length = value?.Length ?? -1;
``` 

### Exception filtering
Exception filtering allows you to check exception before handling it in catch statement.
It can be used for logging, WebException handling based on HTTP code, etc.
The main difference between C# 5 and 6 is when no catch statements should be handled for certain scenarios.
In C# 5 we had to rethrow exception but in C# 6 we can simply avoid handling catch statement.
Avoiding handling exception preserves the scope of the origin of the exception if exception is globally not handled.

Losing scope in C# 5 due rethrow:
// TODO: Add image a and b

Preserving scope in C# 6:
// TODO: Add image

Additional samples with WebException:
##### C# 5
```csharp
try
{
	// Web stuff
}
catch (WebException exception)
{
	if (((HttpWebResponse)e.Response).StatusCode == 404)
	{
		// Handle exception
	}
	else
	{
		throw;
	}
}
```
##### C# 6
```csharp
try
{
	// Web stuff
}
catch (WebException exception) when (((HttpWebResponse)e.Response).StatusCode == 404)
{
	// Handle exception
}
``` 

### Dictionary initializer
Simplifies Dictionary initialization.

##### C# 5
```csharp
Dictionary<string, string> settings = new Dictionary<string, string>();
settings.Add("EnableStuff", "True");
settings.Add("OffsetStuff", "1234");
settings.Add("WidthStuff", "12345");
settings.Add("HeightStuff", "1234");
```
##### C# 6
```csharp
Dictionary<string, string> settings = new Dictionary<string, string>
{
	["EnableStuff"] = "True",
	["OffsetStuff"] = "1234",
	["WidthStuff"] = "12345",
	["HeightStuff"] = "1234",
};
``` 

### String interpolation
Simplifies and shortens String.Format.

##### C# 5
```csharp
string key, value;

// ...
return string.Format("{0} => `{1}` with length {2}",
    key,
    value ?? "key not found",
    value?.Length ?? -1);
```
##### C# 6
```csharp
string key, value;

// ...
return $"{key} => `{value ?? "key not found"}` with length {value?.Length ?? -1}";
``` 