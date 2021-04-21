using System;
using Orso.Arpa.Application.Tranlation;

namespace Orso.Arpa.Application.Localization
{
    //TODO: delete this object after testing!
    public class TranslateObject
    {

        public TranslateObject()
        {
            StringArray = new []{"hello", "array"};
            OtherData = "someOtherStuff";
            Text = "myText";
        }

        [Translate]
        public string Text
        {
            get;
            private set;
        }

        [Translate]
        public string OtherData
        {
            get;
            private set;
        }

        [Translate]
        public string[] StringArray
        {
            get;
            private set;
        }
    }
}
