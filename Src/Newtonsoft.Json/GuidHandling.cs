
namespace Newtonsoft.Json
{

    /// <summary>
    /// Specifies Guid handling options for the <see cref="JsonSerializer"/>.
    /// </summary>
    public enum GuidHandling
    {
        /// <summary>
        /// Default Newtonsoft.Json handling
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
        /// Default on serialize. Parse any valid format on deserialize.
        /// </summary>
        Auto = 6

    }
}
