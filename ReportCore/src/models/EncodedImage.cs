namespace com.bitscopic.reportcore.models
{
    public class EncodedImage {
        public string Image { get; set; }
        public string AltText { get; set; }

        public EncodedImage(string encodedImage, string altText) {
            this.Image = encodedImage;
            this.AltText = altText;
        }
    }
}