namespace QPD_Middle
{
        public interface IAddressService
        {
            Task<AddressResponse> StandardizeAddressAsync(string address);
        }
    
}
