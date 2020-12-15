namespace RpgGameCore2
{
    public class I10n
    {
        public string String;
        public bool ShouldBeTranslated;
        
        public I10n(string s, bool shouldBeTranslated = true)
        {
            String = s;
            ShouldBeTranslated = shouldBeTranslated;
        }
        
        public static implicit operator I10n(string s)
        {
            return new I10n(s);
        }

        public override string ToString()
        {
            // TODO add strings lookup

            return ShouldBeTranslated ? $"{{{String}}}" : String;
        }
    }
}
