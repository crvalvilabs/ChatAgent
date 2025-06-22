namespace Nike.Chatbot.Domain.Entities
{
    public class NikeModel
    {
        public int ModelId { get; private set; }
        public string? ModelName { get; private set; }
        public string? Description { get; private set; }
        public short Year { get; private set; }
        // Relationship
        public ICollection<Price>? ModelPrices { get; private set; }

        public NikeModel(string? modelName, short year)
        {
            SetModelName(modelName);
            SetYear(year);
        }

        public NikeModel(string? modelName, string? description, short year, ICollection<Price>? prices)
        {
            SetModelName(modelName);
            SetDescription(description);
            SetYear(year);
            ModelPrices = prices;
        }

        public NikeModel(int modelId, string? modelName, string? description, short year, ICollection<Price> prices)
        {
            ModelId = modelId;
            SetModelName(modelName);
            SetDescription(description);
            SetYear(year);
            ModelPrices = prices;
        }

        private void SetModelName(string? modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                throw new ArgumentException("Model name cannot be null or empty.", nameof(modelName));
            }
            ModelName = modelName;
        }

        private void SetDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            }
            Description = description;
        }

        private void SetYear(short year)
        {
            if (year < 2020 || year > DateTime.Now.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Year must be between 2020 and the current year.");
            }
            Year = year;
        }
    }
}
