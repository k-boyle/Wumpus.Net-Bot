using System;
using Voltaic;

namespace Umbreon.Interactive.Paginator
{
    public class PaginatedAppearanceOptions
    {
        public static PaginatedAppearanceOptions Default = new PaginatedAppearanceOptions();

        public Utf8String First = new Utf8String("⏮");
        public Utf8String Back = new Utf8String("◀");
        public Utf8String Next = new Utf8String("▶");
        public Utf8String Last = new Utf8String("⏭");
        public Utf8String Stop = new Utf8String("⏹");
        public Utf8String Jump = new Utf8String("🔢");
        public Utf8String Info = new Utf8String("ℹ");

        public string FooterFormat = "Page {0}/{1}";
        public string InformationText = "This is a paginator. React with the respective icons to change page.";

        public JumpDisplayOptions JumpDisplayOptions = JumpDisplayOptions.WithManageMessages;
        public bool DisplayInformationIcon = true;

        public TimeSpan? Timeout = null;
        public TimeSpan InfoTimeout = TimeSpan.FromSeconds(30);
    }

    public enum JumpDisplayOptions
    {
        Never,
        WithManageMessages,
        Always
    }
}
