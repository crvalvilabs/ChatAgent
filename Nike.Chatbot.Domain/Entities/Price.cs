namespace Nike.Chatbot.Domain.Entities
{
    public class Price
    {
        public int PriceId { get; private set; }
        // Foreign Key
        public int ModelId { get; private set; }
        public NikeModel? Model { get; private set; }
        public decimal ShoePrice { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Price(int modelId, decimal shoePrice, DateTime createdAt)
        {
            SetShoePrice(shoePrice);
            SetModelId(modelId);
            SetCreatedAt(createdAt);
        }

        public Price(int priceId, int modelId, decimal shoePrice, DateTime createdAt)
        {
            PriceId = priceId;
            SetShoePrice(shoePrice);
            SetModelId(modelId);
            SetCreatedAt(createdAt);
        }

        private void SetModelId(int modelId)
        {
            if (modelId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(modelId), "Model ID must be greater than zero.");
            }
            ModelId = modelId;
        }

        private void SetShoePrice(decimal shoePrice)
        {
            if (shoePrice <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(shoePrice), "Shoe price must be greater than zero.");
            }
            ShoePrice = shoePrice;
        }

        private void SetCreatedAt(DateTime createdAt)
        {
            if (createdAt > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException(nameof(createdAt), "Created date cannot be in the future.");
            }
            CreatedAt = createdAt;
        }
    }
}
