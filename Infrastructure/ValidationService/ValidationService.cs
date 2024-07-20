using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ValidationService
{
    public class ValidationService
    {
        private readonly HttpClient _httpClient;
        private readonly PhoneNumberUtil _phoneNumberUtil = PhoneNumberUtil.GetInstance();
        public ValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsValidCityAsync(string cityName,string countryName)
        {
            var encodedCityName = Uri.EscapeDataString(cityName);
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?q={encodedCityName}&format=json");
            request.Headers.Add("User-Agent", "Financial_App");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();           
                return !string.IsNullOrEmpty(content) && content.Contains(cityName) && content.Contains(countryName);
            }
            return false;
        }

        public async Task<bool> IsValidZipCodeAsync(string zipCode,string city)
        {
            var encodedCityName = Uri.EscapeDataString(zipCode);
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?q={zipCode}&format=json");
            request.Headers.Add("User-Agent", "Financial_App");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return !string.IsNullOrEmpty(content) && content.Contains(zipCode) && content.Contains(city);
            }
            return false;
        }
        public bool IsValidPhoneNumber(string phoneNumber, string regionCode)
        {
            try
            {
                var number = _phoneNumberUtil.Parse(phoneNumber, regionCode);
                return _phoneNumberUtil.IsValidNumber(number);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}
