﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TrendLens.Client.Entities;

namespace TrendLens.Client.ApiHandles
{
    public class ApiManager
    {
        private readonly HttpClient _httpClient;

        public ApiManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Dictionary<DateTime, int>> GetRedditPostCountsAsync(string searchTerm)
        {
            // Define the Reddit API URL for search
            string url = $"https://www.reddit.com/search.json?q={Uri.EscapeDataString(searchTerm)}";

            // Set User-Agent header as Reddit requires it
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("TrendLensApp/1.0");

            try
            {
                // Make the GET request
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response
                string responseBody = await response.Content.ReadAsStringAsync();
                var redditResponse = JsonSerializer.Deserialize<RedditResponse>(responseBody);

                // Initialize dictionary to count posts per date
                var dateCounts = new Dictionary<DateTime, int>();

                // Process each post
                foreach (var child in redditResponse.data.children)
                {
                    var createdDate = ConvertUnixTimeToDate(child.created_utc);

                    if (dateCounts.ContainsKey(createdDate))
                    {
                        dateCounts[createdDate]++;
                    }
                    else
                    {
                        dateCounts[createdDate] = 1;
                    }
                }

                return dateCounts;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

        private static DateTime ConvertUnixTimeToDate(long unixTime)
        {
            // Convert Unix timestamp to DateTime
            var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTime);
            var dateTime = dateTimeOffset.DateTime;
            return dateTime.Date; // Only return the date part
        }
    }
}
