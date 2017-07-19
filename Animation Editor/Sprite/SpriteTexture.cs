namespace Animation_Editor.Sprite
{
    class SpriteTexture
    {
        public string DisplayName { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
