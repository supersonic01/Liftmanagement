using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Liftmanagement.Common
{
    public class CustomLanguage : CultureInfo
    {
        public CustomLanguage()
            : base(CultureInfo.InvariantCulture.LCID, true)
        {
            this.DateTimeFormat = new DateTimeFormatInfo()
            {
                //your format
                LongDatePattern = "dd.MM.yyyy",
                ShortDatePattern = "dd.MM.yyyy"
            };

            // get internal ctor and create XmlLanguage 
            var mi = typeof(XmlLanguage).GetConstructor(
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                null, new Type[] { typeof(string) }, null);
            Language = (XmlLanguage)mi.Invoke(new[] { "" });


            // set our culture with our format
            var cu_fi = Language.GetType().GetField("_specificCulture", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            cu_fi.SetValue(Language, this);

        }

        public XmlLanguage Language { get; private set; }

    }
}
