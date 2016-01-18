#![Logo](Doc/icons/logo.jpg) Json.NET#

- [Homepage](http://www.newtonsoft.com/json)
- [Documentation](http://www.newtonsoft.com/json/help)
- [NuGet Package](https://www.nuget.org/packages/Newtonsoft.Json)
- [Release Notes](https://github.com/JamesNK/Newtonsoft.Json/releases)
- [Contributing Guidelines](CONTRIBUTING.md)
- [License](LICENSE.md)
- [Stack Overflow](http://stackoverflow.com/questions/tagged/json.net)

GUID Handling added from .NET 4.0 onwards (working in previous .NET versions). Currently when you serilize a GUID in a Object Array and deserialize it; the GUID become a plain String instance even with full TypeNameHandling.

```c#
public class Clase
{
    public Clase()
    {
        Propiedad = new Object[1];
    }

    public object[] Propiedad { get; set; }
}
 Clase clase = new Clase();    
        clase.Propiedad[0] = Guid.NewGuid();

        string output;

        output = JsonConvert.SerializeObject(clase, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });

 Clase deserializedr = JsonConvert.DeserializeObject<Clase>(output, new JsonSerializerSettings
{
    TypeNameHandling = TypeNameHandling.All
});

        Console.WriteLine(deserializedr.Propiedad[0].GetType()); // String!!!!
```

How GUID Handling works:

A new setting was added to JsonSerializerSettings. This setting allows you to control GUID format when serialize/deserialize.

```c#
 public enum GuidHandling
    {
        /// <summary>
        /// Default Newtonsoft.Json handling. Gives you a String on deserilize.
        /// </summary>
        Default = 0,

        /// <summary>
        /// 00000000000000000000000000000000
        /// </summary>
        Digits = 1,

        /// <summary>
        /// 00000000-0000-0000-0000-000000000000
        /// </summary>
        Hyphens = 2,

        /// <summary>
        /// {00000000-0000-0000-0000-000000000000}
        /// </summary>
        Braces = 3,

        /// <summary>
        /// (00000000-0000-0000-0000-000000000000)
        /// </summary>
        Parentheses = 4,

        /// <summary>
        /// {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
        /// </summary>
        Hexadecimal = 5,

        /// <summary>
        /// Default on serialize. Parse any valid format on deserialize and gives you GUID instance instead String.
        /// </summary>
        Auto = 6
    }
```
Examples:

Original Newtonsoft.Json serialization:
```c#
        output = JsonConvert.SerializeObject(clase, Formatting.Indented, new JsonSerializerSettings
        {
            GuidHandling = GuidHandling.Default,
            TypeNameHandling = TypeNameHandling.All

        });
        output = JsonConvert.SerializeObject(clase, Formatting.Indented, new JsonSerializerSettings
        {
            GuidHandling = GuidHandling.Auto,
            TypeNameHandling = TypeNameHandling.All

        });
        output = JsonConvert.SerializeObject(clase, Formatting.Indented, new JsonSerializerSettings
        {   
            //ignore GuidHandling for default behaviour. No breaking changes!
            TypeNameHandling = TypeNameHandling.All

        });
```
Customize Guid Serialization format:

```c#
 output = JsonConvert.SerializeObject(clase, Formatting.Indented, new JsonSerializerSettings
        {
            GuidHandling = GuidHandling.Hexadecimal, // {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}} format in JSON
            TypeNameHandling = TypeNameHandling.All

        });
```

Original Newtonsoft.Json deserialization:

```c#
  Clase deserializedr = JsonConvert.DeserializeObject<Clase>(output, new JsonSerializerSettings
                                {
                                    // //ignore GuidHandling for default behaviour. No breaking changes!
                                    TypeNameHandling = TypeNameHandling.All
                                });

  Console.WriteLine(deserializedr.Propiedad[0].GetType()); //String!
        
  Clase deserializedr = JsonConvert.DeserializeObject<Clase>(output, new JsonSerializerSettings
                                {
                                    GuidHandling = GuidHandling.Default,
                                    TypeNameHandling = TypeNameHandling.All
                                });
  Console.WriteLine(deserializedr.Propiedad[0].GetType()); //String!
```
GuidHandling deserialization:
```c#
  Clase deserializedr = JsonConvert.DeserializeObject<Clase>(output, new JsonSerializerSettings
                                {
                                    GuidHandling = GuidHandling.Auto, //acepts any valid Guid format
                                    TypeNameHandling = TypeNameHandling.All
                                });
  Console.WriteLine(deserializedr.Propiedad[0].GetType()); //Guid!
  
  Clase deserializedr = JsonConvert.DeserializeObject<Clase>(output, new JsonSerializerSettings
                                {
                                    GuidHandling = GuidHandling.Hexadecimal, //faster than Auto; throws exception if JSON does not meet hexadecial format
                                    TypeNameHandling = TypeNameHandling.All
                                });
  Console.WriteLine(deserializedr.Propiedad[0].GetType()); //Guid! 
```
