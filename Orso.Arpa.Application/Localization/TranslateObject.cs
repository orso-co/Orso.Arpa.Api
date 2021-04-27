
using Orso.Arpa.Domain.Translation;

namespace Orso.Arpa.Application.Localization
{
    //TODO: delete this object after testing!
    public class TranslateObject
    {
        private string _text;

        public TranslateObject()
        {
            StringArray = new []{"hello", "array"};
            OtherData = "someOtherStuff";
            Text = "myText";
        }

        [Translate(nameof(TranslateObject))]
        public string Text
        {
            get => _text;
            private set => _text = value;
        }

        [Translate(nameof(TranslateObject))]
        public string OtherData
        {
            get;
            private set;
        }

        [Translate(nameof(TranslateObject))]
        public string[] StringArray
        {
            get;
            private set;
        }
    }
}
